using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ToolsPortable;
using Visa.Database;
using Visa.Database.Enums;
using Visa.Resources;
using Visa.WebCrawler.SeleniumCrawler;

// ReSharper disable once CheckNamespace
namespace Visa.WinForms
{
    public partial class MainForm
    {
        #region Members

        private RegisterUser _crawlerRegistry;

        #endregion

        #region Events

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            _logger.Trace("Start GridView1_InvalidRowException");
            e.ExceptionMode = ExceptionMode.NoAction;
            _logger.Trace("End GridView1_InvalidRowException");
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            _logger.Trace("Start GridView1_ValidateRow");
            var currRow = (VisaDataSet.ClientDataRow)gridView1.GetFocusedDataRow();
            if (currRow == null)
            {
                _logger.Info("End gridView1_CustomDrawRowIndicator. currRow == null");
                return;
            }

            currRow.ClearErrors();

            try
            {
                currRow.Email = currRow.LastName + currRow.Birthday.Year.ToString().Remove(0, 2) + "@i.ua";

            }
            catch
            {
                // ignored
            }

            foreach (var column in currRow.Table.Columns.Cast<DataColumn>().
                Where(column => currRow[column].ConvertToStringOrNull().IsBlank()))
            {
                _logger.Info($"Column {column.ColumnName} is empty for row with receipt = {currRow.NumberOfReceipt}");
                currRow.SetColumnError(column, ResManager.GetString(ResKeys.ValidationError_Message_NullText));
            }
            _logger.Trace("End GridView1_ValidateRow");
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
            var currRow = view?.GetDataRow(e.RowHandle) as VisaDataSet.ClientDataRow;
            if (currRow == null)
            {
                //_logger.Trace("End gridView1_CustomDrawRowIndicator. currRow == null");
                return;
            }


            switch (currRow.RegistryState)
            {
                case (byte)RegistryState.Added:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[0], e.Bounds);
                    break;
                case (byte)RegistryState.Completed:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[1], e.Bounds);
                    break;
                case (byte)RegistryState.Failed:
                    //_logger.Trace($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    e.Graphics.DrawImageUnscaled(imageCollection1.Images[2], e.Bounds);
                    break;
                default:
                    _logger.Error($"gridView1_CustomDrawRowIndicator =>currRow.Email = {currRow.NumberOfReceipt} currRow.RegistryState = {currRow.RegistryState}");
                    break;
            }
            e.Handled = true;
            //_logger.Trace("End gridView1_CustomDrawRowIndicator");
        }

        private void buttonShowSecond_Click(object sender, EventArgs e)
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
                XtraMessageBox.Show(ResManager.GetString(ResKeys.ValidationError_Message_SecondPart), ResManager.GetString(ResKeys.ValidationError_Title), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            _logger.Trace($"End buttonShowSecond_Click. buttonRegistry.Enabled = {buttonRegistry.Enabled}");
        }

        private void progressBarWorker_DoWorkSecondPart(object sender, DoWorkEventArgs e)
        {
            _logger.Trace($"Start progressBarWorker_DoWorkSecondPart. State = {_state}. InitValue = {_initVal}");
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
                    maxVal = 90;
                    break;
                case 4:
                    maxVal = 100;
                    break;
            }

            for (var i = _initVal; i <= maxVal; i++)
            {
                if (_crawlerRegistry?.Error ?? false)
                {
                    _logger.Warn("return progressBarWorker_DoWorkSecondPart. _crawlerRegistry.Error = true");
                    return;
                }
                Thread.Sleep(1000);
            }

            _initVal = maxVal;
            if (maxVal == 100)
            {
                _logger.Info("progressBarWorker_DoWorkSecondPart. maxVal == 100");
                SetDefaultState();
            }
            _logger.Info($"End progressBarWorker_DoWorkSecondPart. State = {_state}. InitValue = {_initVal}");

        }


        #endregion

        #region Functions

        private void CrawlerWorkSecondPart(object data)
        {
            _logger.Trace("Start CrawlerWorkSecondPart");
            var dataRow = data as VisaDataSet.ClientDataRow;

            if (dataRow == null)
            {
                _logger.Error("return _crawlerWorker_DoWork dataRow == null");
                return;
            }

            switch (_state)
            {
                case 1:
                    Invoke(new Action(() =>
                    {
                        _alertControl.AlertFormList.ForEach(alert => alert.Close());
                    }));
                    _crawlerRegistry = new RegisterUser();
                    _crawlerRegistry.PartOne(dataRow,
                        Convert.ToInt32(lookUpEditServiceCenter.EditValue));
                    _state = 105;
                    break;
                case 105:
                    _crawlerRegistry.PartOneAndHalf(
                        Convert.ToInt32(lookUpEditVisaCategory.EditValue));
                    _state = 2;
                    break;
                case 2:
                    _crawlerRegistry.PartTwo(dataRow);
                    _state = 3;
                    break;

                case 3:
                    _crawlerRegistry.PartThree();
                    _state = 4;
                    break;

                case 4:
                    _crawlerRegistry.PartFour(dataRow);
                    if (!_crawlerRegistry.Error)
                    {
                        XtraMessageBox.Show(_crawlerRegistry.OutData,
                            ResManager.GetString(ResKeys.SearchResult),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                    _state = 5;
                    break;

                case 5:
                    _crawlerRegistry.PartFive();
                    if (!_crawlerRegistry.Error)
                    {
                        _logger.Info(
                            $"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawlerRegistry.OutData}. _crawlerRegistry.Error = false ");
                        XtraMessageBox.Show(_crawlerRegistry.OutData,
                            ResManager.GetString(ResKeys.SearchResult),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        CloseBrowsers(false);
                        return;
                    }
                    break;
                default:
                    _logger.Error($"incorrect State = {_state} for CrawlerWorkFirstPart");
                    break;
            }
            if (_crawlerRegistry?.Canceled ?? false)
            {
                SetDefaultState();
                CloseBrowsers(true);
                // ReSharper disable once PossibleNullReferenceException
                _crawlerRegistry.Canceled = false;
            }
            else if (_crawlerRegistry?.Error ?? false)
            {
                _logger.Warn($"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawlerRegistry?.OutData}. _crawlerRegistry.Error = true ");
                ShowAlert(_crawlerRegistry?.OutData.IsNotBlank() ?? false
                    ? _crawlerRegistry.OutData
                    : ResManager.GetString(ResKeys.ServerError));
                Thread.Sleep(500);
                SetDefaultState();
                CloseBrowsers(false);
            }
            else
                ShowAlert(ResManager.GetString(ResKeys.FillCaptchaAndPress));

            _logger.Trace("End CrawlerWorkSecondPart");
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
            for (var i = 0; i < gridView1.RowCount; i++)
            {

                //todo log row with error 
            }
            return bRes;
        }

        private void StartNewWorkRoundSecond()
        {
            _logger.Trace("Start StartNewWorkRoundSecond");

            isFirstPart = false;
            StartNewWorkRoundBase();

            _logger.Trace("End StartNewWorkRoundSecond");
        }

        #endregion
    }
}
