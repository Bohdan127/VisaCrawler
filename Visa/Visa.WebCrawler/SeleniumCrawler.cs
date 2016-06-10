using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Threading;

namespace Visa.WebCrawler
{
    public class SeleniumCrawler
    {
        private string mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private string checkAvailableData = "ctl00_plhMain_lnkChkAppmntAvailability";       //Перевірити доступні для реєстрації дати в кол-центрі
        private string visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр
        private string visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private string buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити
        private string regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації

        public bool IsCompleted { get; private set; }
        public string OutData { get; private set; }



        public IEnumerable<int> DoWork(int serCenId, int visaCatId)
        {                                                 IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(mainUrl);
            IWebElement query = driver.FindElement(By.Id(checkAvailableData));
            query.Click();
            yield return 20;
            //Console.WriteLine("Please type Enter when you finish entering Capcha");
            //Console.ReadLine();
            query = driver.FindElement(By.Id(visaCity)).FindElement(By.CssSelector($"option[value='{serCenId}']"));
            query.Click();
            Thread.Sleep(1000);
            query = driver.FindElement(By.Id(visaCategory)).FindElement(By.CssSelector($"option[value='{visaCatId}']"));
            query.Click();
            yield return 60;
            //Console.WriteLine("Please type Enter when you finish entering Capcha");
            //Console.ReadLine();
            query = driver.FindElement(By.Id(buttonSubmit));
            query.Click();
            Thread.Sleep(1000);
            query = driver.FindElement(By.Id(regData));
            OutData = query.Text;
            //Console.ReadLine();
            driver.Quit();
            IsCompleted = true;
            yield return 100;
        }
    }
}
