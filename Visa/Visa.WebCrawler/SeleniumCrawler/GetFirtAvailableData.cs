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


        public bool Error { get; private set; }
        public string OutData { get; private set; }

        public GetFirtAvailableData()
        {
            _driver = new FirefoxDriver();
        }

        public void PartOne()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            Error = false;
            try
            {
                _query = _driver.FindElement(By.Id(_checkAvailableData));
                _query.Click();
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                Error = true;
            }
        }

        private void CheckForError()
        {
            var erQuery = _driver.FindElement(By.Id(_errorMessage));
            if (erQuery.Text.IsNotBlank())
                throw new NoSuchElementException();
        }

        public void PartTwo(int serCenId, int visaCatId)
        {
            try
            {
                _query = _driver.FindElement(By.Id(_visaCity)).FindElement(By.CssSelector($"option[value='{serCenId}']"));
                _query.Click();
                Thread.Sleep(1000);
                _query = _driver.FindElement(By.Id(_visaCategory)).FindElement(By.CssSelector($"option[value='{visaCatId}']"));
                _query.Click();
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                Error = true;
            }
        }

        public void PartThree()
        {
            try
            {
                _query = _driver.FindElement(By.Id(_buttonSubmit));
                _query.Click();
                Thread.Sleep(1000);
                _query = _driver.FindElement(By.Id(_regData));
                OutData = _query.Text;
                CheckForError();
            }
            catch (NoSuchElementException ex)
            {
                Error = true;
            }
        }

        public void CloseBrowser()
        {
            _driver?.Close();
        }
    }
}
