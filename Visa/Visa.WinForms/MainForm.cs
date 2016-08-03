using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NLog;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.Database;
using Visa.Database.Enums;
using Visa.License.Logic;
using Visa.Resources;
using Visa.Resources.uk_UA;
using Visa.WebCrawler.SeleniumCrawler;
using Visa.WinForms.Views;

namespace Visa.WinForms
{
    public partial class MainForm : Form
    {
        #region CTOR

        public MainForm()
        {
            _logger.Trace("Start MainForm CTOR");
            InitializeComponent();
            InitOtherComponentDetails();

            _logger.Trace("End MainForm CTOR");
        }

        #endregion CTOR

        #region Members

        private StateManager _stateManager;

        private RegisterUser _crawlerRegistry;

        private AlertControl _alertControl;

        /// <summary>
        ///     Thread for crawler logic
        /// </summary>
        private BackgroundWorker _crawlerWorker;

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Field used for getting part of program
        /// </summary>
        private int _state = 1;

        /// <summary>
        ///     Field used for showing progress in ProgressBar
        /// </summary>
        private int _initVal = 1;

        public bool ShowCaptchaMessage => _state == 6 || _state == 1;

        #endregion Members

        #region Events

        private void GridView1_InvalidRowException(object sender,
            InvalidRowExceptionEventArgs e)
        {
            _logger.Trace("Start GridView1_InvalidRowException");
            e.ExceptionMode = ExceptionMode.NoAction;
            _logger.Trace("End GridView1_InvalidRowException");
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

            try
            {
                currRow.Email = currRow.LastName
                                + currRow.Birthday.Year.ToString().Remove(0,
                                    2) + "@i.ua";
                currRow.PeopleCount = SetupManager.GetOptions().PeopleCount;
                currRow.ChildsCount = SetupManager.GetOptions().ChildCount;
                currRow.ReturnData = currRow.RegistryFom.AddYears(1);
            }
            catch
            {
                // ignored
            }

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
            //_logger.Trace("Start gridView1_CustomDrawRowIndicator");
            if (!e.Info.IsRowIndicator)
            {
                //_logger.Trace("End gridView1_CustomDrawRowIndicator. e.Info.IsRowIndicator == false");
                return;
            }
            var view = sender as GridView;
            var currRow =
                view?.GetDataRow(e.RowHandle) as VisaDataSet.ClientDataRow;
            if (currRow == null)
            {
                //_logger.Trace("End gridView1_CustomDrawRowIndicator. currRow == null");
                return;
            }

            switch (currRow.RegistryState)
            {
                case (byte)RegistryState.Added:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[0],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Completed:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[1],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Failed:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[2],
                        e.Bounds);
                    break;

                default:
                    _logger.Error(
                        $"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    break;
            }
            e.Handled = true;
            //_logger.Trace("End gridView1_CustomDrawRowIndicator");
        }

        private void buttonShowSecond_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start buttonShowSecond_Click");
            if (ValidateControlsSecond())
            {
                    _logger.Info("Validation Pass.");
                    StartNewWorkRoundSecond();
            }
            else
            {
                _logger.Warn("Validation Failed. Error Message is shown.");
                XtraMessageBox.Show(
                    ResManager.GetString(
                        ResKeys.ValidationError_Message_SecondPart),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            _logger.Trace(
                $"End buttonShowSecond_Click. buttonRegistry.Enabled = {buttonRegistry.Enabled}");
        }

        /// <summary>
        ///     Open Import File Dialog
        /// </summary>
        private void barButtonItem2_ItemClick(object sender,
            ItemClickEventArgs e)
        {
            _logger.Info("Start barButtonItem2_ItemClick");
            var dataRow =
                (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            if (dataRow == null)
            {
                gridView1.AddNewRow();
                dataRow =
                    (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            }
            var importedRow = ImportManager.ImportRowsFromExcel();
            if (importedRow == null)
            {
                _logger.Info("barButtonItem2_ItemClick importedRow == null");
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
        private void barButtonItem3_ItemClick(object sender,
            ItemClickEventArgs e)
        {
            new SetupForm().ShowDialog();
        }

        private void simpleButtonCancelAction_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start simpleButtonCancelAction_Click");
            if (_crawlerRegistry != null)
                _crawlerRegistry.Canceled = true;
            _crawlerRegistry?.CloseBrowser();
            _logger.Trace("End simpleButtonCancelAction_Click");
        }

        private void _alertControl_BeforeFormShow(object sender,
            AlertFormEventArgs e)
        {
            _logger.Trace(
                $"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}; Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2};");
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2,
                (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender,
            AlertClickEventArgs e)
        {
            _logger.Info(
                $"Start _alertControl_AlertClick. Alert Text - {e.AlertForm.Text}. State - {_state}");
            StartNewWorkRoundBase();
            e.AlertForm.Close();
            _logger.Trace(
                $"End _alertControl_AlertClick. e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}.  buttonRegistry.Enabled ={buttonRegistry.Enabled}");
        }

        private void _alertControl_FormLoad(object sender,
            AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            if (e.AlertForm.AlertInfo.Text.IsNotBlank())
                e.Buttons.PinButton.SetDown(true);
        }

        private void MainForm_Load(object sender,
            EventArgs e)
        {
            _logger.Trace("Start MainForm_Load");
            CheckLicense();
            SetDataSourceForLookUps();
            SetReadOnly(false);
            _logger.Trace("End MainForm_Load");
        }

        private void MainForm_Closed(object sender,
            EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_state}.");
            CloseBrowsers(true);
        }

        private bool _crawlerWorker_CheckSiteAvailability()
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
                    //while (!serverAvailable && !_crawlerRegistry.Canceled)
                    //{
                    //    int t = 2000;// t = 2s
                    //    _logger.Warn($"Site is Unavailable Again. Thread.Sleep {t/1000}s.");
                    //    Thread.Sleep(t);
                    //    serverAvailable = _stateManager.GetCurrentSiteAvailability();
                    //}
                }
            }
            if (serverAvailable)
            {
                ShowAlert(ResManager.GetString(ResKeys.PageAvailable), true);
                _logger.Info("Site is Available. Process is Starting.");
            }
            else
            {
                //_crawlerRegistry.Canceled = true;
                //Break = true;
            }
            return Break;
        }
        private void _crawlerWorker_DoWork(object sender,
        DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_state}");
            
            bool bBreak = _crawlerWorker_CheckSiteAvailability();
            do
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
                else if (_crawlerRegistry != null && _crawlerRegistry.Error) // if Error
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
                else if (_state == 7 && _crawlerRegistry != null)
                {
                    ShowAlert(_crawlerRegistry.OutData,
                        true);
                    bBreak = !SetupManager.GetOptions().RepeatIfCrash;
                }
                else if (_state == 6 || _state == 8)
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
            } while (!bBreak);

            _logger.Trace(
                $"End _crawlerWorker_DoWork. State = {_state}. _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
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

        private void CrawlerWorkSecondPart(object data)
        {
            _logger.Trace($"Start CrawlerWorkSecondPart _state = {_state}");
            var dataRow = data as VisaDataSet.ClientDataRow;

            if (dataRow == null)
            {
                _logger.Error("return _crawlerWorker_DoWork dataRow == null");
                return;
            }

            switch (_state)
            {
                case 1:     // alerts.Close(), and StartsAgain
                    Invoke(
                        new Action(
                            () =>
                            {
                                _alertControl.AlertFormList.ForEach(
                                    alert => alert.Close());
                            }));
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
                    _crawlerRegistry.SelectCityAndReason(dataRow);
                    _state = 5;
                    break;

                case 5:     // ProvidePeopleCount(dataRow)
                    _crawlerRegistry.ProvidePeopleCount(dataRow);
                    _state = 6;
                    break;

                case 6:     // SelectVisaTypeAndCheckForDate(dataRow)
                    var isAvailableDate =
                        _crawlerRegistry.SelectVisaTypeAndCheckForDate(dataRow);
                    _state = isAvailableDate
                        ? 8
                        : 7;
                    break;

                case 7:     // BackToCityAndReason()
                    _crawlerRegistry.BackToCityAndReason();
                    _state = 4;     // SelectCityAndReason(dataRow)
                    break;

                case 8:     // Receipt(dataRow)
                    _crawlerRegistry.Receipt(dataRow);
                    _state = 9;
                    break;

                case 9:     // ClientData(dataRow)
                    _crawlerRegistry.ClientData(dataRow);
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

            _logger.Trace($"End CrawlerWorkSecondPart. _state={_state}");
        }

        private void CrawlerRefreshEngine()
        {
            _logger.Trace($"Start CrawlerRefreshEngine _state = {_state}");
            var counter = 0;
            do
            {
                //_crawlerRegistry.Error = false;  // - я так розумію, що це все ж таки не тут а нижче
                CrawlerWorkSecondPart(gridView1.GetDataRow(0));
                if (!_crawlerRegistry.Canceled && _crawlerRegistry.Error)
                {
                    _logger.Warn($"!_crawlerRegistry.Canceled && _crawlerRegistry.Error. _state = {_state}. counter = {counter}");
                    var breakOut = false;
                    switch (_state)
                    {
                        case 7:     // BackToCityAndReason()
                        case 8:     // Receipt(dataRow)
                            _state = 4;     // SelectCityAndReason(dataRow)
                            _crawlerRegistry.Error = false;
                            break;
                        case 9:     // ClientData(dataRow)
                        case 1:     // alerts.Close(), and StartAgain
                            breakOut = true;
                            _crawlerRegistry.Error = false;
                            break;
                        default:
                            _state--;
                            break;
                    }
                    if (breakOut)
                        break;
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
            } while (counter < 5 && _crawlerRegistry.Error);
            _logger.Trace($"End CrawlerRefreshEngine. _state={_state}");
        }

        private bool ValidateControlsSecond()
        {
            var bRes = true;

            bRes &= !gridView1.HasColumnErrors;
            bRes &= ((BindingSource)gridControl1.DataSource).Count > 0;
            if (bRes)
                bRes &= ValidateRegistryDates();
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
                //todo log row with error
            }
            return bRes;
        }

        private void StartNewWorkRoundSecond()
        {
            _logger.Trace("Start StartNewWorkRoundSecond");

            StartNewWorkRoundBase();

            _logger.Trace("End StartNewWorkRoundSecond");
        }

        private void CheckLicense()
        {
            const string filePath = @".\Visa.key";
            var key = string.Empty;
            var licenseForm = new LicenseForm();
            try
            {
                key = File.ReadAllLines(filePath).FirstOrDefault()
                      ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception during opening license key");
            }
            finally
            {
                if (!licenseForm.CheckInstance(key))
                    licenseForm.ShowDialog();
                if (!licenseForm.IsRegistered
                    || licenseForm.LicenseKey.IsBlank())
                    Close();
            }
            if (licenseForm.LicenseKey == key)
                return;

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.WriteAllText(filePath,
                    licenseForm.LicenseKey,
                    Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception during saving license key");
            }
        }

        /// <summary>
        ///     Close Browsers for all Crawler workers
        /// </summary>
        /// <param name="forceClose">Should it be closed without looking for toggleSwitchCloseBrowser flag? </param>
        protected virtual void CloseBrowsers(bool forceClose)
        {
            if (!forceClose && !SetupManager.GetOptions().CloseBrowser)
                return;
            _crawlerRegistry?.CloseBrowser();
        }

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            var timeStart = DateTime.Now;

            repositoryItemLookUpEditVisaCity.DataSource =
                Enumerable.Where(InstanceProvider.DataSet.Choice,
                    c => c.Type == (short)ChoicesType.ServiceCenter).ToList();

            repositoryItemLookUpEditVisaType.DataSource =
                Enumerable.Where(InstanceProvider.DataSet.Choice,
                    c => c.Type == (short)ChoicesType.VisaCategory).ToList();

            repositoryItemLookUpEditNationality.DataSource =
                Enumerable.Where(InstanceProvider.DataSet.Choice,
                    c => c.Type == (short)ChoicesType.Country).ToList();

            repositoryItemLookUpEditRegistryTime.DataSource =
                Enumerable.Where(InstanceProvider.DataSet.Choice,
                    c => c.Type == (short)ChoicesType.RegistryTime).ToList();

            repositoryItemLookUpEditStatus.DataSource =
                Enumerable.Where(InstanceProvider.DataSet.Choice,
                    c => c.Type == (short)ChoicesType.StatusType).ToList();

            _logger.Info(
                $"Time for initialize datasets = {DateTime.Now - timeStart}");
            _logger.Trace("End SetDataSourceForLookUps.");
        }

        private void InitOtherComponentDetails()
        {
            _logger.Trace("Start InitOtherComponentDetails.");
            var timeStart = DateTime.Now;
            ResManager.RegisterResource("uk_UA",
                uk_UA.ResourceManager);
            _logger.Info("InitOtherComponentDetails. ResManager = uk_UA");
            Closed += MainForm_Closed;
            Load += MainForm_Load;

            buttonRegistry.Click += buttonShowSecond_Click;

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            _alertControl = new AlertControl();
            _alertControl.AlertClick += _alertControl_AlertClick;
            _alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
            _alertControl.FormLoad += _alertControl_FormLoad;

            _crawlerRegistry = new RegisterUser();
            _stateManager = new StateManager();

            clientDataRowBindingSource.DataSource =
                InstanceProvider.DataSet.ClientData;
            repositoryItemTextEditPassword.PasswordChar = '*';

            _state = 2;

            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.BestFitColumns();

            InitColumnNames();
            InitFieldNames();
            InitRepositoryNames();
            InitBarButtonNames();

            _logger.Info(
                $"Time for InitOtherComponentDetails = {DateTime.Now - timeStart}");
            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void InitBarButtonNames()
        {
            _logger.Trace("Start InitBarButtonNames");
            applicationMenu1.MenuCaption =
                ResManager.GetString(ResKeys.ApplicationMenu_Caption);
            barButtonItemImport.Caption =
                ResManager.GetString(ResKeys.BarButtonItemImport_Caption);
            barButtonItemSetup.Caption =
                ResManager.GetString(ResKeys.BarButtonItemSetup_Caption);
            _logger.Trace("End InitBarButtonNames");
        }

        private void InitRepositoryNames()
        {
            _logger.Trace("Start InitRepositoryNames");
            repositoryItemLookUpEditStatus.NullText =
                ResManager.GetString(ResKeys.Status_NullText);
            repositoryItemLookUpEditNationality.NullText =
                ResManager.GetString(ResKeys.Nationality_NullText);
            repositoryItemLookUpEditRegistryTime.NullText =
                ResManager.GetString(ResKeys.RegistryTime_NullText);
            repositoryItemLookUpEditVisaType.NullText =
                ResManager.GetString(ResKeys.VisaCategory_NullText);
            repositoryItemLookUpEditVisaCity.NullText =
                ResManager.GetString(ResKeys.ServiceCenter_NullText);
            _logger.Trace("End InitRepositoryNames");
        }

        private void InitFieldNames()
        {
            _logger.Trace("Start InitFieldNames");
            buttonRegistry.Text =
                ResManager.GetString(ResKeys.ButtonRegistry_Text);
            layoutControlGroupClientData.Text =
                ResManager.GetString(ResKeys.lblClientDataGroup);
            buttonCancelAction.Text =
                ResManager.GetString(ResKeys.ButtonCancelAction_Text);
            _logger.Trace("End InitFieldNames");
        }

        private void InitColumnNames()
        {
            _logger.Trace("Start InitColumnNames");
            colVisaType.Caption = ResManager.GetString(ResKeys.lblVisaCategory);
            colVisaCity.Caption = ResManager.GetString(ResKeys.lblServiceCenter);
            colPeopleCount.Caption = ResManager.GetString(ResKeys.colPeopleCount);
            colChildsCount.Caption = ResManager.GetString(ResKeys.colChildsCount);
            colNumberOfReceipt.Caption =
                ResManager.GetString(ResKeys.colNumberOfReceipt);
            colEndPassportDate.Caption =
                ResManager.GetString(ResKeys.colEndPassportDate);
            colStatus.Caption = ResManager.GetString(ResKeys.colStatus);
            colName.Caption = ResManager.GetString(ResKeys.colName);
            colLastName.Caption = ResManager.GetString(ResKeys.colLastName);
            colBirthday.Caption = ResManager.GetString(ResKeys.colBirthday);
            colReturnData.Caption = ResManager.GetString(ResKeys.colReturnData);
            colNationality.Caption =
                ResManager.GetString(ResKeys.Nationality_Text);
            colRegistryFom.Caption = ResManager.GetString(ResKeys.colRegistryFom);
            colRegistryTo.Caption = ResManager.GetString(ResKeys.colRegistryTo);
            _logger.Trace("End InitColumnNames");
        }

        private void SetDefaultState()
        {
            _logger.Trace(
                $"Start SetDefaultState. State = {_state}. InitValue = {_initVal}.  buttonRegistry.Enabled = {buttonRegistry.Enabled}. buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
            _state = 1;
            _initVal = 1;
            SetReadOnly(false);
            _logger.Trace(
                $"End SetDefaultState. State = {_state}. InitValue = {_initVal}. buttonRegistry.Enabled = {buttonRegistry.Enabled}. buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
        }

        private void SetReadOnly(bool readOnly)
        {
            _logger.Trace($"Makes all controls ReadOnly => {readOnly}");
            Invoke(new Action(() =>
            {
                buttonRegistry.Enabled = !readOnly;
                gridView1.OptionsBehavior.Editable = !readOnly;
                gridView1.OptionsView.NewItemRowPosition =
                    !gridView1.OptionsBehavior.Editable
                        ? NewItemRowPosition.None
                        : NewItemRowPosition.Bottom;
                buttonCancelAction.Enabled = readOnly;
            }));
        }

        /// <summary>
        ///     Show Alert Message about User needed action
        /// </summary>
        private void ShowAlert(string message,
            bool title)
        {
            _logger.Info($"ShowAlert with message = {message}. title = {title}");
            Invoke(new Action(() =>
            {
                if (title)
                    _alertControl.Show(null,
                        message,
                        "");
                else
                    _alertControl.Show(null,
                        "",
                        message);
            }));
        }

        private void StartNewWorkRoundBase()
        {
            _logger.Trace("Start StartNewWorkRoundBase");

            SetReadOnly(true);

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

        #endregion Functions
    }
}