using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        //main link - https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=
        private static string mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private static string checkAvailableData = "ctl00_plhMain_lnkChkAppmntAvailability";       //Перевірити доступні для реєстрації дати в кол-центрі
        private static string visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр
        private static string visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private static string buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити                                      private static string regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації

        static void Main(string[] args)
        {
            FirefoxDriver driver = new FirefoxDriver();
            //new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"),
            //    new FirefoxProfile(@"C:\Users\BohdanPC\AppData\Roaming\Mozilla\Firefox\Profiles\0igfgrhe.default"));
            driver.Navigate().GoToUrl(mainUrl);
            Thread.Sleep(3000);
            IWebElement query = driver.FindElement(By.Id(checkAvailableData));
            query.Click();
            Console.WriteLine("Please type Enter when you finish entering Capcha");
            Console.ReadLine();
            query = driver.FindElement(By.Id(visaCity)).FindElement(By.CssSelector("option[value='5']"));
            query.Click();
            Thread.Sleep(3000);
            query = driver.FindElement(By.Id(visaCategory)).FindElement(By.CssSelector("option[value='235']"));
            query.Click();
            Console.WriteLine("Please type Enter when you finish entering Capcha");
            Console.ReadLine();
            query = driver.FindElement(By.Id(buttonSubmit));
            query.Click();
            Thread.Sleep(3000);
            //query = driver.FindElement(By.Id(regData));
            Console.WriteLine(query.Text);
            Console.ReadLine();
            driver.Quit();
        }
    }
}
