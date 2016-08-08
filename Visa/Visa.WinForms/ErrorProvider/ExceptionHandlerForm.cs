using DevExpress.XtraEditors;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
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
            MessageBox.Show(SendMailResult,"Sending e-mail result:");
        }

        private void simpleButton1_Click(object sender,
            EventArgs e)
        {
            Close();
        }

        public static string SendErrorMail(Exception exText)
        {
            const string subject = "Visa.WebCrawler Critical Error";
            const string to = "visahelper2016@gmail.com";
            const string from = "visahelper2016@gmail.com";
            const string server = "smtp.googlemail.com";
            const int port = 587;
            const string user = "visahelper2016@gmail.com";
            const string Password = "Zaq12wsX";
            var body = exText.Message;
            body += Environment.NewLine + exText.StackTrace;

            var message = new MailMessage(from, to, subject, body)
            {
                Priority = MailPriority.High
            };
            message.Attachments.Add(new Attachment(".\\Visa.log"));
            var client = new SmtpClient(server,
                port)
            {
                Credentials = new NetworkCredential(user,
                    Password),
                EnableSsl = true
            };

            var SendErrorMailResult = "Email message is sent.";
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                SendErrorMailResult =
                    string.Format("Exception caught in SendErrorMail(): {0}",
                        ex);
            }
            return SendErrorMailResult;
        }
    }
}