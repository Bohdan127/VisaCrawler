using System.Collections;
using System.Resources;
using ToolsPortable;

namespace Visa.Resources
{
    public class ResManager : SortedList
    {
        private static ResManager _instance;
        public static string NoDataSource = "__NO_DATA__";

        public static ResManager Instance
            => _instance ?? (_instance = new ResManager());

        public static void RegisterResource(string sys,
            ResourceManager mgr)
        {
            if (!Instance.ContainsKey(sys))
                Instance.Add(sys,
                    mgr);
        }

        public static void RemoveResource(string sys)
        {
            Instance.Remove(sys);
        }

        public static void ClearResources() => Instance.Clear();

        public static string GetString(string code)
        {
            if (code == null)
                return NoDataSource;
            foreach (DictionaryEntry de in Instance)
            {
                var res = (de.Value as ResourceManager)?.GetString(code);
                if (res.IsNotBlank())
                    return res;
            }
            return NoDataSource;
        }

        public static string GetString(string sys,
            string code)
        {
            if (!Instance.ContainsKey(sys))
                return NoDataSource;

            var res = (Instance[sys] as ResourceManager)?.GetString(code);
            if (res.IsNotBlank())
                return res;
            return GetString(code);
        }

        public static string GetString(string sys,
            string code,
            params object[] sParams)
        {
            if (!Instance.ContainsKey(sys))
                return NoDataSource;

            var res = (Instance[sys] as ResourceManager)?.GetString(code);

            if (res.IsBlank())
                return NoDataSource;

            if (sParams != null
                && sParams.Length > 0)
                // ReSharper disable once AssignNullToNotNullAttribute
                return string.Format(res,
                    sParams);

            return NoDataSource;
        }
    }
}