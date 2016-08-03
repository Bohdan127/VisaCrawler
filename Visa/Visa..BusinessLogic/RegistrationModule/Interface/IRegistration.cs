using System;
using Visa.BusinessLogic.RegistrationModule.Helpers;
using Visa.Database;

namespace Visa.BusinessLogic.RegistrationModule.Interface
{
    public interface IRegistration
    {

        event EventHandler<StateEventArgs> StateChanged;

        event EventHandler<ServerAvailabilityEventArgs> AvailabilityChanged;


        VisaDataSet.ClientDataRow ClientDataRow { get; set; }


        void StartRegistration();

        void CancelRegistration();

        void CloseBrowsers(bool forceClose);

        //event EventHandler CancelRegistration;
        //event EventHandler<ClientEventArgs> StartRegistration;
    }
}
