using DevExpress.XtraEditors;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Visa.BusinessLogic.Managers;
using Visa.Resources;

namespace Visa.WinForms.ErrorProvider
{
    public partial class ExceptionHandlerForm : XtraForm
    {
        public ExceptionHandlerForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            if (!DesignMode)
            {
                Text = ResManager.GetString(ResKeys.ExceptionHanler_Title);
                textBox2.Text =
                    ResManager.GetString(ResKeys.ExceptionHanler_Text);
            }
        }

        public string StackTrace
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public static void ShowException(Exception ex)
        {
            var form = new ExceptionHandlerForm
            {
                StackTrace = ex.Message + ex.StackTrace
            };
            form.ShowDialog();

            var SendMailResult =
                SendErrorMail(ex); //sending mail message
            MessageBox.Show(SendMailResult, "Sending e-mail result:");
        }

        private void simpleButton1_Click(object sender,
            EventArgs e)
        {
            Close();
        }

        public static string SendErrorMail(Exception exText)
        {
            const string subject = "Visa.WebCrawler Critical Error";
            const string from = "visahelper2016@gmail.com";
            const string server = "smtp.googlemail.com";
            const int port = 587;
            const string user = "visahelper2016@gmail.com";
            const string password = "Zaq12wsX";
            var body = "MachineName:" + Environment.MachineName;
            body += "\nOSVersion:" + Environment.OSVersion;
            body += "\nUserName:" + Environment.UserName;
            body += Environment.NewLine + exText.Message;
            body += Environment.NewLine + exText.StackTrace;

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

            var sendErrorMailResult = "Email message is sent.";
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                sendErrorMailResult =
                    $"Exception caught in SendErrorMail(): {ex}";
            }
            return sendErrorMailResult;
        }
    }
}