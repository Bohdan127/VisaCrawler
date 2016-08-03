using System;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class StateEventArgs : EventArgs
    {
        public bool ReadOnly { get; set; }

        public StateEventArgs(bool readOnly)
        {
            ReadOnly = readOnly;
        }
    }
}
