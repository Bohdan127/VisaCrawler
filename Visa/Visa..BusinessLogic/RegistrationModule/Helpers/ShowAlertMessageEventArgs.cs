using System;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class ShowAlertMessageEventArgs : EventArgs
    {
        public ShowAlertMessageEventArgs(string message, bool title, string numbetOfReceipt)
        {
            Message = message;
            Title = title;
            NumbetOfReceipt = numbetOfReceipt;
        }

        public string Message { get; set; }

        public bool Title { get; set; }

        public string NumbetOfReceipt { get; set; }
    }
}
