﻿namespace Visa.BusinessLogic.SVN_Model
{
    public enum ProgressState : int
    {
        NoState,
        Start,
        GoToUrl,
        StartRegistration,
        SelectCityAndReason,
        SubmitCityAndReason,
        ProvidePeopleCount,
        SelectVisaType,
        CheckDate,
        BackToCityAndReason,
        Receipt,
        SubmitReciept,
        EmailAndPassword,
        SubmitEmailAndPassword,
        ClientData,
        SubmitClientData,
        GetFirstDate,
        ShowMessage,
        SubmitRegistrationDate,
        SelectRegistrTime,
        SendingEmail,
        BreakState
    }
}
