using System;

namespace Visa.BusinessLogic.RegistrationModule.Helpers
{
    public class StateEventArgs : EventArgs
    {
        public StateEventArgs(bool readOnly)
        {
            ReadOnly = readOnly;
        }

        public bool ReadOnly { get; set; }
    }
}