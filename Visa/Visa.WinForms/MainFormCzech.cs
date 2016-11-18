using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GlobalResources;
using GlobalResources.uk_UA;
using License.Logic;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.RegistrationModule.Helpers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.Database.Enums;
using Visa.WebCrawler.RegistrationModule;
using Visa.WinForms.Views;

namespace Visa.WinForms
{
    public partial class MainFormCzech : Form
    {
        #region CTOR

        public MainFormCzech()
        {
            _logger.Trace("Start MainForm CTOR");

            InitializeComponent();
            InstanceProvider.VisaCountry = "Czech";
            Closed += MainForm_Closed;
            Load += MainForm_Load;
            VisaRegistrationCzech.Canceled = false;

            _logger.Trace("End MainForm CTOR");
        }

        #endregion CTOR

        #region Members

        /// <summary>
        ///     Thread for crawler logic
        /// </summary>
        private BackgroundWorker _crawlerWorker;

        private Dictionary<AlertControl, VisaRegistrationCzech> _visaRegistrations;

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Field used for getting part of program
        /// </summary>
        private ProgressState _progressState = ProgressState.Start;

        #endregion Members

        #region Events

        private void _alertControl_BeforeFormShow(object sender,
           AlertFormEventArgs e)
        {
            if (e.AlertForm.AlertInfo.Text.IsBlank())
                return;
            _logger.Trace(
                $"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}, "
                + $" Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2}");
            e.Location =
                new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2,
                    (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender,
            AlertClickEventArgs e)
        {
            _logger.Info(
                $"Start _alertControl_AlertClick. Alert Text - {e.AlertForm.Text}. State - {_progressState}");

            GetVisaRegistration(sender).StartWork(null);
            e.AlertForm.Close();
            _logger.Trace(
                $"End _alertControl_AlertClick. e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}.");
        }

        /// <summary>
        /// Find VisaRegistration object from alert sender
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private VisaRegistrationCzech GetVisaRegistration(object sender)
        {
            var alertControl = _visaRegistrations.Keys.FirstOrDefault(
               alert => alert == sender as AlertControl);

            if (alertControl != null)
                return _visaRegistrations[alertControl];

            _logger.Error("_alertControl_AlertClick. alertControl was not founded in __visaRegistrations");
            throw new NotImplementedException(nameof(alertControl));
        }

        private void _alertControl_FormLoad(object sender,
            AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            if (e.AlertForm.AlertInfo.Text.IsNotBlank())
                e.Buttons.PinButton.SetDown(true);
            var visaReg = GetVisaRegistration(sender);
            e.AlertForm.AlertInfo.Caption =
                $"{visaReg.CurrentClientDataRow.Name} {visaReg.CurrentClientDataRow.LastName}\n{e.AlertForm.AlertInfo.Caption}";
        }

        private void GridView1_InvalidRowException(object sender,
            InvalidRowExceptionEventArgs e)
        {
            _logger.Trace("Start GridView1_InvalidRowException");
            e.ExceptionMode = ExceptionMode.NoAction;
            _logger.Trace("End GridView1_InvalidRowException");
        }

        private void GridView1_RowCountChanged(object sender, EventArgs e)
        {
            gridView1.OptionsView.NewItemRowPosition = gridView1.RowCount < 6
                ? NewItemRowPosition.Bottom
                : NewItemRowPosition.None;
        }

        private void GridView1_ValidateRow(object sender,
            ValidateRowEventArgs e)
        {
            _logger.Trace("Start GridView1_ValidateRow");
            var currRow =
                (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            if (currRow == null)
            {
                _logger.Info(
                    "End gridView1_CustomDrawRowIndicator. currRow == null");
                return;
            }

            currRow.ClearErrors();

            currRow.Email = currRow.LastName
                + currRow.Birthday.ToString("yy") // short Year
                + "@i.ua";
            currRow.PeopleCount = SetupManager.GetOptions().PeopleCount;
            currRow.ChildsCount = SetupManager.GetOptions().ChildCount;
            currRow.ReturnData = currRow.RegistryFom.AddYears(1);

            foreach (var column in currRow.Table.Columns.Cast<DataColumn>().
                Where(
                    column => currRow[column].ConvertToStringOrNull().IsBlank())
                )
            {
                _logger.Info(
                    $"Column {column.ColumnName} is empty for row with receipt = {currRow.NumberOfReceipt}");
                currRow.SetColumnError(column,
                    ResManager.GetString(
                        ResKeys.ValidationError_Message_NullText));
            }
            _logger.Trace("End GridView1_ValidateRow");
        }

        private void gridView1_CustomDrawRowIndicator(object sender,
            RowIndicatorCustomDrawEventArgs e)
        {
            /*
             *
             * Bohdan Benetskyi
             * this function write too much logs, we can hide all Trace logs in this function
             *
             */
            if (!e.Info.IsRowIndicator)
            {
                return;
            }
            var view = sender as GridView;
            var currRow =
                view?.GetDataRow(e.RowHandle) as VisaDataSet.ClientDataRow;
            if (currRow == null)
            {
                return;
            }

            switch (currRow.RegistryState)
            {
                case (byte)RegistryState.Added:
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[0],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Completed:
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[1],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Failed:
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[2],
                        e.Bounds);
                    break;

                default:
                    _logger.Error(
                        $"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt}"
                        + $" currRow.RegistryState = {currRow.RegistryState}");
                    break;
            }
            e.Handled = true;
        }

        private void gridControl1_KeyDown(object sender,
            KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (view.OptionsBehavior.Editable
                && e.KeyData == Keys.Delete)
            {
                _logger.Trace("Start gridControl1_KeyDown with Keys.Delete");
                view.DeleteSelectedRows();
                e.Handled = true;
                _logger.Info("gridControl1_KeyDown. SelectedRows was Deleted");
                _logger.Trace("End gridControl1_KeyDown with Keys.Delete");
            }
        }

        private void buttonShowSecond_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start buttonShowSecond_Click");

            //before validation we should disable validation and remove new row in grid
            SetReadOnly(true);

            if (ValidateControlsSecond())
            {
                _logger.Info("Validation Pass.");
                StartNewWorkRoundBase();
            }
            else
            {
                SetReadOnly(false);
                _logger.Warn("Validation Failed. Error Message is shown.");
                XtraMessageBox.Show(
                    ResManager.GetString(
                        ResKeys.ValidationError_Message_SecondPart),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            _logger.Trace(
                $"End buttonShowSecond_Click. buttonRegistry.Enabled = {registryButton.Enabled}");
        }

        /// <summary>
        ///     Open Import File Dialog
        /// </summary>
        private void importButtonClick(object sender,
            EventArgs e)
        {
            _logger.Info("Start barButtonItem2_ItemClick");

            if (gridView1.OptionsView.NewItemRowPosition
                != NewItemRowPosition.Bottom)
                return;//todo bbenetskyy 07.09.2016 no free time, we should write logs for all my changes in 07.09.2016

            gridView1.AddNewRow();
            gridView1.FocusedRowHandle = gridView1.RowCount;
            gridView1.Focus();
            var dataRow = (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            var importedRow = ImportManager.ImportRowsFromExcel();
            if (importedRow == null)
            {
                _logger.Warn("barButtonItem2_ItemClick importedRow == null");
                dataRow.Delete();
                return;
            }
            dataRow.Nationality =
                InstanceProvider.DataSet.Choice.FirstOrDefault(c =>
                    c.Type == (short)ChoicesType.Country
                    && Extentions.GetStringSimilarityInPercent(c.Name,
                        importedRow.Nationality,
                        false) > 80)?.Value;
            dataRow.VisaCity =
                InstanceProvider.DataSet.Choice.FirstOrDefault(c =>
                    c.Type == (short)ChoicesType.ServiceCenter
                    && Extentions.GetStringSimilarityInPercent(c.Name,
                        importedRow.VisaCity,
                        false) > 80)?.Value;
            dataRow.VisaType =
                InstanceProvider.DataSet.Choice.FirstOrDefault(c =>
                    c.Type == (short)ChoicesType.VisaCategory
                    && Extentions.GetStringSimilarityInPercent(c.Name,
                        importedRow.VisaType,
                        false) > 80)?.Value;
            dataRow.NumberOfReceipt = importedRow.NumberOfReceipt;
            dataRow.EndPassportDate = importedRow.EndPassportDate;
            dataRow.Status = importedRow.Status;
            dataRow.Name = importedRow.Name;
            dataRow.LastName = importedRow.LastName;
            dataRow.Birthday = importedRow.Birthday;
            dataRow.RegistryFom = importedRow.RegistryFom;
            dataRow.RegistryTo = importedRow.RegistryTo;
            gridView1.RefreshData();
            _logger.Info("End barButtonItem2_ItemClick");
        }

        /// <summary>
        ///     Open Setup form
        /// </summary>
        private void setupButtonClick(object sender,
            EventArgs e)
        {
            new SetupForm().ShowDialog();
        }

        private void simpleButtonCancel_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start simpleButtonCancelAction_Click");

            VisaRegistrationCzech.Canceled = true;
            foreach (var vr in _visaRegistrations)
            {
                vr.Value.CancelRegistration();
                Invoke(new Action(() => vr.Key.AlertFormList.ForEach(
                    alert => alert.Close())));
            }
            SetDefaultState();

            _logger.Trace("End simpleButtonCancelAction_Click");
        }

        private void MainForm_Load(object sender,
            EventArgs e)
        {
            _logger.Trace("Start MainForm_Load");
            SetReadOnly(false);
            ResManager.RegisterResource("uk_UA",
                uk_UA.ResourceManager);
            _logger.Info("InitOtherComponentDetails. ResManager = uk_UA");
            //we can off checking just for off line testing:
            CheckLicense();
            CheckForUpdates();
            InitOtherComponentDetails();
            SetDataSourceForLookUps();
            _logger.Trace("End MainForm_Load");
        }

        private async void CheckForUpdates()
        {
            if (!SetupManager.GetOptions()
                             .CheckForUpdates) return;

            var updateManager = new UpdateManager();
            var release = await updateManager.GetRelease();
            if (!updateManager.NeedUpdate(Assembly.GetEntryAssembly(), release)) return;

            if (SetupManager.GetOptions()
                            .AutoUpdates)
            {
            }
            else
            { }

        }

        private void MainForm_Closed(object sender,
            EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_progressState}.");
            if(_visaRegistrations != null)
                foreach (var vr in _visaRegistrations.Values)
                {
                    vr.CancelRegistration();
                }
        }

        private void _crawlerWorker_DoWork(object sender,
            DoWorkEventArgs e)
        {
            _logger.Trace("Start _crawlerWorker_DoWork.");

            VisaRegistrationCzech.Canceled = false;
            _visaRegistrations.Clear();
            var taskList = new List<Task>();
            for (var rowId = 0;
                rowId < gridView1.RowCount;
                rowId++)
            {
                /*
                 * bbenetskyy 07.09.2016
                 * we need to create new value instead of using global rowId,
                 * because it value will be calculate during execution 
                 */
                var id = rowId;
                var visaReg = new VisaRegistrationCzech();
                visaReg.ShowAlertMessagEventHandler += ShowAlert;
                taskList.Add(new Task(() => visaReg.StartWork(gridView1.GetDataRow(id) as VisaDataSet.ClientDataRow)));

                var alertControl = new AlertControl();
                alertControl.AlertClick += _alertControl_AlertClick;
                alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
                alertControl.FormLoad += _alertControl_FormLoad;
                _visaRegistrations.Add(alertControl, visaReg);
            }
            taskList.ForEach(task => task.Start());
            taskList.ForEach(task => task.Wait());

            _logger.Trace("End _crawlerWorker_DoWork.");
        }

        /// <summary>
        ///     Show Alert Message about User needed action
        /// </summary>
        private void ShowAlert(object sender,
            ShowAlertMessageEventArgs e)
        {
            _logger.Info($"ShowAlert with message = {e.Message}. title = {e.Title}. Receipt Number = {e.NumbetOfReceipt}");

            var alertControl =
                _visaRegistrations.FirstOrDefault(
                    vr =>
                        vr.Value.CurrentClientDataRow.NumberOfReceipt
                        == e.NumbetOfReceipt)
                    .Key;
            Invoke(new Action(() =>
            {
                if (e.Title)
                    alertControl.Show(null,
                        e.Message,
                        "");
                else
                    alertControl.Show(null,
                        "",
                        e.Message);
            }));
        }

        private void GridView1_InitNewRow(object sender,
            InitNewRowEventArgs e)
        {
            var newRow =
                (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            newRow.Nationality = SetupManager.GetOptions().Nationality;
            newRow.Password = SetupManager.GetOptions().Password;
            newRow.PeopleCount = SetupManager.GetOptions().PeopleCount;
            newRow.ChildsCount = SetupManager.GetOptions().ChildCount;
            gridControl1.Refresh();
        }

        #endregion Events

        #region Functions

        private bool ValidateControlsSecond()
        {
            var bRes = gridView1.RowCount > 0;
            bRes = bRes && !gridView1.HasColumnErrors;
            bRes = bRes && ValidateRegistryDates();
            if (bRes && SetupManager.GetOptions().Email.IsBlank())
            {
                XtraMessageBox.Show(ResManager.GetString(ResKeys.ValidationError_Message_Option_Email),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                bRes = false;
            }
            if (bRes && SetupManager.GetOptions().Password.IsBlank())
            {
                XtraMessageBox.Show(ResManager.GetString(ResKeys.ValidationError_Message_Option_Password),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                bRes = false;
            }

            _logger.Info($"ValidateControlsSecond = {bRes}");

            return bRes;
        }

        private bool ValidateRegistryDates()
        {
            var bRes = true;
            for (var i = 0;
                i < gridView1.RowCount;
                i++)
            {
                gridView1.FocusedRowHandle = i;
                var row = (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();

                if (row.RegistryFom <= row.RegistryTo)
                    continue;

                bRes = false;
                row.SetColumnError(ResManager.GetString(ResKeys.colRegistryFom_DataColumn_Name),
                    ResManager.GetString(ResKeys.colRegistryFom_ValidationError));
                row.SetColumnError(ResManager.GetString(ResKeys.colRegistryTo_DataColumn_Name),
                    ResManager.GetString(ResKeys.colRegistryTo_ValidationError));
            }
            return bRes;
        }

        private void CheckLicense()
        {
            _logger.Trace("Start CheckLicense");
            const string filePath = @".\Visa.key";
            var key = string.Empty;
            var licenseForm = new LicenseForm("uk_UA", uk_UA.ResourceManager);
            var start = DateTime.Now;
            try
            {
                key = File.ReadAllLines(filePath).FirstOrDefault() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception during opening license key");
                _logger.Error(ex.Message);
            }
            finally
            {
                if (!licenseForm.CheckInstance(key))
                {
                    _logger.Info($"Time for CheckLicense = {DateTime.Now - start}");
                    licenseForm.ShowDialog();
                }
                else
                {
                    _logger.Info($"Time for CheckLicense = {DateTime.Now - start}");
                }
                if (!licenseForm.IsRegistered
                    || licenseForm.LicenseKey.IsBlank())
                    Close();
            }
            if (licenseForm.LicenseKey == key)
                return;

            try
            {
                _logger.Info($"Save new license key = {licenseForm.LicenseKey}");
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.WriteAllText(filePath,
                    licenseForm.LicenseKey,
                    Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception during saving license key");
                _logger.Error(ex.Message);
            }
            _logger.Trace("End CheckLicense");
        }

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            var timeStart = DateTime.Now;

            repositoryItemLookUpEditVisaCity.DataSource = Enumerable.Where(InstanceProvider.DataSet.Choice,
                c => c.Type == (short)ChoicesType.ServiceCenter).ToList();

            repositoryItemLookUpEditVisaType.DataSource = Enumerable.Where(InstanceProvider.DataSet.Choice,
                c => c.Type == (short)ChoicesType.VisaCategory).ToList();

            repositoryItemLookUpEditNationality.DataSource = Enumerable.Where(InstanceProvider.DataSet.Choice,
                c => c.Type == (short)ChoicesType.Country).ToList();

            repositoryItemLookUpEditRegistryTime.DataSource = Enumerable.Where(InstanceProvider.DataSet.Choice,
                c => c.Type == (short)ChoicesType.RegistryTime).ToList();

            repositoryItemLookUpEditStatus.DataSource = Enumerable.Where(InstanceProvider.DataSet.Choice,
                c => c.Type == (short)ChoicesType.StatusType).ToList();

            _logger.Info($"Time for initialize datasets = {DateTime.Now - timeStart}");
            _logger.Trace("End SetDataSourceForLookUps.");
        }

        private void InitOtherComponentDetails()
        {
            _logger.Trace("Start InitOtherComponentDetails.");
            var timeStart = DateTime.Now;

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            _visaRegistrations =
                new Dictionary<AlertControl, VisaRegistrationCzech>();

            clientDataRowBindingSource.DataSource =
                InstanceProvider.DataSet.ClientData;
            repositoryItemTextEditPassword.PasswordChar = '*';

            _progressState = ProgressState.GoToUrl;

            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.RowCountChanged += GridView1_RowCountChanged;
            gridView1.BestFitColumns();

            InitColumnNames();
            InitFieldNames();
            InitRepositoryNames();

            _logger.Info(
                $"Time for InitOtherComponentDetails = {DateTime.Now - timeStart}");
            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void InitRepositoryNames()
        {
            _logger.Trace("Start InitRepositoryNames");
            repositoryItemLookUpEditStatus.NullText = ResManager.GetString(ResKeys.Status_NullText);
            repositoryItemLookUpEditNationality.NullText = ResManager.GetString(ResKeys.Nationality_NullText);
            repositoryItemLookUpEditRegistryTime.NullText = ResManager.GetString(ResKeys.RegistryTime_NullText);
            repositoryItemLookUpEditVisaType.NullText = ResManager.GetString(ResKeys.VisaCategory_NullText);
            repositoryItemLookUpEditVisaCity.NullText = ResManager.GetString(ResKeys.ServiceCenter_NullText);
            _logger.Trace("End InitRepositoryNames");
        }

        private void InitFieldNames()
        {
            _logger.Trace("Start InitFieldNames");
            layoutControlGroupClientData.Text = ResManager.GetString(ResKeys.lblClientDataGroup);
            registryButton.Text = ResManager.GetString(ResKeys.ButtonRegistry_Text);
            cancelButton.Text = ResManager.GetString(ResKeys.ButtonCancelAction_Text);
            importButton.Text = ResManager.GetString(ResKeys.BarButtonItemImport_Caption);
            setupButton.Text = ResManager.GetString(ResKeys.BarButtonItemSetup_Caption);
            _logger.Trace("End InitFieldNames");
        }

        private void InitColumnNames()
        {
            _logger.Trace("Start InitColumnNames");

            colVisaType.Caption = ResManager.GetString(ResKeys.lblVisaCategory);
            lViewColVisaType.Caption = ResManager.GetString(ResKeys.lblVisaCategory);

            colVisaCity.Caption = ResManager.GetString(ResKeys.lblServiceCenter);
            lViewColVisaCity.Caption = ResManager.GetString(ResKeys.lblServiceCenter);

            colPeopleCount.Caption = ResManager.GetString(ResKeys.colPeopleCount);
            lViewColPeopleCount.Caption = ResManager.GetString(ResKeys.colPeopleCount);

            colChildsCount.Caption = ResManager.GetString(ResKeys.colChildsCount);
            lViewColChildsCount.Caption = ResManager.GetString(ResKeys.colChildsCount);

            colNumberOfReceipt.Caption = ResManager.GetString(ResKeys.colNumberOfReceipt);
            lViewColNumberOfReceipt.Caption = ResManager.GetString(ResKeys.colNumberOfReceipt);

            colEndPassportDate.Caption = ResManager.GetString(ResKeys.colEndPassportDate);
            lViewColEndPassportDate.Caption = ResManager.GetString(ResKeys.colEndPassportDate);

            colStatus.Caption = ResManager.GetString(ResKeys.colStatus);
            lViewColStatus.Caption = ResManager.GetString(ResKeys.colStatus);

            colName.Caption = ResManager.GetString(ResKeys.colName);
            lViewColName.Caption = ResManager.GetString(ResKeys.colName);

            colLastName.Caption = ResManager.GetString(ResKeys.colLastName);
            lViewColLastName.Caption = ResManager.GetString(ResKeys.colLastName);

            colBirthday.Caption = ResManager.GetString(ResKeys.colBirthday);
            lViewColBirthday.Caption = ResManager.GetString(ResKeys.colBirthday);

            colReturnData.Caption = ResManager.GetString(ResKeys.colReturnData);
            lViewColReturnData.Caption = ResManager.GetString(ResKeys.colReturnData);

            colNationality.Caption = ResManager.GetString(ResKeys.Nationality_Text);
            lViewColNationality.Caption = ResManager.GetString(ResKeys.Nationality_Text);

            colRegistryFom.Caption = ResManager.GetString(ResKeys.colRegistryFom);
            lViewColRegistryFom.Caption = ResManager.GetString(ResKeys.colRegistryFom);

            colRegistryTo.Caption = ResManager.GetString(ResKeys.colRegistryTo);
            lViewColRegistryTo.Caption = ResManager.GetString(ResKeys.colRegistryTo);

            _logger.Trace("End InitColumnNames");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_progressState}."
                + $"  buttonRegistry.Enabled = {registryButton.Enabled}."
                + $" buttonCancelAction.Enabled = {cancelButton.Enabled}.");
            _progressState = ProgressState.Start;
            SetReadOnly(false);
            _logger.Trace($"End SetDefaultState. State = {_progressState}."
                + $" buttonRegistry.Enabled = {registryButton.Enabled}."
                + $" buttonCancelAction.Enabled = {cancelButton.Enabled}.");
        }

        private void SetReadOnly(bool readOnly)
        {
            _logger.Trace($"Makes all controls ReadOnly => {readOnly}");
            Invoke(new Action(() =>
            {
                registryButton.Enabled = !readOnly;
                gridView1.OptionsBehavior.Editable = !readOnly;
                gridView1.OptionsView.NewItemRowPosition = !gridView1.OptionsBehavior.Editable
                    ? NewItemRowPosition.None
                    : NewItemRowPosition.Bottom;
                cancelButton.Enabled = readOnly;
            }));
        }

        private void StartNewWorkRoundBase()
        {
            _logger.Trace("Start StartNewWorkRoundBase");

            if (!_crawlerWorker.IsBusy)
            {
                _logger.Info("Start _crawlerWorker.RunWorkerAsync");
                _crawlerWorker.RunWorkerAsync();
            }
            else
            {
                XtraMessageBox.Show(ResManager.GetString(ResKeys.Worker_IsBusy),
                    ResManager.GetString(ResKeys.lblCancelGroup),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                _logger.Warn("_crawlerWorker.IsBusy = true");
                SetReadOnly(false);
            }
            _logger.Trace("End StartNewWorkRoundBase");
        }

        #endregion Functions
    }
}

/*
 * todo bbenetskyy 07.09.2016
 * We need to add receipt validation
 * in multi thread program using
 * for be sure that same receipts 
 * are not used
 */
