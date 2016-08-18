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
        [Description("Sessionexpiry")]
        Sessionexpiry = 5
    }

    /// <summary>
    ///     IDictionary < string, VisaPage >
    /// </summary>
    public static class PageDict
    {
        public static IDictionary<string, VisaPage> PageCodeDic
        = new Dictionary<string, VisaPage>()
        {
            { "None.None", (VisaPage)1},
            { "AppWelcome.AppWelcome", (VisaPage)2},
            {  "AppScheduling.AppScheduling", (VisaPage)3},
            { "AppSchedulingGetInfo.AppSchedulingGetInfo", (VisaPage)4},
            { "AppSchedulingReceiptDetails.AppSchedulingReceiptDetails", (VisaPage)5},
            { "Sessionexpiry", (VisaPage)5}
        };
        public static string[] pages = new string[]{
            "None.None",
            "AppWelcome.AppWelcome",
            "AppScheduling.AppScheduling",
            "AppSchedulingGetInfo.AppSchedulingGetInfo",
            "AppSchedulingReceiptDetails.AppSchedulingReceiptDetails",
            "Sessionexpiry"
        };
    }
}
