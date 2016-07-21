using System;

namespace Visa.BusinessLogic.SVN_Model
{
    [Serializable]
    public class SetupOptions
    {
        public string Password { get; set; }
        public string Nationality { get; set; }
        public bool CloseBrowser { get; set; }
        public bool RepeatIfCrash { get; set; }
    }
}
