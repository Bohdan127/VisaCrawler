using System.Json;
using System.Net;

namespace Visa.WinForms.Managers
{
    public class StateManager
    {
        //todo maybe better to move this link into options???
        protected string ScriptUrl = "http://wiknaopen.esy.es/visa/out.json";

        public bool GetCurrentSiteAvailability()
        {
            var bRes = GetScriptResult(ScriptUrl);

            return bRes;
        }

        protected virtual bool GetScriptResult(string scriptUrl)
        {
            JsonValue json;
            using (var wc = new WebClient())
            {
                var jsonString = wc.DownloadString(ScriptUrl);
                json = JsonValue.Parse(jsonString);
            }
            return json["SiteStatus"].ToString() == "1";
        }
    }
}