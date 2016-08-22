namespace Visa.BusinessLogic.SVN_Model
{
    public enum ProgressState
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
        SubmitDate,
        BackToCityAndReason,
        Receipt,
        SubmitReciept,
        EmailAndPassword,
        SubmitEmailAndPassword,
        ClientData,
        SubmitClientData,
        GetFirstDate,
        ShowMessage,
        SelectRegistrationTime,
        SendingEmail,
        BreakState
    }
}
