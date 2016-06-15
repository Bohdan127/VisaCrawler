using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Visa.Database;
using Visa.Resources;
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

        private int c = 0;
        #endregion Members

        #region CTOR

        public MainForm()
        {
            InitializeComponent();
            InitOtherComponentDetails();
        }

        #endregion CTOR

        #region Events

        private void _alertControl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2, (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender, AlertClickEventArgs e)
        {
            StartNewWorkRound();
            buttonShow.Enabled = false;
            e.AlertForm.Close();
        }

        private void _alertControl_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            e.Buttons.PinButton.SetDown(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lookUpEditServiceCenter.Properties.DataSource =
                InstanceProvider.DataSet.ServiceCenter.ToList();
            lookUpEditVisaCategory.Properties.DataSource =
                InstanceProvider.DataSet.VisaCategory.ToList();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                StartNewWorkRound();
                buttonShow.Enabled = false;
            }
            else
            {
                XtraMessageBox.Show("Будь-ласка випевніть візовий ценрт і візову категорію",
                    "Валідація не пройдена",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
        }

        private void progressBarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
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
                    return;
                Invoke(new Action(() =>
                {
                    progressBarControl1.EditValue = i;
                }));
                Thread.Sleep(150);
            }

            _initVal = maxVal;
            if (maxVal == 100)
            {
                SetDefaultState();
            }
        }

        private void _crawlerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var incomingData = e.Argument as Tuple<int, int>;

            if (incomingData == null) return;

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
                        XtraMessageBox.Show(_crawler.OutData,
                            "Результати пошуку",
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
                ShowAlert("Сервер не відповідає. Щоб поторити спробу запустіть перевірку знову!");
                Thread.Sleep(500);
                SetDefaultState();
                _crawler.CloseBrowser();
            }
            else
                ShowAlert("Випевніть капчу і натисніть ТУТ.");
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            _crawler.CloseBrowser();
        }

        #endregion Events

        #region Functions


        private void InitOtherComponentDetails()
        {
            ResManager.RegisterResource("uk_UA", new UkUaManager());
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
        }

        private void SetDefaultState()
        {
            _state = 1;
            _initVal = 1;
            Invoke(new Action(() =>
            {
                buttonShow.Enabled = true;
            }));
        }

        /// <summary>
        /// Show Alert Message about User needed action 
        /// </summary>
        private void ShowAlert(string message)
        {
            Invoke(new Action(() =>
            {
                _alertControl.Show(null, "", message); //ResManager.GetString("FillCaptchaAndPress"));
            }));
        }

        private bool ValidateControls()
        {
            var bRes = true;

            bRes &= lookUpEditServiceCenter.GetSelectedDataRow() != null;
            bRes &= lookUpEditVisaCategory.GetSelectedDataRow() != null;

            return bRes;
        }

        private void StartNewWorkRound()
        {
            if (!_progressBarWorker.IsBusy)
                _progressBarWorker.RunWorkerAsync();

            if (!_crawlerWorker.IsBusy)
                _crawlerWorker.RunWorkerAsync(new Tuple<int, int>(
                    ((VisaDataSet.ServiceCenterRow)lookUpEditServiceCenter.GetSelectedDataRow()).Value,
                    ((VisaDataSet.VisaCategoryRow)lookUpEditVisaCategory.GetSelectedDataRow()).Value));
        }

        #endregion Functions
    }
}