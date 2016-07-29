using NLog;
using System.Json;
using System.Net;

namespace Visa.BusinessLogic.Managers
{
    public class StateManager
    {
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        //todo maybe better to move this link into options???
        protected string ScriptUrl = "http://wiknaopen.esy.es/visa/out.json";

        public bool GetCurrentSiteAvailability()
        {
            _logger.Trace("Start GetCurrentSiteAvailability.");

            var bRes = GetScriptResult(ScriptUrl);

            _logger.Trace($"End GetCurrentSiteAvailability. bRes = {bRes}");
            return bRes;
        }

        protected virtual bool GetScriptResult(string scriptUrl)
        {
            _logger.Trace($"Start GetScriptResult. scriptUrl = {scriptUrl}");
            JsonValue json;
            using (var wc = new WebClient())
            {
                var jsonString = wc.DownloadString(ScriptUrl);
                json = JsonValue.Parse(jsonString);
            }
            _logger.Trace($"End GetScriptResult. json[\"SiteStatus\"] = {json["SiteStatus"]}");
            return json["SiteStatus"].ToString() == "1";
        }
    }
}