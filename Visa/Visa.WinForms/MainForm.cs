using DevExpress.XtraBars.Alerter;
using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Visa.Database;
using Visa.Database.Enums;
using Visa.Resources;
using Visa.Resources.uk_UA;

namespace Visa.WinForms
{
    public partial class MainForm : Form
    {
        #region Members

        private AlertControl _alertControl;

        private bool? isFirstPart;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Field used for getting part of program
        /// </summary>
        private int _state = 1;

        /// <summary>
        /// Field used for showing progress in ProgressBar
        /// </summary>
        private int _initVal = 1;

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
            StartNewWorkRoundFirst();
            buttonShow.Enabled = false;
            buttonRegistry.Enabled = false;
            e.AlertForm.Close();
            _logger.Trace($"End _alertControl_AlertClick. buttonShow.Enabled = {buttonShow.Enabled}. e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}.  buttonRegistry.Enabled ={ buttonRegistry.Enabled }");
        }

        private void _alertControl_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            e.Buttons.PinButton.SetDown(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _logger.Trace("Start MainForm_Load");
            SetDataSourceForLookUps();
            _logger.Trace("End MainForm_Load");
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_state}.");
            _crawler?.CloseBrowser();
        }

        private void _crawlerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_state}");

            if (isFirstPart == null)
            {
                _logger.Error($"_crawlerWorker_DoWork isFirstPart == null. State = {_state}. _crawler.Error = {_crawler.Error}");
                return;//todo enable search button
            }

            if (isFirstPart.Value)
                CrawlerWorkFirstPart(e.Argument);
            else
                //CrawlerWorkSecondPart
                CrawlerWorkFirstPart(e.Argument);

            _logger.Trace($"End _crawlerWorker_DoWork. State = {_state}. _crawler.Error = {_crawler.Error}");
        }

        #endregion Events

        #region Functions

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            var timeStart = DateTime.Now;
            lookUpEditServiceCenter.Properties.DataSource =
                           InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.ServiceCenter).ToList();
            lookUpEditVisaCategory.Properties.DataSource =
                InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.VisaCategory).ToList();
            repositoryItemLookUpEditNationality.DataSource =
                InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.Country).ToList();
            repositoryItemLookUpEditRegistryTime.DataSource =
                InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.RegistryTime).ToList();
            repositoryItemLookUpEditStatus.DataSource =
                InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.StatusType).ToList();
            _logger.Info($"Time for initialize datasets = {timeStart - DateTime.Now}");
            _logger.Trace("End SetDataSourceForLookUps.");
        }

        private void InitOtherComponentDetails()
        {
            _logger.Trace("Start InitOtherComponentDetails.");
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

            clientDataRowBindingSource.DataSource = InstanceProvider.DataSet.ClientData;
            repositoryItemTextEditPassword.PasswordChar = '*';
            isFirstPart = null;

            InitColumnNames();
            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void InitColumnNames()
        {
            _logger.Trace("Start InitColumnNames");
            colPeopleCount.Caption = ResManager.GetString(ResKeys.colPeopleCount);
            colChildsCount.Caption = ResManager.GetString(ResKeys.colChildsCount);
            colNumberOfReceipt.Caption = ResManager.GetString(ResKeys.colNumberOfReceipt);
            colEmail.Caption = ResManager.GetString(ResKeys.colEmail);
            colPassword.Caption = ResManager.GetString(ResKeys.colPassword);
            colEndPassportDate.Caption = ResManager.GetString(ResKeys.colEndPassportDate);
            colStatus.Caption = ResManager.GetString(ResKeys.colStatus);
            colName.Caption = ResManager.GetString(ResKeys.colName);
            colLastName.Caption = ResManager.GetString(ResKeys.colLastName);
            colBirthday.Caption = ResManager.GetString(ResKeys.colBirthday);
            colReturnData.Caption = ResManager.GetString(ResKeys.colReturnData);
            colNationality.Caption = ResManager.GetString(ResKeys.colNationality);
            colRegistryFom.Caption = ResManager.GetString(ResKeys.colRegistryFom);
            colRegistryTo.Caption = ResManager.GetString(ResKeys.colRegistryTo);
            colRegistryTime.Caption = ResManager.GetString(ResKeys.colRegistryTime);
            _logger.Trace("End InitColumnNames");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}.  buttonRegistry.Enabled = { buttonRegistry.Enabled }");
            _state = 1;
            _initVal = 1;
            isFirstPart = null;
            Invoke(new Action(() =>
            {
                buttonShow.Enabled = true;
                buttonRegistry.Enabled = true;
            }));
            _logger.Trace($"End SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}.  buttonRegistry.Enabled = { buttonRegistry.Enabled }");
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

        private void StartNewWorkRoundBase()
        {
            _logger.Trace("Start StartNewWorkRoundBase");

            if (isFirstPart == null)
            {
                _logger.Error($"StartNewWorkRoundBase isFirstPart == null. State = {_state}. _crawler.Error = {_crawler.Error}");
                return;//todo enable search button
            }

            if (!_progressBarWorker.IsBusy)
            {
                _logger.Info("Start _progressBarWorker.RunWorkerAsync");
                _progressBarWorker.RunWorkerAsync();
            }

            if (!_crawlerWorker.IsBusy)
            {
                _logger.Info("Start _crawlerWorker.RunWorkerAsync");
                if (isFirstPart.Value)
                    _crawlerWorker.RunWorkerAsync(new Tuple<int, int>(
                        Convert.ToInt32(((VisaDataSet.ChoiceRow)lookUpEditServiceCenter.GetSelectedDataRow()).Value),
                        Convert.ToInt32(((VisaDataSet.ChoiceRow)lookUpEditVisaCategory.GetSelectedDataRow()).Value)));
                else
                    _crawlerWorker.RunWorkerAsync(gridControl1.DataSource);
            }
            _logger.Trace("End StartNewWorkRoundBase");
        }

        #endregion Functions
    }
}