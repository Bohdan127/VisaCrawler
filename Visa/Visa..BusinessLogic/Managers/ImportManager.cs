using Excel;
using GlobalResources;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;
using ToolsPortable;
using Visa.Database;

namespace Visa.BusinessLogic.Managers
{
    public static class ImportManager
    {
        private const string _filter =
            "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private static readonly OpenFileDialog OpenFileDialog;

        static ImportManager()
        {
            _logger.Trace("Start CTOR ImportManager");OpenFileDialog = new OpenFileDialog
            {
                Title = ResManager.GetString(ResKeys.OpenFileDialog_Title),
                Filter = _filter,
                CheckFileExists = true,
                CheckPathExists = true
            };
            _logger.Trace("End CTOR ImportManager");
        }

        public static VisaDataSet.ClientDataRow[] ImportRowsFromExcel()
        {
            _logger.Trace("Start ImportRowsFromExcel");
            VisaDataSet.ClientDataRow[] resRow = null;
            var visaDataSet = new VisaDataSet();
            int rowsCount = 0;
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                _logger.Info(
                    $"ImportRowsFromExcel _openFileDialog.ShowDialog() == DialogResult.OK  _openFileDialog.FileName = {OpenFileDialog.FileName}");
                var stream = File.Open(OpenFileDialog.FileName,
                    FileMode.Open,
                    FileAccess.Read);

                var excelReader = OpenFileDialog.FileName.EndsWith("xls")
                    ? ExcelReaderFactory.CreateBinaryReader(stream)
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);
                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                var result = excelReader.AsDataSet();
                var sheetOne = result?.Tables[0];
                for (int i = 0; i < sheetOne?.Rows.Count; i++)
                {
                    if (sheetOne?.Rows[i][0].ConvertToStringOrNull() != null)
                        rowsCount = i + 1;
                    else break;
                }
                if (rowsCount == 0)
                {
                    _logger.Trace($"End ImportRowsFromExcel. no one row imported");
                    return null;
                }
                else
                    resRow = new VisaDataSet.ClientDataRow[rowsCount];
                for (int i = 0; i < rowsCount; i++)
                {
                    var resDataRow = sheetOne?.Rows[i];

                    if (resDataRow == null)
                        _logger.Error(
                            $"ImportRowsFromExcel resDataRow == null. _openFileDialog.FileName = {OpenFileDialog.FileName}");
                    else
                    {
                        resRow[i] = visaDataSet.ClientData.NewClientDataRow();
                        resRow[i].Nationality = resDataRow[0].ConvertToStringOrNull();
                        resRow[i].VisaCity = resDataRow[1].ConvertToStringOrNull();
                        resRow[i].VisaType = resDataRow[2].ConvertToStringOrNull();
                        resRow[i].NumberOfReceipt =
                            resDataRow[3].ConvertToStringOrNull();
                        resRow[i].EndPassportDate = Convert.ToDateTime(resDataRow[4]);
                        resRow[i].Status = resDataRow[5].ConvertToStringOrNull();
                        resRow[i].Name = resDataRow[6].ConvertToStringOrNull();
                        resRow[i].LastName = resDataRow[7].ConvertToStringOrNull();
                        resRow[i].Birthday = Convert.ToDateTime(resDataRow[8]);
                        resRow[i].RegistryFom = Convert.ToDateTime(resDataRow[9]);
                        resRow[i].RegistryTo = Convert.ToDateTime(resDataRow[10]);
                        if (resDataRow.Table.Columns.Count > 11)
                        {
                            resRow[i].PassportNumber = resDataRow[11].ConvertToStringOrNull();
                            resRow[i].CellNumber = resDataRow[12].ConvertToStringOrNull();
                        }
                    }
                }
            }
            for (int i = 0; i < rowsCount; i++)
                _logger.Trace(
                    $"ImportRowsFromExcel. resRow[{i}].NumberOfReceipt = {resRow[i]?.NumberOfReceipt}");
            _logger.Trace(
                   $"End ImportRowsFromExcel. rowsCount = {rowsCount}");
            return resRow;
        }
    }
}