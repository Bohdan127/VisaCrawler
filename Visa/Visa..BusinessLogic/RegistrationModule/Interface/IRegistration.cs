using System;
using Visa.BusinessLogic.RegistrationModule.Helpers;
using Visa.Database;

namespace Visa.BusinessLogic.RegistrationModule.Interface
{
    public interface IRegistration
    {
        VisaDataSet.ClientDataRow ClientDataRow { get; set; }

        event EventHandler<StateEventArgs> StateChanged;

        event EventHandler<ServerAvailabilityEventArgs> AvailabilityChanged;

        void StartRegistration();

        void CancelRegistration();

        void CloseBrowsers(bool forceClose);

        //event EventHandler<ClientEventArgs> StartRegistration;

        //event EventHandler CancelRegistration;
    }
}