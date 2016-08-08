using System;
using Visa.Database;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class ClientEventArgs : EventArgs
    {
        public VisaDataSet.ClientDataRow ClientRow { get; set; }
    }
}