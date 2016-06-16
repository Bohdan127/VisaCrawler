using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;
using ToolsPortable;

namespace Visa.WebCrawler.SeleniumCrawler
{
    public class GetFirtAvailableData
    {
        private string _mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private string _checkAvailableData = "ctl00_plhMain_lnkChkAppmntAvailability";       //Перевірити доступні для реєстрації дати в кол-центрі
        private string _visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр
        private string _visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private string _buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити
        private string _regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації
        private string _errorMessage = "ctl00_plhMain_lblMsg";
        private IWebDriver _driver;
        private IWebElement _query;
        private int c = 0;


        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool Error { get; private set; }
        public string OutData { get; private set; }

        public GetFirtAvailableData()
        {
            _logger.Trace("Start GetFirtAvailableData constructor");
            _driver = new FirefoxDriver();
            _logger.Trace("End GetFirtAvailableData constructor");
        }

        public void PartOne()
        {
            _logger.Trace($"Start PartOne. Error = {Error}");
            _driver.Navigate().GoToUrl(_mainUrl);
            Error = false;
            try
            {
                _query = _driver.FindElement(By.Id(_checkAvailableData));
                _query.Click();
                _logger.Trace($"PartOne. _query.Click();");
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
            _logger.Trace($"End PartOne. Error = {Error}");
        }

        private void CheckForError()
        {
            _logger.Trace($"Start CheckForError. Error = {Error}");
            var erQuery = _driver.FindElement(By.Id(_errorMessage));
            if (erQuery.Text.IsNotBlank())
            {
                _logger.Error($"throw new NoSuchElementException. Reason erQuery.Text.IsNotBlank = {erQuery.Text}");
                throw new NoSuchElementException();
            }
            _logger.Trace($"End CheckForError. Error = {Error}");
        }

        public void PartTwo(int serCenId, int visaCatId)
        {
            _logger.Trace($"Start PartTwo. Error = {Error}. Service Center Id = {serCenId}. Visa Category Id = {_visaCategory}");
            try
            {
                _query = _driver.FindElement(By.Id(_visaCity)).FindElement(By.CssSelector($"option[value='{serCenId}']"));
                _query.Click();
                _logger.Trace($"PartTwo. _query.Click();");
                Thread.Sleep(1000);
                _query = _driver.FindElement(By.Id(_visaCategory)).FindElement(By.CssSelector($"option[value='{visaCatId}']"));
                _query.Click();
                _logger.Trace($"PartTwo. _query.Click();");
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
            _logger.Trace($"End PartTwo. Error = {Error}. Service Center Id = {serCenId}. Visa Category Id = {_visaCategory}");
        }

        public void PartThree()
        {
            _logger.Trace($"Start PartThree. Error = {Error}");
            try
            {
                _query = _driver.FindElement(By.Id(_buttonSubmit));
                _query.Click();
                _logger.Trace($"PartThree. _query.Click();");
                Thread.Sleep(1000);
                _query = _driver.FindElement(By.Id(_regData));
                OutData = _query.Text;
                _logger.Trace($"PartThree. OutData = {OutData}");
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
            _logger.Trace($"End PartThree. Error = {Error}");
        }

        public void CloseBrowser()
        {
            _logger.Trace("CloseBrowser");
            _driver?.Close();
        }
    }
}
