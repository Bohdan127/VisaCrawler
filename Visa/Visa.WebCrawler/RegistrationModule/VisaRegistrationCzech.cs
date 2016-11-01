//#define GoWithoutDates
//#define ClientPerformanceRequest

using DevExpress.XtraEditors;
using GlobalResources;
using NLog;
using System;
using System.Media;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.RegistrationModule.Helpers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.WebCrawler.SeleniumCrawler;

namespace Visa.WebCrawler.RegistrationModule
{
    public class VisaRegistrationCzech
    {
        public VisaRegistrationCzech()
        {
            InitOtherComponentDetails();
        }

        #region Members

        public static bool Canceled { get; set; }

        public EventHandler<ShowAlertMessageEventArgs>
            ShowAlertMessagEventHandler;

        public VisaDataSet.ClientDataRow CurrentClientDataRow
        {
            get;
            private set;
        }

        private RegisterUserCzech _crawlerRegistry;

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Field used for getting part of program
        /// </summary>
        private ProgressState _progressState = ProgressState.Start;

        private const int RefreshCount = int.MaxValue - 1;

        #endregion Members

        #region Functions

        public void StartWork(VisaDataSet.ClientDataRow clientDataRow)
        {
            _logger.Trace(
                $"Start _crawlerWorker_DoWork. State = {_progressState}");

            if (clientDataRow != null)
                CurrentClientDataRow = clientDataRow;

            if(_crawlerRegistry != null)
                _crawlerRegistry.Canceled = Canceled;
            var bBreak = false; //_crawlerWorker_CheckSiteAvailability();

            do
            {
                CrawlerRefreshEngine();
                if (_crawlerRegistry == null)
                {
                    throw new NotImplementedException();
                    //This error instead of NullPointerException,
                    //because if _crawlerRegistry is null here we something code in wrong way
                }
                if (_crawlerRegistry.Canceled)
                {
                    _logger.Warn(
                        $" _crawlerRegistry.Canceled _state={_progressState}");
                    bBreak = true;
                    SetDefaultState();
                    CloseBrowsers(false);
                    _crawlerRegistry.Canceled = false;
                    _crawlerRegistry.Error = false;
                }
                else if (_crawlerRegistry.Error) // if Error
                {
                    _logger.Warn(
                        $"return _crawlerWorker_DoWork. State = {_progressState}."
                        + $" OutData = {_crawlerRegistry.OutData}. _crawlerRegistry.Error = true ");
                    SetDefaultState();
                    bBreak = true;
                    SystemSounds.Beep.Play();
                    ShowAlert(_crawlerRegistry.OutData.IsNotBlank()
                        ? _crawlerRegistry.OutData
                        : ResManager.GetString(ResKeys.ServerError),
                        true);
                    CloseBrowsers(false);
                    _crawlerRegistry.OutData = string.Empty;
                    _crawlerRegistry.Error = false;
                }
                else
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (_progressState)
                    {
                        case ProgressState.Start:
                            XtraMessageBox.Show(
                                ResManager.GetString(
                                    ResKeys.Complete_Registration),
                                ResManager.GetString(ResKeys.SearchResult),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            bBreak = true;
                            SetDefaultState();
                            break;

                        case ProgressState.SelectVisaType:
                        case ProgressState.ShowMessage:
#if (!GoWithoutDates)
                        case ProgressState.GetFirstDate:
#endif
                        case ProgressState.SubmitDate:
                        case ProgressState.SubmitClientData:
                            string sss = "";
                            if (SetupManager.GetOptions().RuCaptchaID.IsNotBlank())
                                sss = _crawlerRegistry.SendRecaptchav2Request(
                                    "6Lc7lBATAAAAAG-R0DVv6sR4BJPtVLMFJf7YusKQ");
                            if (sss.StartsWith("OK"))
                            {
                                ShowAlert(
                                    $"OK response: \r\n{sss.Substring(3)}",
                                    true);
                            }
                            else
                            {
                                SystemSounds.Beep.Play();
                                ShowAlert(
                                    ResManager.GetString(ResKeys.FillCaptchaAndPress),
                                    false);
                                ShowAlert(
                                    $"Error in response: \r\n{sss}",
                                    true);
                                bBreak = true;
                            }
                            break;
#if (GoWithoutDates)
                            case ProgressState.GetFirstDate:
                            ShowAlert(
                                ResManager.GetString(ResKeys.Fill_Calendar_And_Captcha),
                                false);
                            bBreak = true;
                            break;
#else
                        case ProgressState.SelectRegistrationTime:
                            SystemSounds.Beep.Play();
                            ShowAlert(_crawlerRegistry.OutData,
                                true);
                            ShowAlert(
                                ResManager.GetString(ResKeys.FillCaptchaAndPress),
                                false);
                            bBreak = true;
                            break;
#endif
                        case ProgressState.BreakState:
                            SetDefaultState();
                            bBreak = true;
                            break;
                    }
            } while (!bBreak);
            
            _logger.Trace(
                $"End _crawlerWorker_DoWork. State = {_progressState}."
                + $" _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
        }

        private void CrawlerWorkSecondPart()
        {
            _logger.Trace(
                $"Start CrawlerWorkSecondPart _state = {_progressState}");

            if (CurrentClientDataRow == null)
            {
                _logger.Error("return _crawlerWorker_DoWork dataRow == null");
                return;
            }
            switch (_progressState)
            {
                case ProgressState.Start:
                    _crawlerRegistry = new RegisterUserCzech();
                    _crawlerRegistry.Canceled = Canceled;
                    _progressState = ProgressState.GoToUrl;
                    break;

                case ProgressState.GoToUrl:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.GoToUrl());
                    _progressState = ProgressState.StartRegistration;
                    break;

                case ProgressState.StartRegistration:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.StartRegistration());
                    _progressState = ProgressState.SelectCityAndReason;
                    break;

                case ProgressState.SelectCityAndReason:
                    _crawlerRegistry.RunNextStep(
                        () =>
                            _crawlerRegistry.SelectCityAndReason(
                                CurrentClientDataRow));
                    _progressState = ProgressState.SubmitCityAndReason;
                    break;

                case ProgressState.SubmitCityAndReason:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.PressSubmitButton());
                    _progressState = ProgressState.ProvidePeopleCount;
                    break;

                case ProgressState.ProvidePeopleCount:
                    _crawlerRegistry.RunNextStep(
                        () =>
                            _crawlerRegistry.ProvidePeopleCount(
                                CurrentClientDataRow));
                    _progressState = ProgressState.SelectVisaType;
                    break;

                case ProgressState.SelectVisaType:
                    _crawlerRegistry.RunNextStep(
                        () =>
                            _crawlerRegistry.SelectVisaType(CurrentClientDataRow));

                    if (!_crawlerRegistry.ReEnterCaptcha)
                        _progressState = ProgressState.CheckDate;

                    break;

                case ProgressState.CheckDate:
                    //todo bbenetskyy 26.08.2016 this checking is a crutch, should be refactored later
                    if (_crawlerRegistry.IsServerDown)
                    {
                        _crawlerRegistry.Error = true;
                        _logger.Warn(
                            $"End CheckDate. Error = {_crawlerRegistry.Error}.");
                        return;
                    }
                    var isAvailableDate =
                        _crawlerRegistry.CheckDate(CurrentClientDataRow);
                    if (_crawlerRegistry.FillCapchaFailed)
                    {
                        _crawlerRegistry.Error = false;
                        _progressState = ProgressState.SelectVisaType;
                        _logger.Warn("Fill Capcha Failed");
                        _crawlerRegistry.FillCapchaFailed = false;
                        return;
                    }
                    if (!_crawlerRegistry.Error)
                    {
                        if (isAvailableDate)
                        {
                            _crawlerRegistry.RegistrarionDateAvailability =
                                DialogResult.Yes;
                            _progressState = ProgressState.SubmitDate;
                        }
                        else
                        {
#if (!GoWithoutDates)
                            _crawlerRegistry.RegistrarionDateAvailability =
                                DialogResult.Retry;
                            _progressState = ProgressState.BackToCityAndReason;
#else
                            var dialogResult =
                                XtraMessageBox.Show(_crawlerRegistry.OutData,
                                    ResManager.GetString(ResKeys.SearchResult),
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);
                            // ReSharper disable once SwitchStatementMissingSomeCases
                            switch (dialogResult)
                            {
                                case DialogResult.Cancel:
                                    _crawlerRegistry.RegistrarionDateAvailability =
                                        DialogResult.Cancel;
                                    _progressState = BreakState;
                                    //go out from the registration process
                                    break;

                                case DialogResult.Yes:
                                    _crawlerRegistry.RegistrarionDateAvailability =
                                        DialogResult.None;
                                    _progressState = ProgressState.Receipt;
                                    break;

                                case DialogResult.No:
                                    _crawlerRegistry.RegistrarionDateAvailability =
                                        DialogResult.Retry;
                                    _progressState =
                                        ProgressState.BackToCityAndReason;
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
#endif
                        }
                        SystemSounds.Beep.Play();
                        ShowAlert(
                            _crawlerRegistry.OutData,
                            true);
                    }
                    break;

                case ProgressState.SubmitDate:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.PressSubmitButton());
                    _progressState = ProgressState.Receipt;
                    break;

                case ProgressState.BackToCityAndReason:
                    //todo bbenetskyy 26.08.2016 This is also not good, should be refactored
                    if (_crawlerRegistry.IsServerDown)
                    {
                        _crawlerRegistry.Error = true;
                        _logger.Warn(
                            $"End BackToCityAndReason. Error = {_crawlerRegistry.Error}.");
                        break;
                    }
                    _crawlerRegistry.BackToCityAndReason();
                    _progressState = ProgressState.SelectCityAndReason;
                    break;

                case ProgressState.Receipt:
                    //todo bbenetskyy 26.08.2016 - this is temporary fix, should be reviewed and refactored
                    if (_crawlerRegistry.SpecialTmpCheckForCapchaError())
                    {
                        _logger.Warn("Capcha was not fill!!!");
                        _progressState = ProgressState.SubmitDate;
                        return;
                    }
                    if (_crawlerRegistry.Error)
                    {
                        _progressState = ProgressState.SubmitReciept;
                        return;
                    }
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.Receipt(CurrentClientDataRow));
                    _progressState = ProgressState.SubmitReciept;
                    break;

                case ProgressState.SubmitReciept:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.PressSubmitButton());
                    _progressState = ProgressState.EmailAndPassword;
                    break;

                case ProgressState.EmailAndPassword:
#if ClientPerformanceRequest
                    _crawlerRegistry.RunNextStepV2(
                                       () => _crawlerRegistry.EmailAndPassword(currentClientDataRow));
#else
                    _crawlerRegistry.RunNextStep(
                        () =>
                            _crawlerRegistry.EmailAndPassword(
                                CurrentClientDataRow));
#endif
                    _progressState = ProgressState.SubmitEmailAndPassword;
                    break;

                case ProgressState.SubmitEmailAndPassword:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.PressSubmitButton(true));
                    _progressState = ProgressState.ClientData;
                    break;

                case ProgressState.ClientData:
#if ClientPerformanceRequest
                    _crawlerRegistry.RunNextStepV2(
                                       () => _crawlerRegistry.ClientData(currentClientDataRow));
#else
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.ClientData(CurrentClientDataRow));
#endif
                    _progressState = ProgressState.SubmitClientData;
                    break;

                case ProgressState.SubmitClientData:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.PressSubmitButton());
                    _progressState = ProgressState.CheckForErrorBeforeDate;
                    break;

                case ProgressState.CheckForErrorBeforeDate:
                    //todo bbenetskyy 26.08.2016 This just one more tmp crutch
                    _crawlerRegistry.RunNextStep(
                        () => { });
                    _progressState = ProgressState.GetFirstDate;
                    break;

                case ProgressState.GetFirstDate:
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (_crawlerRegistry.RegistrarionDateAvailability)
                    {
                        case DialogResult.Cancel:
                        case DialogResult.Retry:
#if !GoWithoutDates
                        case DialogResult.None:
#endif
                            _logger.Warn(
                                $"{_crawlerRegistry.RegistrarionDateAvailability}"
                                + $" for GetFirstDate _state={_progressState}");
                            _progressState = ProgressState.Start;
                            EmailManager.SendErrorMail(
                                new NotImplementedException());
                            XtraMessageBox.Show(
                                ResManager.GetString(
                                    ResKeys.Application_Logic_Error),
                                ResManager.GetString(ResKeys.SearchResult),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            break;

                        case DialogResult.Yes:
                            _logger.Info(
                                $"Yes for GetFirstDate _state={_progressState}");

                            _crawlerRegistry.RunNextStep(
                                () =>
                                    _crawlerRegistry.GetFirstDate(
                                        CurrentClientDataRow));
                            if (!_crawlerRegistry.ReEnterCaptcha)
                                _progressState =
                                    ProgressState
                                        .CheckForErrorBeforeRegistrationTime;
                            break;
#if GoWithoutDates
                        case DialogResult.None:
                            _progressState = ProgressState.ShowMessage;//show special message for selecting Date of Registration
                            break;
#endif
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
#if GoWithoutDates
                case ProgressState.ShowMessage:// show special message for selecting Date of Registration
                    _progressState = ProgressState.SelectRegistrationTime;
                    break;
#endif
                case ProgressState.CheckForErrorBeforeRegistrationTime:
                    //todo bbenetskyy 26.08.2016 This just one more tmp crutch
                    _crawlerRegistry.RunNextStep(
                        () => { });
                    _progressState =
                        ProgressState.SelectRegistrationTime;
                    break;

                case ProgressState.SelectRegistrationTime:
                    _crawlerRegistry.RunNextStep(
                        () =>
                            _crawlerRegistry.SelectRegistrationTime(
                                CurrentClientDataRow));
                    _progressState = ProgressState.Start;
                    //todo bbenetskyy dataRow.Status!!!!!!!!
                    break;

                default:
                    _logger.Error(
                        $"Incorrect State = {_progressState} for CrawlerWorkFirstPart");
                    break;
            }

            _logger.Trace($"End CrawlerWorkSecondPart. _state={_progressState}");
        }

        private void CrawlerRefreshEngine()
        {
            _logger.Trace(
                $"Start CrawlerRefreshEngine _state = {_progressState}");
            var counter = 0;
            do
            {
                if (_crawlerRegistry != null && _crawlerRegistry.Error)
                {
                    if (_crawlerRegistry.IsServerDown)
                    {
                        _logger.Warn($"Reload page. _state = {_progressState}");
                        ShowAlert(
                            ResManager.GetString(ResKeys.WebPage_WillBeReloaded),
                            true);
                        _crawlerRegistry.ReloadPage();
                    }
                }
                if (_crawlerRegistry != null)
                    _crawlerRegistry.Error = false;

                CrawlerWorkSecondPart();

                if (_crawlerRegistry == null)
                    throw new NotImplementedException("CrawlerRefreshEngine => _crawlerRegistry == null");

                if (_crawlerRegistry.Canceled
                    || !_crawlerRegistry.Error)
                    continue;

                _logger.Warn(
                    $"!_crawlerRegistry.Canceled && _crawlerRegistry.Error. _state = {_progressState}."
                    + $" counter = {counter} _crawlerRegistry.ValidationError = {_crawlerRegistry.ValidationError}");
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (_progressState)
                {
                    case ProgressState.CheckDate:
                        break;

                    case ProgressState.BackToCityAndReason:
                        _progressState = ProgressState.BackToCityAndReason;
                        //todo need check no _progressState changes
                        break;

                    default:
                        _progressState--;
                        break;
                }
                counter++;

                if (!_crawlerRegistry.ValidationError)
                    continue;

                //we should show that error and finish the process of registration
                counter = RefreshCount;
                _progressState = ProgressState.BreakState;
                _crawlerRegistry.ValidationError = false;
            } while (counter < RefreshCount
                     && _crawlerRegistry.Error
                     && SetupManager.GetOptions()
                         .RepeatIfCrash);
            _logger.Trace($"End CrawlerRefreshEngine. _state={_progressState}");
        }

        /// <summary>
        ///     Close Browsers for all Crawler workers
        /// </summary>
        /// <param name="forceClose">Should it be closed without looking for toggleSwitchCloseBrowser flag? </param>
        private void CloseBrowsers(bool forceClose)
        {
            if (!forceClose
                && !SetupManager.GetOptions()
                    .CloseBrowser)
                return;
            _crawlerRegistry?.CloseBrowser();
        }

        private void InitOtherComponentDetails()
        {
            _logger.Trace("Start InitOtherComponentDetails.");

            _progressState = ProgressState.Start;

            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_progressState}.");

            _progressState = ProgressState.Start;

            _logger.Trace("End SetDefaultState.");
        }

        /// <summary>
        ///     Show Alert Message about User needed action
        /// </summary>
        private void ShowAlert(string message,
            bool title)
        {
            _logger.Info($"ShowAlert with message = {message}. title = {title}");
            ShowAlertMessagEventHandler?.Invoke(null, new ShowAlertMessageEventArgs(message, title, CurrentClientDataRow.NumberOfReceipt));
        }

        public void CancelRegistration()
        {
            if (_crawlerRegistry != null)
            {
                _crawlerRegistry.Canceled = true;
                _crawlerRegistry.Error = false;
            }
            CloseBrowsers(false);
        }

        #endregion Functions
    }
}