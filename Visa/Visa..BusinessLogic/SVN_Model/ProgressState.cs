namespace Visa.BusinessLogic.SVN_Model
{
    public enum ProgressState : int
    {
        NoState = -1,
        Start = 1,
        GoToUrl = 2,
        StartRegistration = 3,
        SelectCityAndReason = 4,
        ProvidePeopleCount = 5,
        SelectVisaType = 6,
        CheckDate = 7,
        BackToCityAndReason = 8,
        Receipt = 9,
        EmailAndPassword = 10,
        ClientData = 11,
        SubmitClientData1 = 12,
        GetFirstDate = 13,
        ShowMessage = 14,
        SubmitClientData2 = 15,
        SelectRegistrTime = 16,
        SendingEmail = 17,
        BreakState = 127
    }
}
