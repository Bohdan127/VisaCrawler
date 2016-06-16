using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Visa.Database;
using Visa.Resources;
using Visa.Resources.uk_UA;
using Visa.WebCrawler.SeleniumCrawler;

namespace Visa.WinForms
{
    public partial class MainForm : Form
    {
        #region Members

        private GetFirtAvailableData _crawler;

        /// <summary>
        /// Thread for crawler logic
        /// </summary>
        private BackgroundWorker _crawlerWorker;

        /// <summary>
        /// Thread for ProgressBar logic
        /// </summary>
        private BackgroundWorker _progressBarWorker;

        /// <summary>
        /// Field used for getting part of program
        /// </summary>
        private int _state = 1;

        /// <summary>
        /// Field used for showing progress in ProgressBar
        /// </summary>
        private int _initVal = 1;

        private AlertControl _alertControl;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion Members

        #region CTOR

        public MainForm()
        {
            _logger.Trace("Start MainForm CTOR");
            InitializeComponent();
            InitOtherComponentDetails();
            _logger.Trace("End MainForm CTOR");
        }

        #endregion CTOR

        #region Events

        private void _alertControl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            _logger.Trace($"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}; Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2};");
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2, (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender, AlertClickEventArgs e)
        {
            _logger.Info($"Start _alertControl_AlertClick. Alert Text - {e.AlertForm.Text}. State - {_state}");
            StartNewWorkRound();
            buttonShow.Enabled = false;
            e.AlertForm.Close();
            _logger.Trace($"End _alertControl_AlertClick. buttonShow.Enabled = {buttonShow.Enabled}. e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}");
        }

        private void _alertControl_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            e.Buttons.PinButton.SetDown(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _logger.Trace($"Start MainForm_Load");
            lookUpEditServiceCenter.Properties.DataSource =
                InstanceProvider.DataSet.ServiceCenter.ToList();
            lookUpEditVisaCategory.Properties.DataSource =
                InstanceProvider.DataSet.VisaCategory.ToList();
            _logger.Trace($"End MainForm_Load");
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            _logger.Trace($"Start buttonShow_Click");
            if (ValidateControls())
            {
                _logger.Info("Validation Pass.");
                StartNewWorkRound();
                buttonShow.Enabled = false;
            }
            else
            {
                _logger.Warn($"Validation Failed. Error Message is shown.");
                XtraMessageBox.Show(ResManager.GetString(ResKeys.ValidationError_Message),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            _logger.Trace($"End buttonShow_Click. buttonShow.Enabled = {buttonShow.Enabled}");
        }

        private void progressBarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.Trace($"Start progressBarWorker_DoWork. State = {_state}. InitValue = {_initVal}");
            var maxVal = 0;
            switch (_state)
            {
                case 1:
                    maxVal = 30;
                    break;

                case 2:
                    maxVal = 60;
                    break;

                case 3:
                    maxVal = 100;
                    break;

                default:
                    break;
            }

            for (var i = _initVal; i <= maxVal; i++)
            {
                if (_crawler.Error)
                {
                    _logger.Warn("return progressBarWorker_DoWork. _crawler.Error = true");
                    return;
                }
                Invoke(new Action(() =>
                {
                    progressBarControl1.EditValue = i;
                }));
                Thread.Sleep(150);
            }

            _initVal = maxVal;
            if (maxVal == 100)
            {
                _logger.Info("progressBarWorker_DoWork. maxVal == 100");
                SetDefaultState();
            }
            _logger.Info($"End progressBarWorker_DoWork. State = {_state}. InitValue = {_initVal}");

        }

        private void _crawlerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_state}");
            var incomingData = e.Argument as Tuple<int, int>;

            if (incomingData == null)
            {
                _logger.Error("return _crawlerWorker_DoWork incomingData == null");
                return;
            }

            switch (_state)
            {
                case 1:
                    Invoke(new Action(() =>
                    {
                        _alertControl.AlertFormList.ForEach(alert => alert.Close());
                    }));
                    _crawler = new GetFirtAvailableData();
                    _crawler.PartOne();
                    _state = 2;
                    break;

                case 2:
                    _crawler.PartTwo(incomingData.Item1, incomingData.Item2);
                    _state = 3;
                    break;

                case 3:
                    _crawler.PartThree();
                    if (!_crawler.Error)
                    {
                        _logger.Info($"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawler.OutData}. _crawler.Error = false ");
                        XtraMessageBox.Show(_crawler.OutData,
                            ResManager.GetString(ResKeys.SearchResult),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;
                default:
                    break;
            }
            if (_crawler.Error)
            {
                _logger.Warn($"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawler.OutData}. _crawler.Error = true ");
                ShowAlert(ResManager.GetString(ResKeys.ServerError));
                Thread.Sleep(500);
                SetDefaultState();
                _crawler.CloseBrowser();
            }
            else
                ShowAlert(ResManager.GetString(ResKeys.FillCaptchaAndPress));
            _logger.Trace($"End _crawlerWorker_DoWork. State = {_state}. _crawler.Error = {_crawler.Error} ");
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_state}.");
            _crawler.CloseBrowser();
        }

        #endregion Events

        #region Functions


        private void InitOtherComponentDetails()
        {
            _logger.Trace($"Start InitOtherComponentDetails.");
            ResManager.RegisterResource("uk_UA", uk_UA.ResourceManager);
            _logger.Trace("InitOtherComponentDetails. ResManager = uk_UA");
            Closed += MainForm_Closed;

            _progressBarWorker = new BackgroundWorker();
            _progressBarWorker.WorkerSupportsCancellation = true;
            _progressBarWorker.DoWork += progressBarWorker_DoWork;

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            _alertControl = new AlertControl();
            _alertControl.AlertClick += _alertControl_AlertClick;
            _alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
            _alertControl.FormLoad += _alertControl_FormLoad;
            _logger.Trace($"End InitOtherComponentDetails.");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}");
            _state = 1;
            _initVal = 1;
            Invoke(new Action(() =>
            {
                buttonShow.Enabled = true;
            }));
            _logger.Trace($"End SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}");
        }

        /// <summary>
        /// Show Alert Message about User needed action 
        /// </summary>
        private void ShowAlert(string message)
        {
            _logger.Info($"ShowAlert with message = {message}");
            Invoke(new Action(() =>
            {
                _alertControl.Show(null, "", message);
            }));
        }

        private bool ValidateControls()
        {
            var bRes = true;

            bRes &= lookUpEditServiceCenter.GetSelectedDataRow() != null;
            bRes &= lookUpEditVisaCategory.GetSelectedDataRow() != null;

            _logger.Info($"ValidateControls = {bRes}");
            return bRes;
        }

        private void StartNewWorkRound()
        {
            _logger.Trace("Start StartNewWorkRound");
            if (!_progressBarWorker.IsBusy)
            {
                _logger.Info("Start _progressBarWorker.RunWorkerAsync");
                _progressBarWorker.RunWorkerAsync();
            }

            if (!_crawlerWorker.IsBusy)
            {
                _logger.Info("Start _crawlerWorker.RunWorkerAsync");
                _crawlerWorker.RunWorkerAsync(new Tuple<int, int>(
                    ((VisaDataSet.ServiceCenterRow)lookUpEditServiceCenter.GetSelectedDataRow()).Value,
                    ((VisaDataSet.VisaCategoryRow)lookUpEditVisaCategory.GetSelectedDataRow()).Value));
            }
            _logger.Trace("End StartNewWorkRound");
        }

        #endregion Functions
    }
}