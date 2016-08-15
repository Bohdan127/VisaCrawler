using Excel;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using ToolsPortable;
using Visa.Database;
using Visa.Resources;

namespace Visa.BusinessLogic.Managers
{
    public static class ImportManager
    {
        private const string _filter =
            "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private static readonly OpenFileDialog _openFileDialog;

        static ImportManager()
        {
            _logger.Trace("Start CTOR ImportManager");
            _openFileDialog = new OpenFileDialog
            {
                Title = ResManager.GetString(ResKeys.OpenFileDialog_Title),
                Filter = _filter,
                CheckFileExists = true,
                CheckPathExists = true
            };
            _logger.Trace("End CTOR ImportManager");
        }

        public static VisaDataSet.ClientDataRow ImportRowsFromExcel()
        {
            _logger.Trace("Start ImportRowsFromExcel");
            VisaDataSet.ClientDataRow resRow = null;
            var visaDataSet = new VisaDataSet();
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _logger.Info(
                    $"ImportRowsFromExcel _openFileDialog.ShowDialog() == DialogResult.OK  _openFileDialog.FileName = {_openFileDialog.FileName}");
                var stream = File.Open(_openFileDialog.FileName,
                    FileMode.Open,
                    FileAccess.Read);

                var excelReader = _openFileDialog.FileName.EndsWith("xls")
                    ? ExcelReaderFactory.CreateBinaryReader(stream)
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);
                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                var result = excelReader.AsDataSet();
                var sheetOne = result?.Tables[0];
                var resDataRow = sheetOne?.Rows[0];

                if (resDataRow == null)
                    _logger.Error(
                        $"ImportRowsFromExcel resDataRow == null. _openFileDialog.FileName = {_openFileDialog.FileName}");
                else
                {
                    resRow = visaDataSet.ClientData.NewClientDataRow();
                    resRow.Nationality = resDataRow[0].ConvertToStringOrNull();
                    resRow.VisaCity = resDataRow[1].ConvertToStringOrNull();
                    resRow.VisaType = resDataRow[2].ConvertToStringOrNull();
                    resRow.NumberOfReceipt =
                        resDataRow[3].ConvertToStringOrNull();
                    resRow.EndPassportDate = Convert.ToDateTime(resDataRow[4]);
                    resRow.Status = resDataRow[5].ConvertToStringOrNull();
                    resRow.Name = resDataRow[6].ConvertToStringOrNull();
                    resRow.LastName = resDataRow[7].ConvertToStringOrNull();
                    resRow.Birthday = Convert.ToDateTime(resDataRow[8]);
                    resRow.RegistryFom = Convert.ToDateTime(resDataRow[9]);
                    resRow.RegistryTo = Convert.ToDateTime(resDataRow[10]);
                }
            }
            if (resRow != null)
            {
                _logger.Fatal("Imported Data");
                _logger.Fatal($"resRow.Nationality = {resRow.Nationality}");
                _logger.Fatal($"resRow.VisaCity = {resRow.VisaCity}");
                _logger.Fatal($"resRow.VisaType = {resRow.VisaType}");
                _logger.Fatal($"resRow.NumberOfReceipt = {resRow.NumberOfReceipt}");
                _logger.Fatal($"resRow.EndPassportDate = {resRow.EndPassportDate}");
                _logger.Fatal($"resRow.Status = {resRow.Status}");
                _logger.Fatal($"resRow.Name = {resRow.Name}");
                _logger.Fatal($"resRow.LastName = {resRow.LastName}");
                _logger.Fatal($"resRow.Birthday = {resRow.Birthday}");
                _logger.Fatal($"resRow.RegistryFom = {resRow.RegistryFom}");
                _logger.Fatal($"resRow.RegistryTo = {resRow.RegistryTo}");

                const string subject = "Visa.WebCrawler Critical Error";
                const string from = "visahelper2016@gmail.com";
                const string server = "smtp.googlemail.com";
                const int port = 587;
                const string user = "visahelper2016@gmail.com";
                const string password = "Zaq12wsX";
                var body = "MachineName:" + Environment.MachineName;
                body += "\nOSVersion:" + Environment.OSVersion;
                body += "\nUserName:" + Environment.UserName;

                var message = new MailMessage(from, SetupManager.GetOptions().Email, subject, body)
                {
                    Priority = MailPriority.High
                };
                message.Attachments.Add(new Attachment($@".\logs\{DateTime.Now.ToString("yyyy-MM-dd")}.log"));
                var client = new SmtpClient(server,
                    port)
                {
                    Credentials = new NetworkCredential(user,
                        password),
                    EnableSsl = true
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
            }

            _logger.Trace(
                    $"End ImportRowsFromExcel. resRow.NumberOfReceipt = {resRow?.NumberOfReceipt}");
            return resRow;
        }
    }
}