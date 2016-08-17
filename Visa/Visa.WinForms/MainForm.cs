//#define GoWithoutDates

using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.Database.Enums;
using Visa.License.Logic;
using Visa.Resources;
using Visa.Resources.uk_UA;
using Visa.WebCrawler.SeleniumCrawler;
using Visa.WinForms.ErrorProvider;
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

            Closed += MainForm_Closed;
            Load += MainForm_Load;

            #endregion CTOR


            _logger.Trace("End MainForm CTOR");
        }


        #region Members

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
        private ProgressState _progressState = ProgressState.Start;

        private const int RefreshCount = int.MaxValue - 1;

        /// <summary>
        ///     Constant marks _state for immediate end of registration process
        /// </summary>
        private const ProgressState BreakState = ProgressState.BreakState;

        public bool ShowCaptchaMessage => _progressState == ProgressState.SelectVisaType || _progressState == ProgressState.Start;

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

            currRow.Email = currRow.LastName
                + currRow.Birthday.ToString("yy") // short Year
                + "@i.ua";
            //currRow.Email = SetupManager.GetOptions().Email;
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
                    //_logger.Trace(
                    //$"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt}"
                    //+$" currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[0],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Completed:
                    //_logger.Trace(
                    //$"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt}"
                    //+$" currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[1],
                        e.Bounds);
                    break;

                case (byte)RegistryState.Failed:
                    //_logger.Trace(
                    //$"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt}"
                    //+$" currRow.RegistryState = {currRow.RegistryState}");
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
            //_logger.Trace("End gridView1_CustomDrawRowIndicator");
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
            {
                _crawlerRegistry.Canceled = true;
                _crawlerRegistry.Error = false;
                CloseBrowsers(false);
            }
            SetDefaultState();
            _logger.Trace("End simpleButtonCancelAction_Click");
        }

        private void _alertControl_BeforeFormShow(object sender,
            AlertFormEventArgs e)
        {
            _logger.Trace($"Calculate Alert Location. X- {(Screen.PrimaryScreen.Bounds.Width + 150) / 2}, "
                + $" Y - {(Screen.PrimaryScreen.Bounds.Height - 150) / 2};");
            e.Location = new Point((Screen.PrimaryScreen.Bounds.Width + 150) / 2,
                (Screen.PrimaryScreen.Bounds.Height - 150) / 2);
        }

        private void _alertControl_AlertClick(object sender,
            AlertClickEventArgs e)
        {
            _logger.Info(
                $"Start _alertControl_AlertClick. Alert Text - {e.AlertForm.Text}. State - {_progressState}");
            StartNewWorkRoundBase();
            e.AlertForm.Close();
            _logger.Trace(
                $"End _alertControl_AlertClick. e.AlertForm.IsAccessible = {e.AlertForm.IsAccessible}."
                + $"  buttonRegistry.Enabled ={buttonRegistry.Enabled}");
        }

        private void _alertControl_FormLoad(object sender,
            AlertFormLoadEventArgs e)
        {
            _logger.Info("_alertControl_FormLoad");
            if (e.AlertForm.AlertInfo.Text.IsNotBlank())
                e.Buttons.PinButton.SetDown(true);
        }

        private async void MainForm_Load(object sender,
            EventArgs e)
        {
            _logger.Trace("Start MainForm_Load");
            SetReadOnly(false);
            ResManager.RegisterResource("uk_UA",
                uk_UA.ResourceManager);
            _logger.Info("InitOtherComponentDetails. ResManager = uk_UA");
            //we can off checking just for offline testing:
            CheckLicense();
            await Task.Run(() => Invoke(
                new Action(() =>
                {
                    InitOtherComponentDetails();
                    SetDataSourceForLookUps();
                })))
                .ConfigureAwait(false);
            _logger.Trace("End MainForm_Load");
        }

        private void MainForm_Closed(object sender,
            EventArgs e)
        {
            _logger.Info($"MainForm_Closed. State = {_progressState}.");
            CloseBrowsers(true);
        }

        private void _crawlerWorker_DoWork(object sender,
            DoWorkEventArgs e)
        {
            _logger.Trace($"Start _crawlerWorker_DoWork. State = {_progressState}");

            var bBreak = false; //_crawlerWorker_CheckSiteAvailability();
            do
            {
                CrawlerRefreshEngine();
                _logger.Info($"End CrawlerWorkSecondPart _state={_progressState}");
                if (_crawlerRegistry == null)
                {
                    //This just testing changes!!!!
                    throw new NotImplementedException();
                    //This error instead of NullPointerException,
                    //because if _crawlerRegistry is null here we something code in wrong way
                }
                if (_crawlerRegistry.Canceled)
                {
                    _logger.Warn($" _crawlerRegistry.Canceled _state={_progressState}");
                    bBreak = true;
                    //SetDefaultState();
                    //CloseBrowsers(false);
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
                            XtraMessageBox.Show(ResManager.GetString(ResKeys.Complete_Registration),
                                ResManager.GetString(ResKeys.SearchResult),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            bBreak = true;
                            SetDefaultState();
                            break;
                        case ProgressState.SelectVisaType:
                        case ProgressState.Receipt:
                        case ProgressState.ShowMessage:
                        case ProgressState.SubmitClientData2:
                            ShowAlert(
                                ResManager.GetString(ResKeys.FillCaptchaAndPress),
                                false);
                            bBreak = true;
                            break;
                        case ProgressState.GetFirstDate:
                            ShowAlert(
                                ResManager.GetString(ResKeys.Fill_Calendar_And_Captcha),
                                false);
                            bBreak = true;
                            break;
                        case ProgressState.BreakState:
                            SetDefaultState();
                            bBreak = true;
                            break;
                    }
            } while (!bBreak);

            _logger.Trace($"End _crawlerWorker_DoWork. State = {_progressState}."
                + $" _crawlerRegistry.Error = {_crawlerRegistry?.Error}");
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
            _logger.Trace($"Start CrawlerWorkSecondPart _state = {_progressState}");
            var dataRow = data as VisaDataSet.ClientDataRow;

            if (dataRow == null)
            {
                _logger.Error("return _crawlerWorker_DoWork dataRow == null");
                return;
            }
            switch (_progressState)
            {
                case ProgressState.Start: // alerts.Close(), and StartsAgain
                    Invoke(
                        new Action(
                            () =>
                            {
                                _alertControl.AlertFormList.ForEach(
                                    alert => alert.Close());
                            }));
                    _crawlerRegistry = new RegisterUser();
                    _progressState = ProgressState.GoToUrl;
                    break;

                case ProgressState.GoToUrl: // GoToUrl()
                    _crawlerRegistry.GoToUrl();
                    _progressState = ProgressState.StartRegistration;
                    break;

                case ProgressState.StartRegistration: // StartRegistration()
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.StartRegistration());
                    _progressState = ProgressState.SelectCityAndReason;
                    break;

                case ProgressState.SelectCityAndReason: // SelectCityAndReason(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.SelectCityAndReason(dataRow));
                    _progressState = ProgressState.ProvidePeopleCount;
                    break;

                case ProgressState.ProvidePeopleCount: // ProvidePeopleCount(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.ProvidePeopleCount(dataRow));
                    _progressState = ProgressState.SelectVisaType;
                    break;

                case ProgressState.SelectVisaType: // SelectVisaType(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.SelectVisaType(dataRow));
                    _progressState = ProgressState.CheckDate;
                    break;

                case ProgressState.CheckDate: //CheckDate(dataRow)
                    //todo Bohdan127 we should think how to use here  _crawlerRegistry.RunNextStep(() =>
                    var isAvailableDate =
                        _crawlerRegistry.CheckDate(dataRow);
                    if (_crawlerRegistry.FillCapchaFailed)
                    {
                        _crawlerRegistry.Error = false;
                        _progressState = ProgressState.SelectVisaType;
                        _logger.Warn("Fill Capcha Failed");
                        _crawlerRegistry.FillCapchaFailed = false;
                        return;
                    }
                    if (isAvailableDate)
                    {
                        _crawlerRegistry.RegistrarionDateAvailability =
                            DialogResult.Yes;
                        _progressState = ProgressState.Receipt;
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
                    break;

                case ProgressState.BackToCityAndReason: // BackToCityAndReason()
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.BackToCityAndReason());
                    _progressState = ProgressState.SelectCityAndReason; // SelectCityAndReason(dataRow)
                    break;

                case ProgressState.Receipt: // Receipt(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.Receipt(dataRow));
                    _progressState = ProgressState.EmailAndPassword;
                    break;

                case ProgressState.EmailAndPassword: // EmailAndPassword(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.EmailAndPassword(dataRow));
                    _progressState = ProgressState.ClientData; // ClientData(dataRow)
                    break;
                case ProgressState.ClientData: // ClientData(dataRow)
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.ClientData(dataRow));
                    _progressState = ProgressState.SubmitClientData1; // SubmitClientData()
                    break;

                case ProgressState.SubmitClientData1: // SubmitClientData()
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.SubmitClientData());
                    _progressState = ProgressState.GetFirstDate; // GetFirstDate(dataRow)
                    break;

                case ProgressState.GetFirstDate: // GetFirstDate(dataRow)
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (_crawlerRegistry.RegistrarionDateAvailability)
                    {
                        case DialogResult.Cancel:
                        case DialogResult.Retry:
#if !GoWithoutDates
                        case DialogResult.None:
#endif
                            _logger.Warn($"{_crawlerRegistry.RegistrarionDateAvailability}"
                            + $" for GetFirstDate _state={_progressState}");
                            _progressState = ProgressState.Start; // alerts.Close(), and StartAgain
                            ExceptionHandlerForm.SendErrorMail(
                                new NotImplementedException());
                            XtraMessageBox.Show(ResManager.GetString(ResKeys.Application_Logic_Error),
                                ResManager.GetString(ResKeys.SearchResult),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            break;
                        case DialogResult.Yes:
                            _logger.Info($"Yes for GetFirstDate _state={_progressState}");

                            _crawlerRegistry.RunNextStep(
                                () => _crawlerRegistry.GetFirstDate(dataRow));

                            if (!_crawlerRegistry.GetFirstDateScroll || _crawlerRegistry.Error)
                                _progressState = ProgressState.SubmitClientData2;
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

                case ProgressState.ShowMessage:// show special message for selecting Date of Registration
                    _progressState = ProgressState.SubmitClientData2;
                    break;
                case ProgressState.SubmitClientData2:// SubmitClientData
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.SubmitClientData());
                    _progressState = ProgressState.SelectRegistrTime;
                    break;
                case ProgressState.SelectRegistrTime:
                    _crawlerRegistry.RunNextStep(
                        () => _crawlerRegistry.SelectRegistrationTime());
                    _progressState = ProgressState.Start; // alerts.Close(), and StartAgain
                    //todo Bohdan127 dataRow.Status!!!!!!!!
                    break;

                #region Old dead code, will be removed later because probably part of them will be needed later

                //case 5:
                //    _crawlerRegistry.PartFive();
                //    if (!_crawlerRegistry.Error)
                //    {
                //        _logger.Info(
                //            $"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawlerRegistry.OutData}."
                //                + " _crawlerRegistry.Error = false ");
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
                    _logger.Error($"Incorrect State = {_progressState} for CrawlerWorkFirstPart");
                    break;
            }

            _logger.Trace($"End CrawlerWorkSecondPart. _state={_progressState}");
        }

        private void CrawlerRefreshEngine()
        {
            _logger.Trace($"Start CrawlerRefreshEngine _state = {_progressState}");
            var counter = 0;
            var toStateFour = false;
            do
            {
                if (_crawlerRegistry.Error)
                {
                    if (_crawlerRegistry.IsServerDown)
                    {
                        _logger.Warn($"Reload page. _state = {_progressState}");
                        ShowAlert(ResManager.GetString(ResKeys.WebPage_WillBeReloaded),
                            true);
                        if (toStateFour)
                        {
                            _progressState = ProgressState.SelectCityAndReason;
                        }
                        _crawlerRegistry.ReloadPage();
                    }
                    else
                    {
                        ShowAlert(ResManager.GetString(ResKeys.WebPage_StillNotLoaded),
                            true);
                    }
                }
                toStateFour = false;
                _crawlerRegistry.Error = false;

                //todo Bohdan127 later here should be changed for collecting all rows instead first one like now
                CrawlerWorkSecondPart(gridView1.GetDataRow(0));

                if (_crawlerRegistry.Canceled
                    || !_crawlerRegistry.Error)
                    continue;

                _logger.Warn($"!_crawlerRegistry.Canceled && _crawlerRegistry.Error. _state = {_progressState}."
                     + $" counter = {counter} _crawlerRegistry.ValidationError = {_crawlerRegistry.ValidationError}");
                switch (_progressState)
                {
                    case ProgressState.Start:
                        _progressState = ProgressState.SubmitClientData2;
                        break;
                    case ProgressState.CheckDate:
                        toStateFour = true;
                        break;
                    case ProgressState.BackToCityAndReason: // BackToCityAndReason()
                    case ProgressState.Receipt: // Receipt(dataRow)
                        _progressState = ProgressState.SelectCityAndReason; // SelectCityAndReason(dataRow)
                        break;
                    case ProgressState.SubmitClientData2:
                        _progressState = ProgressState.SubmitClientData1; // GetFirstDate(dataRow)
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
                _progressState = BreakState;
                _crawlerRegistry.ValidationError = false;
            } while (counter < RefreshCount
                     && _crawlerRegistry.Error
                     && SetupManager.GetOptions().RepeatIfCrash);
            _logger.Trace($"End CrawlerRefreshEngine. _state={_progressState}");
        }

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
            var licenseForm = new LicenseForm();
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

        /// <summary>
        ///     Close Browsers for all Crawler workers
        /// </summary>
        /// <param name="forceClose">Should it be closed without looking for toggleSwitchCloseBrowser flag? </param>
        protected virtual void CloseBrowsers(bool forceClose)
        {
            if (!forceClose
                && !SetupManager.GetOptions().CloseBrowser)
                return;
            _crawlerRegistry?.CloseBrowser();
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

            buttonRegistry.Click += buttonShowSecond_Click;

            _crawlerWorker = new BackgroundWorker();
            _crawlerWorker.DoWork += _crawlerWorker_DoWork;

            _alertControl = new AlertControl();
            _alertControl.AlertClick += _alertControl_AlertClick;
            _alertControl.BeforeFormShow += _alertControl_BeforeFormShow;
            _alertControl.FormLoad += _alertControl_FormLoad;

            _crawlerRegistry = new RegisterUser();

            clientDataRowBindingSource.DataSource = InstanceProvider.DataSet.ClientData;
            repositoryItemTextEditPassword.PasswordChar = '*';

            _progressState = ProgressState.GoToUrl;

            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.BestFitColumns();

            InitColumnNames();
            InitFieldNames();
            InitRepositoryNames();
            InitBarButtonNames();

            _logger.Info($"Time for InitOtherComponentDetails = {DateTime.Now - timeStart}");
            _logger.Trace("End InitOtherComponentDetails.");
        }

        private void InitBarButtonNames()
        {
            _logger.Trace("Start InitBarButtonNames");
            applicationMenu1.MenuCaption = ResManager.GetString(ResKeys.ApplicationMenu_Caption);
            barButtonItemImport.Caption = ResManager.GetString(ResKeys.BarButtonItemImport_Caption);
            barButtonItemSetup.Caption = ResManager.GetString(ResKeys.BarButtonItemSetup_Caption);
            _logger.Trace("End InitBarButtonNames");
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
            buttonRegistry.Text = ResManager.GetString(ResKeys.ButtonRegistry_Text);
            layoutControlGroupClientData.Text = ResManager.GetString(ResKeys.lblClientDataGroup);
            buttonCancelAction.Text = ResManager.GetString(ResKeys.ButtonCancelAction_Text);
            _logger.Trace("End InitFieldNames");
        }

        private void InitColumnNames()
        {
            _logger.Trace("Start InitColumnNames");
            colVisaType.Caption = ResManager.GetString(ResKeys.lblVisaCategory);
            colVisaCity.Caption = ResManager.GetString(ResKeys.lblServiceCenter);
            colPeopleCount.Caption = ResManager.GetString(ResKeys.colPeopleCount);
            colChildsCount.Caption = ResManager.GetString(ResKeys.colChildsCount);
            colNumberOfReceipt.Caption = ResManager.GetString(ResKeys.colNumberOfReceipt);
            colEndPassportDate.Caption = ResManager.GetString(ResKeys.colEndPassportDate);
            colStatus.Caption = ResManager.GetString(ResKeys.colStatus);
            colName.Caption = ResManager.GetString(ResKeys.colName);
            colLastName.Caption = ResManager.GetString(ResKeys.colLastName);
            colBirthday.Caption = ResManager.GetString(ResKeys.colBirthday);
            colReturnData.Caption = ResManager.GetString(ResKeys.colReturnData);
            colNationality.Caption = ResManager.GetString(ResKeys.Nationality_Text);
            colRegistryFom.Caption = ResManager.GetString(ResKeys.colRegistryFom);
            colRegistryTo.Caption = ResManager.GetString(ResKeys.colRegistryTo);
            _logger.Trace("End InitColumnNames");
        }

        private void SetDefaultState()
        {
            _logger.Trace($"Start SetDefaultState. State = {_progressState}."
                + $"  buttonRegistry.Enabled = {buttonRegistry.Enabled}."
                + $" buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
            _progressState = ProgressState.Start;
            SetReadOnly(false);
            _logger.Trace($"End SetDefaultState. State = {_progressState}."
                + $" buttonRegistry.Enabled = {buttonRegistry.Enabled}."
                + $" buttonCancelAction.Enabled = {buttonCancelAction.Enabled}.");
        }

        private void SetReadOnly(bool readOnly)
        {
            _logger.Trace($"Makes all controls ReadOnly => {readOnly}");
            Invoke(new Action(() =>
            {
                buttonRegistry.Enabled = !readOnly;
                gridView1.OptionsBehavior.Editable = !readOnly;
                gridView1.OptionsView.NewItemRowPosition = !gridView1.OptionsBehavior.Editable
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