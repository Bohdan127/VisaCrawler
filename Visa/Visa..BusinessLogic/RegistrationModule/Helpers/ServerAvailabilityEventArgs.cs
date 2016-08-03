using System;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class ServerAvailabilityEventArgs : EventArgs
    {
        public bool Available { get; set; }

        public ServerAvailabilityEventArgs(bool available)
        {
            Available = available;
        }
    }
}
