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
            var form = new ExceptionHandlerForm()
            {
                StackTrace = ex.Message + ex.StackTrace
            };
            form.ShowDialog();
                       
            string SendMailResult = 
                SendErrorMail(ex); //sending mail message
            form.StackTrace += SendMailResult;
            form.Update();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
            string body = exText.Message;
            body += Environment.NewLine + exText.StackTrace;

            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient(server, port)
            {
                /* Credentials are necessary if the server requires the client */
                /* to authenticate before it will send e-mail on the client's behalf.*/
                //Credentials = CredentialCache.DefaultNetworkCredentials,
                Credentials = new System.Net.NetworkCredential(user, Password),
                EnableSsl = true,
                Timeout = 10000
            };

            string SendErrorMailResult = "Email message sent.";
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                SendErrorMailResult = 
                    String.Format("Exception caught in SendErrorMail(): {0}",
                    ex.ToString());
            }
            return SendErrorMailResult;
        }
    }
}