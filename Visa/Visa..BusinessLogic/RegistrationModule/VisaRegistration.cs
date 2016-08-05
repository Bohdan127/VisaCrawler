using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.RegistrationModule.Helpers;
using Visa.Database;
using Visa.Resources;
using Visa.WebCrawler.SeleniumCrawler;

namespace Visa.BusinessLogic.RegistrationModule
{
    public class VisaRegistration //: IRegistration
    {
        #region Members

        public event EventHandler<StateEventArgs> StateChanged;

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private readonly BackgroundWorker _crawlerWorker;

        private readonly StateManager _stateManager;

        private RegisterUser _crawlerRegistry;

        /// <summary>
        ///     Field used for getting part of program
        /// </summary>AlertFormList
        private int _state = 1;

        private readonly AlertControl _alertControl;

        private const int RefreshCount = 15;

        private VisaDataSet.ClientDataRow _clientDataRow;

        public VisaDataSet.ClientDataRow ClientDataRow
        {
            get { return _clientDataRow; }
            set
            {
                //todo should we always trust to incoming data or better check it???
                _clientDataRow = value;
            }
        }

        #endregion

        #region CTOR

        public VisaRegistration()
        {
            //Background Worker => _crawlerWorker

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            //Alert COntrol => _alertControl

            _alertControl = new AlertControl();
            _alertControl.AlertClick += _alertControl_AlertClick;
            _alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
            _alertControl.FormLoad += _alertControl_FormLoad;

            _stateManager = new StateManager();
        }


        #endregion

        #region Events

        #region Alert Events

        private void _alertControl_BeforeFormShow(object sender,
           AlertFormEventArgs e)
        {
            _logger.Trace(
                $"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}; " +
                $"Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2};");
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2,
                (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender,
            AlertClickEventArgs e)
        {
            _logger.Info("Start _alertControl_AlertClick. " +
                         $"Alert Text - {e.AlertForm.Text}. State - {_state}");
            StartNewWorkRoundBase();
            e.AlertForm.Close();
            _logger.Trace("End _alertControl_AlertClick. " +
                          $"e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}.");
        }

        private void _alertControl_FormLoad(object sender,
            AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            if (e.AlertForm.AlertInfo.Text.IsNotBlank())
                e.Buttons.PinButton.SetDown(true);
        }

        #endregion

        private void _crawlerWorker_DoWork(object sender,
          DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_state}");

            bool bBreak = CheckSiteAvailability();
            while (!bBreak)
            {
                //todo later here should be changed for collecting all rows instead first one like now
                CrawlerRefreshEngine();
                _logger.Info($"End CrawlerWorkSecondPart _state={_state}");
                if (_crawlerRegistry != null && _crawlerRegistry.Canceled)
                {
                    _logger.Warn($" _crawlerRegistry.Canceled _state={_state}");
                    bBreak = true;
                    SetDefaultState();
                    //CloseBrowsers(true);//todo should be tested with false, before was always true here
                    CloseBrowsers(false);
                    _crawlerRegistry.Canceled = false;
                    _crawlerRegistry.Error = false;
                }
                else if (_crawlerRegistry != null && _crawlerRegistry.Error)
                {
                    _logger.Warn(
                        $"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawlerRegistry.OutData}. _crawlerRegistry.Error = true ");
                    if (SetupManager.GetOptions().RepeatIfCrash
                        && (_state == 4 || _state == 5 || _state == 6))
                        _state = 2;
                    else
                    {
                        SetDefaultState();
                        bBreak = true;
                    }
                    ShowAlert(_crawlerRegistry.OutData.IsNotBlank()
                        ? _crawlerRegistry.OutData
                        : ResManager.GetString(ResKeys.ServerError),
                        true);
                    Thread.Sleep(500);
                    CloseBrowsers(false);
                    _crawlerRegistry.Error = false;
                }
                else if (_state == 8 && _crawlerRegistry != null)
                {
                    ShowAlert(_crawlerRegistry.OutData,
                        true);
                    bBreak = !SetupManager.GetOptions().RepeatIfCrash;
                }
                else if (_state == 6 || _state == 9)
                {
                    ShowAlert(ResManager.GetString(ResKeys.FillCaptchaAndPress),
                        false);
                    bBreak = true;
                }
                else if (_state == 1)
                {
                    ShowAlert(
                        ResManager.GetString(ResKeys.FillCaptchaAndComplete),
                        true);
                    bBreak = true;
                }
            }

            _logger.Trace(
                $"End _crawlerWorker_DoWork. State = {_state}. _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
        }

        #endregion

        #region Functions

        private bool CheckSiteAvailability()
        {
            bool Break = false;
            var serverAvailable = _stateManager.GetCurrentSiteAvailability();
            if (!serverAvailable)
            {
                _logger.Warn("Site is Unavailable. Error Message is shown.");
                DialogResult dResult = XtraMessageBox.Show(
                    ResManager.GetString(ResKeys.AvailabilityError_Message),
                    ResManager.GetString(ResKeys.PageNotAvailable), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Stop);
                if (dResult == DialogResult.Cancel)
                {
                    _crawlerRegistry.Canceled = true;
                    Break = true;
                    //new OperationCanceledException();
                }
                else
                {
                    while (!serverAvailable && !_crawlerRegistry.Canceled)
                    {
                        int t = 2000;// t = 2s
                        _logger.Warn($"Site is Unavailable Again. Thread.Sleep {t / 1000}s.");
                        Thread.Sleep(t);
                        serverAvailable = _stateManager.GetCurrentSiteAvailability();
                    }
                }
            }
            if (serverAvailable)
            {
                ShowAlert(ResManager.GetString(ResKeys.PageAvailable), true);
                _logger.Info("Site is Available. Process is Starting.");
            }
            else
            {
                _crawlerRegistry.Canceled = true;
                Break = true;
            }
            return Break;
        }

        /// <summary>
        ///     Close Browsers for all Crawler workers
        /// </summary>
        /// <param name="forceClose">Should it be closed without looking for toggleSwitchCloseBrowser flag? </param>
        public virtual void CloseBrowsers(bool forceClose)
        {
            if (!forceClose && !SetupManager.GetOptions().CloseBrowser)
                return;
            _crawlerRegistry?.CloseBrowser();
        }

        private void CrawlerChangeStateEngine()
        {
            _logger.Trace($"Start CrawlerChangeStateEngine _state = {_state}");

            switch (_state)
            {
                case 1:     // alerts.Close(), and StartsAgain
                    _alertControl.AlertFormList.ForEach(
                        alert => alert.Close());
                    _crawlerRegistry = new RegisterUser();
                    _state = 2;
                    break;

                case 2:     // GoToUrl()
                    _crawlerRegistry.GoToUrl();
                    _state = 3;
                    break;

                case 3:     // StartRegistration()
                    _crawlerRegistry.StartRegistration();
                    _state = 4;
                    break;

                case 4:     // SelectCityAndReason(dataRow)
                    _crawlerRegistry.SelectCityAndReason(ClientDataRow);
                    _state = 5;
                    break;

                case 5:     // ProvidePeopleCount(dataRow)
                    _crawlerRegistry.ProvidePeopleCount(ClientDataRow);
                    _state = 6;
                    break;

                case 6:     // SelectVisaType(dataRow)
                    _crawlerRegistry.SelectVisaType(ClientDataRow);
                    _state = 7;
                    break;

                case 7:     //CheckData(dataRow)
                    var isAvailableDate =
                        _crawlerRegistry.CheckData(ClientDataRow);
                    _state = isAvailableDate
                        ? 9
                        : 8;
                    break;

                case 8:     // BackToCityAndReason()
                    _crawlerRegistry.BackToCityAndReason();
                    _state = 4;     // SelectCityAndReason(dataRow)
                    break;

                case 9:     // Receipt(dataRow)
                    _crawlerRegistry.Receipt(ClientDataRow);
                    _state = 10;     // ClientData(dataRow)
                    break;

                case 10:     // ClientData(dataRow)
                    _crawlerRegistry.ClientData(ClientDataRow);
                    _state = 1;     // alerts.Close(), and StartAgain
                    //todo dataRow.Status
                    break;

                #region Old dead code, will be removed later because probably part of them will be needed later

                //XtraMessageBox.Show("Реєстрація клієнта закінчена",//todo move it to resource
                //    ResManager.GetString(ResKeys.SearchResult),
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Exclamation);
                //_state = 4;

                //case 4:
                //    _crawlerRegistry.PartFour(dataRow);
                //    if (!_crawlerRegistry.Error)
                //    {
                //        XtraMessageBox.Show(_crawlerRegistry.OutData,
                //            ResManager.GetString(ResKeys.SearchResult),
                //            MessageBoxButtons.OK,
                //            MessageBoxIcon.Exclamation);
                //    }
                //    _state = 5;
                //    break;

                //case 5:
                //    _crawlerRegistry.PartFive();
                //    if (!_crawlerRegistry.Error)
                //    {
                //        _logger.Info(
                //            $"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawlerRegistry.OutData}. _crawlerRegistry.Error = false ");
                //        XtraMessageBox.Show(_crawlerRegistry.OutData,
                //            ResManager.GetString(ResKeys.SearchResult),
                //            MessageBoxButtons.OK,
                //            MessageBoxIcon.Exclamation);
                //        CloseBrowsers(false);
                //        return;
                //    }
                //    break;

                #endregion Old dead code, will be removed later because probably part of them will be needed later

                default:
                    _logger.Error(
                        $"incorrect State = {_state} for CrawlerWorkFirstPart");
                    break;
            }

            _logger.Trace($"End CrawlerChangeStateEngine. _state={_state}");
        }

        private void CrawlerRefreshEngine()
        {
            _logger.Trace($"Start CrawlerRefreshEngine _state = {_state}");
            var counter = 0;
            do
            {
                _crawlerRegistry.Error = false;
                CrawlerChangeStateEngine();
                if (!_crawlerRegistry.Canceled && _crawlerRegistry.Error)
                {
                    _logger.Warn($"!_crawlerRegistry.Canceled && _crawlerRegistry.Error. _state = {_state}. counter = {counter}");
                    //var breakOut = false;
                    switch (_state)
                    {
                        case 8:     // BackToCityAndReason()
                        case 9:     // Receipt(dataRow)
                            _state = 4;     // SelectCityAndReason(dataRow)
                            break;
                        //todo for delete couple commits later
                        //case 9:     // ClientData(dataRow)
                        //case 1:     // alerts.Close(), and StartAgain
                        //    breakOut = true;
                        //    break;    
                        default:
                            _state--;
                            break;
                    }
                    var serverAvailable =
                        _stateManager.GetCurrentSiteAvailability();
                    counter++;
                    Thread.Sleep(1000);
                    if (!serverAvailable)
                    {
                        _logger.Warn($"Reload page. _state = {_state}");
                        _crawlerRegistry.ReloadPage();
                    }
                }
                else
                    break;
            } while (counter < RefreshCount && _crawlerRegistry.Error);
            _logger.Trace($"End CrawlerRefreshEngine. _state={_state}");
        }

        private void SetDefaultState()
        {
            _logger.Trace(
                $"Start SetDefaultState. State = {_state}.");
            _state = 1;
            StateChanged?.Invoke(null, new StateEventArgs(false));
            _logger.Trace(
                $"End SetDefaultState. State = {_state}.");
        }

        /// <summary>
        ///     Show Alert Message about User needed action
        /// </summary>
        private void ShowAlert(string message,
            bool title)
        {
            _logger.Info($"ShowAlert with message = {message}. title = {title}");
            if (title)
                _alertControl.Show(null,
                    message,
                    "");
            else
                _alertControl.Show(null,
                    "",
                    message);
        }

        public void StartRegistration()
        {
            StateChanged?.Invoke(null, new StateEventArgs(true));
            StartNewWorkRoundBase();
        }
        public void StartNewWorkRoundBase()
        {
            _logger.Trace("Start StartNewWorkRoundBase");

            if (!_crawlerWorker.IsBusy)
            {
                _logger.Info("Start _crawlerWorker.RunWorkerAsync");
                _crawlerWorker.RunWorkerAsync();
            }
            else
            {
                _logger.Warn("_crawlerWorker.IsBusy = true");
            }
            _logger.Trace("End StartNewWorkRoundBase");
        }

        #endregion
    }
}
