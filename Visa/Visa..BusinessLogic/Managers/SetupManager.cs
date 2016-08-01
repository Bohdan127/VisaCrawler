using NLog;
using System;
using System.IO;
using System.Xml.Serialization;
using Visa.BusinessLogic.SVN_Model;

namespace Visa.BusinessLogic.Managers
{
    public static class SetupManager
    {
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private static SetupOptions _options;

        public static bool RefreshNeeded { get; set; }

        public static SetupOptions GetOptions()
        {
            _logger.Trace($"Start GetOptions RefreshNeeded = {RefreshNeeded}");
            var xs = new XmlSerializer(typeof (SetupOptions));
            if (_options == null || RefreshNeeded)
            {
                _logger.Info("Read current options from file");
                try
                {
                    using (var sr = new StreamReader(@".\SetupOptions.xml"))
                    {
                        _options = (SetupOptions) xs.Deserialize(sr);
                        _logger.Info(
                            @"Read successfully from file .\SetupOptions.xml");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("During getting Setup options error thrown");
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                    SetDefaultOptions();
                }
            }
            RefreshNeeded = false;
            _logger.Trace("End GetOptions");
            return _options;
        }

        public static void SaveOptions(SetupOptions options)
        {
            _logger.Trace("Start SaveOptions");
            var xs = new XmlSerializer(typeof (SetupOptions));
            try
            {
                TextWriter tw = new StreamWriter(@".\SetupOptions.xml");
                xs.Serialize(tw,
                    options);
            }
            catch (Exception ex)
            {
                _logger.Error("During saving Setup options error thrown");
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                SetDefaultOptions();
            }
            finally
            {
                RefreshNeeded = false;
                _options = options;
            }
            _logger.Trace("End SaveOptions");
        }

        private static void SetDefaultOptions()
        {
            _logger.Trace("_options set to default parameters");
            _options = new SetupOptions
            {
                CloseBrowser = true,
                RepeatIfCrash = true,
                Nationality = "219", //UKRAINE code
                Password = "QWE1@3ewq",
                PeopleCount = "1",
                ChildCount = "0",
                AvailabilityUrl = "http://wiknaopen.esy.es/visa/out.json"
            };
        }
    }
}