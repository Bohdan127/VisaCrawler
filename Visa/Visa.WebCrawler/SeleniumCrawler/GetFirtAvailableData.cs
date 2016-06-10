using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

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
        private IWebDriver _driver;
        private IWebElement _query;


        public int State { get; private set; }
        public string OutData { get; private set; }

        public GetFirtAvailableData()
        {
            _driver = new FirefoxDriver();
        }

        public void PartOne()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            _query = _driver.FindElement(By.Id(_checkAvailableData));
            _query.Click();
        }

        public void PartTwo(int serCenId, int visaCatId)
        {
            _query = _driver.FindElement(By.Id(_visaCity)).FindElement(By.CssSelector($"option[value='{serCenId}']"));
            _query.Click();
            Thread.Sleep(1000);
            _query = _driver.FindElement(By.Id(_visaCategory)).FindElement(By.CssSelector($"option[value='{visaCatId}']"));
            _query.Click();
        }

        public void PartThree()
        {
            _query = _driver.FindElement(By.Id(_buttonSubmit));
            _query.Click();
            Thread.Sleep(1000);
            _query = _driver.FindElement(By.Id(_regData));
            OutData = _query.Text;
        }
    }
}
