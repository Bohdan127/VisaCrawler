using System;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class ServerAvailabilityEventArgs : EventArgs
    {
        public ServerAvailabilityEventArgs(bool available)
        {
            Available = available;
        }

        public bool Available { get; set; }
    }
}