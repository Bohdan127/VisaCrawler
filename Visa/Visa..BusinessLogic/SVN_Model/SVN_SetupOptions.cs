using System;

namespace Visa.BusinessLogic.SVN_Model
{
    [Serializable]
    public class SetupOptions
    {
        public string PeopleCount { get; set; }
        public string ChildCount { get; set; }
        public string Password { get; set; }
        public string Nationality { get; set; }
        public bool CloseBrowser { get; set; }
        public bool RepeatIfCrash { get; set; }
        public string AvailabilityUrl { get; set; }
    }
}