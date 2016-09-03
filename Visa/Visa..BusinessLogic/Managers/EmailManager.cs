using System;
using System.Net;
using System.Net.Mail;

namespace Visa.BusinessLogic.Managers
{
    public static class EmailManager
    {
        const string Subject = "Visa.WebCrawler Critical Error";
        const string Email = "visahelper2016@gmail.com";
        const string Server = "smtp.googlemail.com";
        const int Port = 587;
        const string Password = "Zaq12wsXz";

        public static string SendErrorMail(Exception exText)
        {
            var body = "MachineName:" + Environment.MachineName;
            body += "\nOSVersion:" + Environment.OSVersion;
            body += "\nUserName:" + Environment.UserName;
            body += Environment.NewLine + exText.Message;
            body += Environment.NewLine + exText.StackTrace;

            var message = new MailMessage(Email, Email, Subject, body)
            {
                Priority = MailPriority.High,
                Attachments = { new Attachment($@".\logs\{DateTime.Now.ToString("yyyy-MM-dd")}.log") }
            };
            return SendEmailWithMessage(message);
        }

        public static string SendEmailWithPhoto(string fileName)
        {
            var message = new MailMessage(Email, SetupManager.GetOptions().Email, Subject, "")
            {
                Priority = MailPriority.High,
                Attachments = { new Attachment(fileName) }
            };
            return SendEmailWithMessage(message);
        }

        public static string SendEmailWithMessage(MailMessage message)
        {
            string sendErrorMailResult;
            using (var client = new SmtpClient(Server,
                Port)
            {
                Credentials = new NetworkCredential(Email,
                    Password),
                EnableSsl = true
            })
            {
                sendErrorMailResult = "Email message is sent.";
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    sendErrorMailResult =
                        $"Exception caught in SendErrorMail(): {ex}";
                }
            }
            return sendErrorMailResult;
        }
    }
}
