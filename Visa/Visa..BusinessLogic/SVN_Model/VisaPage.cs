using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Visa.BusinessLogic.SVN_Model
{
    /// <summary>
    ///     Enum "Name of Current Page"
    /// </summary>
    public enum VisaPage
    {
        [Description("None")]
        None = 0,
        [Description("AppWelcome")]
        AppWelcome = 1,
// (S(ems55hr1n1behuikzzbvxbnr))/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA%3d
        [Description("AppScheduling")]
        AppScheduling = 2,
// (S(ems55hr1n1behuikzzbvxbnr))/AppScheduling/AppScheduling.aspx?p=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA%3d
        [Description("AppSchedulingGetInfo")]
        AppSchedulingGetInfo = 3,
// (S(ems55hr1n1behuikzzbvxbnr))/AppScheduling/AppSchedulingGetInfo.aspx?p=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA%3d
        [Description("AppSchedulingReceiptDetails")]
        AppSchedulingReceiptDetails = 4,
        [Description("AppEmailRegistration")]
        AppEmailRegistration = 5,
        [Description("AppSchedulingVisaCategory")]
        AppSchedulingVisaCategory = 6,
        [Description("AppSchedulingInterviewDate")]
        AppSchedulingInterviewDate = 7,
        [Description("Sessionexpiry")]
        Sessionexpiry = 10
    }

    /// <summary>
    ///     IDictionary < string, VisaPage >
    /// </summary>
    public static class PageDict
    {
        public static IDictionary<string, VisaPage> PageCodeDic
        = new Dictionary<string, VisaPage>()
        {
            { "None.None", (VisaPage)0},
            { "AppWelcome.AppWelcome", (VisaPage)1},
            {  "AppScheduling.AppScheduling", (VisaPage)2},
            { "AppSchedulingGetInfo.AppSchedulingGetInfo", (VisaPage)3},
            { "AppSchedulingReceiptDetails.AppSchedulingReceiptDetails", (VisaPage)4},
            { "AppEmailRegistration.AppEmailRegistration", (VisaPage)5},
            { "AppSchedulingVisaCategory.AppSchedulingVisaCategory", (VisaPage)6},
            { "AppSchedulingInterviewDate.AppSchedulingInterviewDate", (VisaPage)7},
            { "Sessionexpiry", (VisaPage)10}
        };
        public static string[] pages = new string[]{
            "None.None",
            "AppWelcome.AppWelcome",
            "AppScheduling.AppScheduling",
            "AppSchedulingGetInfo.AppSchedulingGetInfo",
            "AppSchedulingReceiptDetails.AppSchedulingReceiptDetails",
            "AppEmailRegistration.AppEmailRegistration",
            "AppSchedulingVisaCategory.AppSchedulingVisaCategory",
            "AppSchedulingInterviewDate.AppSchedulingInterviewDate",
            "Sessionexpiry"
        };
    }
}
