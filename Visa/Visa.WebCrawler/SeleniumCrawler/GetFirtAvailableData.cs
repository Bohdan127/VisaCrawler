using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;
using ToolsPortable;
using Visa.WebCrawler.Interfaces;

namespace Visa.WebCrawler.SeleniumCrawler
{
    public class GetFirtAvailableData : ICrawler
    {
        private const string _mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private const string _checkAvailableData = "ctl00_plhMain_lnkChkAppmntAvailability";       //Перевірити доступні для реєстрації дати в кол-центрі
        private const string _visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр
        private const string _visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private const string _buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити
        private const string _regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації
        private const string _errorMessage = "ctl00_plhMain_lblMsg";
        private readonly IWebDriver _driver;


        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool Error { get; private set; }
        public string OutData { get; private set; }
        public bool Canceled { get; set; }

        public GetFirtAvailableData()
        {
            _logger.Trace("Start GetFirtAvailableData constructor");
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
            _driver = new FirefoxDriver(prof);
            _logger.Trace("End GetFirtAvailableData constructor");
        }

        public void PartOne()
        {
            _logger.Info($"Start PartOne. Error = {Error}");
            _driver.Navigate().GoToUrl(_mainUrl);
            Error = false;
            try
            {
                FindElementWithChecking(By.Id(_checkAvailableData)).Click();
                _logger.Info("PartOne._checkAvailableData Click");
                CheckForError();
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
            {
                if (Canceled)
                    _logger.Warn($"Canceled by User. Error  = {Error}");
                else
                {
                    _logger.Error($"NoSuchElementException with message = {ex.Message}");
                    Error = true;
                }
            }
            _logger.Info($"End PartOne. Error = {Error}");
        }

        private void CheckForError()
        {
            _logger.Info($"Start CheckForError. Error = {Error}");
            IWebElement erQuery = null;

            try
            {
                erQuery = FindElementWithChecking(By.Id(_errorMessage));
            }
            catch (NoSuchElementException ex)
            {
                _logger.Info($"Error element not found. Error ={Error}");
            }

            if (erQuery != null && erQuery.Text.IsNotBlank())
            {
                _logger.Error($"throw new NoSuchElementException. Reason erQuery.Text.IsNotBlank = {erQuery.Text}");
                throw new NoSuchElementException();
            }
            _logger.Info($"End CheckForError. Error = {Error}");
        }

        public void PartTwo(int serCenId, int visaCatId)
        {
            _logger.Info($"Start PartTwo. Error = {Error}. Service Center Id = {serCenId}. Visa Category Id = {visaCatId}");
            try
            {
                FindElementWithChecking(By.Id(_visaCity)).FindElement(By.CssSelector($"option[value='{serCenId}']")).Click();
                _logger.Info($"PartTwo. _visaCity option[value='{serCenId}'] Click");
                Thread.Sleep(1000);
                FindElementWithChecking(By.Id(_visaCategory)).FindElement(By.CssSelector($"option[value='{visaCatId}']")).Click();
                _logger.Info($"PartTwo. _visaCategory option[value='{visaCatId}'] Click");
                CheckForError();
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
            {
                if (Canceled)
                    _logger.Warn($"Canceled by User. Error  = {Error}");
                else
                {
                    _logger.Error($"NoSuchElementException with message = {ex.Message}");
                    Error = true;
                }
            }
            _logger.Info($"End PartTwo. Error = {Error}. Service Center Id = {serCenId}. Visa Category Id = {_visaCategory}");
        }

        public void PartThree()
        {
            _logger.Info($"Start PartThree. Error = {Error}");
            try
            {
                FindElementWithChecking(By.Id(_buttonSubmit)).Click();
                _logger.Info("PartThree. _buttonSubmit Click");
                Thread.Sleep(1000);
                OutData = FindElementWithChecking(By.Id(_regData)).Text;
                _logger.Info($"PartThree. OutData = {OutData}");
                CheckForError();
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
            {
                if (Canceled)
                    _logger.Warn($"Canceled by User. Error  = {Error}");
                else
                {
                    _logger.Error($"NoSuchElementException with message = {ex.Message}");
                    Error = true;
                }
            }
            _logger.Info($"End PartThree. Error = {Error}");
        }

        public IWebElement FindElementWithChecking(By by)
        {
            if (Canceled)
            {
                _logger.Info("Interrupted by Canceled flag. throw new WebDriverException");
                throw new WebDriverException();
            }
            return _driver?.FindElement(by);
        }

        public void CloseBrowser()
        {
            _logger.Trace("CloseBrowser");
            _driver?.Quit();
        }
    }
}
