using DevExpress.XtraBars.Alerter;
using DevExpress.XtraGrid.Views.Grid;
using License.Logic;
using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolsPortable;
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

        private void simpleButtonCancelAction_Click(object sender, EventArgs e)
        {
            _logger.Trace("Start simpleButtonCancelAction_Click");
            if (_crawler != null)
                _crawler.Canceled = true;
            if (_crawlerRegistry != null)
                _crawlerRegistry.Canceled = true;
            _crawler?.CloseBrowser();
            _crawlerRegistry?.CloseBrowser();
            _logger.Trace("End simpleButtonCancelAction_Click");
        }

        private void _alertControl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            _logger.Trace($"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}; Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2};");
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2, (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender, AlertClickEventArgs e)
        {
            _logger.Info($"Start _alertControl_AlertClick. Alert Text - {e.AlertForm.Text}. State - {_state}");
            StartNewWorkRoundBase();
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
            CheckLicense();
            SetDataSourceForLookUps();
            SetReadOnly(false);
            _logger.Trace("End MainForm_Load");
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_state}.");
            CloseBrowsers(true);
        }

        private void _crawlerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_state}");

            if (isFirstPart == null)
            {
                _logger.Error($"_crawlerWorker_DoWork isFirstPart == null. State = {_state}. _crawler.Error = {_crawler.Error}. _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
                return;
            }

            if (isFirstPart.Value)
                CrawlerWorkFirstPart(e.Argument);
            else
            {
                CrawlerWorkSecondPart(e.Argument ?? gridView1.GetDataRow(0));//todo later here should be changed for collecting all rows instead first one like now
            }

            _logger.Trace($"End _crawlerWorker_DoWork. State = {_state}. _crawler.Error = {_crawler?.Error}. _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
        }

        #endregion Events

        #region Functions

        private void CheckLicense()
        {
            const string filePath = @".\Visa.key";
            var key = string.Empty;
            var licenseForm = new LicenseForm();
            try
            {
                key = File.ReadAllLines(filePath).FirstOrDefault() ?? string.Empty;
            }
            finally
            {
                if (!licenseForm.CheckInstance(key))
                    licenseForm.ShowDialog();
                if (!licenseForm.IsRegistered)
                    Close();
            }
            if (licenseForm.LicenseKey == key) return;

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.WriteAllText(filePath, licenseForm.LicenseKey, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception during saving license key");
            }
        }

        /// <summary>
        /// Close Browsers for all Crawler workers
        /// </summary>
        /// <param name="forceClose">Should it be closed without looking for toggleSwitchCloseBrowser flag? </param>
        protected virtual void CloseBrowsers(bool forceClose)
        {
            if (!forceClose && !toggleSwitchCloseBrowser.EditValue.ConvertToBool())
                return;
            _crawler?.CloseBrowser();
            _crawlerRegistry?.CloseBrowser();
        }

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            var timeStart = DateTime.Now;

            var servCenters = InstanceProvider.DataSet.Choice.
                Where(c => c.Type == (short)ChoicesType.ServiceCenter).ToList(); ;

            lookUpEditServiceCenter.Properties.DataSource = servCenters;

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
            var timeStart = DateTime.Now;
            ResManager.RegisterResource("uk_UA", uk_UA.ResourceManager);
            _logger.Info("InitOtherComponentDetails. ResManager = uk_UA");
            Closed += MainForm_Closed;
            Load += MainForm_Load;

            buttonShow.Click += buttonShow_Click;
            buttonRegistry.Click += buttonShowSecond_Click;

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            _alertControl = new AlertControl();
            _alertControl.AlertClick += _alertControl_AlertClick;
            _alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
            _alertControl.FormLoad += _alertControl_FormLoad;

            clientDataRowBindingSource.DataSource = InstanceProvider.DataSet.ClientData;
            repositoryItemTextEditPassword.PasswordChar = '*';
            isFirstPart = null;
            toggleSwitchCloseBrowser.EditValue = true;

            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.BestFitColumns();

            InitColumnNames();
            InitFieldNames();
            InitRepositoryNames();
            _logger.Info($"Time for InitOtherComponentDetails = {timeStart - DateTime.Now}");
            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void InitRepositoryNames()
        {
            _logger.Trace("Start InitRepositoryNames");
            repositoryItemLookUpEditStatus.NullText = ResManager.GetString(ResKeys.Status_NullText);
            repositoryItemLookUpEditNationality.NullText = ResManager.GetString(ResKeys.Nationality_NullText);
            repositoryItemLookUpEditRegistryTime.NullText = ResManager.GetString(ResKeys.RegistryTime_NullText);
            _logger.Trace("End InitRepositoryNames");
        }

        private void InitFieldNames()
        {
            _logger.Trace("Start InitFieldNames");
            lookUpEditVisaCategory.Properties.NullText = ResManager.GetString(ResKeys.VisaCategory_NullText);
            lookUpEditServiceCenter.Properties.NullText = ResManager.GetString(ResKeys.ServiceCenter_NullText);
            buttonRegistry.Text = ResManager.GetString(ResKeys.ButtonRegistry_Text);
            buttonShow.Text = ResManager.GetString(ResKeys.ButtonShow_Text);
            layoutControlGroupFirst.Text = ResManager.GetString(ResKeys.lblFirstAvailableGroup);
            layoutControlGroupSecond.Text = ResManager.GetString(ResKeys.lblClientRegostrationGroup);
            layoutControlGroupClientData.Text = ResManager.GetString(ResKeys.lblClientDataGroup);
            layoutControlItemVisaCategory.Text = ResManager.GetString(ResKeys.lblVisaCategory);
            layoutControlGroupCancel.Text = ResManager.GetString(ResKeys.lblCancelGroup);
            layoutControlItemCloseBrower.Text = ResManager.GetString(ResKeys.lblCloseBrowser);
            buttonCancelAction.Text = ResManager.GetString(ResKeys.ButtonCancelAction_Text);
            toggleSwitchCloseBrowser.Properties.OnText = ResManager.GetString(ResKeys.ToggleSwitch_OnText);
            toggleSwitchCloseBrowser.Properties.OffText = ResManager.GetString(ResKeys.ToggleSwitch_OffText);
            _logger.Trace("End InitFieldNames");
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
            _logger.Trace("End InitColumnNames");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}.  buttonRegistry.Enabled = { buttonRegistry.Enabled }. buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
            _state = 1;
            _initVal = 1;
            isFirstPart = null;
            SetReadOnly(false);
            _logger.Trace($"End SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonShow.Enabled = {buttonShow.Enabled}.  buttonRegistry.Enabled = { buttonRegistry.Enabled }. buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
        }

        private void SetReadOnly(bool readOnly)
        {
            _logger.Trace($"Makes all controls ReadOnly => {readOnly}");
            Invoke(new Action(() =>
            {
                buttonShow.Enabled = !readOnly;
                buttonRegistry.Enabled = !readOnly;
                gridView1.OptionsBehavior.Editable = !readOnly;
                gridView1.OptionsView.NewItemRowPosition = !gridView1.OptionsBehavior.Editable
                    ? NewItemRowPosition.None
                    : NewItemRowPosition.Bottom;
                buttonCancelAction.Enabled = readOnly;
            }));
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
                _logger.Error($"StartNewWorkRoundBase isFirstPart == null. State = {_state}. _crawler.Error = {_crawler?.Error}. _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
                return;
            }

            SetReadOnly(true);

            if (!_crawlerWorker.IsBusy)
            {
                _logger.Info("Start _crawlerWorker.RunWorkerAsync");
                if (isFirstPart.Value)
                    _crawlerWorker.RunWorkerAsync(new Tuple<int, int>(
                        Convert.ToInt32(lookUpEditServiceCenter.EditValue),
                        Convert.ToInt32(lookUpEditVisaCategory.EditValue)));
                else
                    _crawlerWorker.RunWorkerAsync();
            }
            else
            {
                _logger.Warn("_crawlerWorker.IsBusy = true");
            }
            _logger.Trace("End StartNewWorkRoundBase");
        }

        #endregion Functions
    }
}