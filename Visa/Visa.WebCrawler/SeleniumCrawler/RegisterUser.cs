using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;
using ToolsPortable;
using Visa.Database;
using Visa.WebCrawler.Interfaces;

namespace Visa.WebCrawler.SeleniumCrawler
{
    public class RegisterUser : ICrawler
    {
        #region Members

        private const string visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private const string buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити          
        private const string visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр i Пункт Прийому Візових Анкетx
        private const string mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private const string registryId = "ctl00_plhMain_lnkSchApp"; // Призначити дату подачі документів
        private const string reason = "ctl00_plhMain_cboPurpose"; //Мета візиту
        private const string numOfApplicants = "ctl00_plhMain_tbxNumOfApplicants"; //Кількість заявників
        private const string numOfChildrens = "ctl00_plhMain_txtChildren"; //К-сть дітей вписаних у паспорт батьків
        private const string receiptNumber = "ctl00_plhMain_repAppReceiptDetails_ctl01_txtReceiptNumber";//Номер квитанції
        private const string buttonSubmitEmail = "ctl00_plhMain_btnSubmitDetails"; //Підтвердити імейл
        private const string email = "ctl00_plhMain_txtEmailID";
        private const string passForMail = "ctl00_plhMain_txtPassword";
        private const string errorMessage = "ctl00_plhMain_lblMsg"; //Ок текст - на коли можна зареєструватись
        private const string endPassportDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPPTEXPDT";//Дата закінчення терміну дії паспорту
        private const string statusField = "ctl00_plhMain_repAppVisaDetails_ctl01_cboTitle"; //Статус
        private const string personName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxFName"; //Ім'я
        private const string personLastName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxLName"; //Прізвище
        private const string personBirthday = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxDOB"; //Дата народження
        private const string returnDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxReturn"; //Дата повернення
        private const string nationality = "ctl00_plhMain_repAppVisaDetails_ctl01_cboNationality"; //Національність
        private const string availableData = "OpenDateAllocated"; //Будь ласка, оберіть Вільну дату
        private const string registryTime = "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot"; //Будь ласка, оберіть Час

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IWebDriver _driver;

        #endregion

        public bool Error { get; private set; }
        public string OutData { get; private set; }
        public bool Canceled { get; set; }

        public RegisterUser()
        {
            _logger.Trace("Start RegisterUser constructor");
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone", "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
            _driver = new FirefoxDriver(prof);
            _logger.Trace("End RegisterUser constructor");
        }

        public void PartOne(VisaDataSet.ClientDataRow dataRow, int serCenId)
        {
            _logger.Info($"Start PartOne. Error = {Error}. Service Center Id = {serCenId}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            try
            {
                _driver.Navigate().GoToUrl(mainUrl);
                Thread.Sleep(2000);
                {
                    var query = FindElementWithChecking(By.Id(registryId));
                    _logger.Info("PartOne. registryId Click");
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(visaCity))
                        .FindElement(By.CssSelector($"option[value=\"{serCenId}\"]"))
                        .Click();
                    _logger.Info($"PartOne. visaCity option[value={serCenId}]  Click");
                }
                {
                    //always be 1 - Подача документів
                    FindElementWithChecking(By.Id(reason))
                        .FindElement(By.CssSelector("option[value='1']"))
                        .Click();
                    _logger.Info("PartOne. reason option[value='1'] Click");
                }
                {
                    FindElementWithChecking(By.Id(buttonSubmit))
                        .Click();
                    _logger.Info("PartOne. buttonSubmit Click");
                }
                Thread.Sleep(2000);
                try
                {
                    var query = FindElementWithChecking(By.Id(numOfApplicants));
                    _logger.Info($"PartOne. numOfApplicants Clear and set {dataRow.PeopleCount}");
                    query.Clear();
                    query.SendKeys(dataRow.PeopleCount);
                }
                catch (NoSuchElementException ex)
                {
                    Thread.Sleep(2000);
                    var query = FindElementWithChecking(By.Id(numOfApplicants));
                    _logger.Info($"PartOne. numOfApplicants Clear and set {dataRow.PeopleCount}");
                    query.Clear();
                    query.SendKeys(dataRow.PeopleCount);
                }
                {
                    var query = FindElementWithChecking(By.Id(numOfChildrens));
                    _logger.Info($"PartOne. numOfChildrens Clear and set {dataRow.ChildsCount}");
                    query.Clear();
                    query.SendKeys(dataRow.ChildsCount);
                }
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

        public void PartOneAndHalf(int visaCatId)
        {
            _logger.Info($"Start PartOneAndHalf. Error = {Error}. Visa Category Id = {visaCatId}.");
            try
            {
                FindElementWithChecking(By.Id(visaCategory))
                    .FindElement(By.CssSelector($"option[value=\"{visaCatId}\"]"))
                    .Click();
                _logger.Info($"PartOne. visaCategory option[value={visaCatId}]  Click");
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

        public void PartTwo(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start PartTwo. Error = {Error}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            try
            {
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(buttonSubmit)).Click();
                    _logger.Info("PartTwo. buttonSubmit Click");
                }
                Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(receiptNumber))
                        .SendKeys(dataRow.NumberOfReceipt);
                    _logger.Info($"PartTwo. receiptNumber set text {dataRow.NumberOfReceipt}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(buttonSubmit))
                        .Click();
                    _logger.Info("PartTwo. buttonSubmit Click");
                }
                CheckForError();
                Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(email))
                        .SendKeys(dataRow.Email);
                    _logger.Info($"PartTwo. email set text {dataRow.Email}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(passForMail))
                        .SendKeys(dataRow.Password);
                    _logger.Info($"PartTwo. passForMail set text {dataRow.Password}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(buttonSubmitEmail))
                        .Click();
                    _logger.Info("PartTwo. buttonSubmitEmail Click");
                }
                Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(endPassportDate))
                        .SendKeys(dataRow.EndPassportDate.ToShortDateString().Replace(".", "/"));
                    _logger.Info($"PartTwo. endPassportDate set text {dataRow.EndPassportDate.ToShortDateString().Replace(".", "/")}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(statusField))
                        .FindElement(By.CssSelector($"option[value=\"{dataRow.Status}\"]"))
                        .Click();
                    _logger.Info($"PartTwo. statusField option[value={dataRow.Status}] Click");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(personName))
                        .SendKeys(dataRow.Name);
                    _logger.Info($"PartTwo. personName set text {dataRow.Name}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(personLastName))
                        .SendKeys(dataRow.LastName);
                    _logger.Info($"PartTwo. personLastName set text {dataRow.LastName}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(personBirthday))
                        .SendKeys(dataRow.Birthday.ToShortDateString().Replace(".", "/"));
                    _logger.Info($"PartTwo. personBirthday set text {dataRow.Birthday.ToShortDateString().Replace(".", "/")}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(returnDate))
                        .SendKeys(dataRow.ReturnData.ToShortDateString().Replace(".", "/"));
                    _logger.Info($"PartTwo. returnDate set text {dataRow.ReturnData.ToShortDateString().Replace(".", "/")}");
                }
                //Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(nationality))
                        .FindElement(By.CssSelector($"option[value=\"{dataRow.Nationality}\"]"))
                        .Click();
                    _logger.Info($"PartTwo. nationality option[value=\"{dataRow.Nationality}\"] Click");
                }
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
            _logger.Info($"End PartTwo. Error = {Error}");
        }

        public void PartThree()
        {
            _logger.Info($"Start PartThree. Error = {Error}.");
            try
            {
                FindElementWithChecking(By.Id(buttonSubmit))
                    .Click();
                _logger.Info("PartThree. buttonSubmit Click");
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
            _logger.Info($"End PartThree. Error = {Error}");
        }

        public void PartFour(VisaDataSet.ClientDataRow dataRow)

        {
            _logger.Info($"Start PartFour. Error = {Error}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            try
            {
                Thread.Sleep(2000);
                {
                    var queryCollection = _driver.FindElements(By.ClassName(availableData));
                    _logger.Info($"PartFour. maxDate = { dataRow.RegistryFom.Day}. minDate = {dataRow.RegistryTo.Day}");
                    foreach (var element in queryCollection)
                    {
                        var date = element.Text.ConvertToIntOrNull();

                        if (date == null || date.Value > dataRow.RegistryTo.Day || date.Value < dataRow.RegistryFom.Day) continue;

                        _logger.Info($"PartFour. date.Value = {date.Value} element Click");
                        OutData = date.Value.ToString();
                        element.Click();
                        break;
                    }
                }
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

        public void PartFive()
        {
            _logger.Info($"Start PartFive. Error = {Error}.");
            try
            {
                OutData = string.Empty;
                Thread.Sleep(2000);
                {
                    FindElementWithChecking(By.Id(registryTime)).Click();
                    _logger.Info("PartFive. registryTime Click");
                }
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
            _logger.Info($"End PartFive. Error = {Error}");
        }

        private void CheckForError()
        {
            _logger.Info($"Start CheckForError. Error = {Error}");
            IWebElement erQuery = null;

            try
            {
                erQuery = FindElementWithChecking(By.Id(errorMessage));
            }
            catch (NoSuchElementException ex)
            {
                _logger.Info($"Error element not found. Error ={Error}");
            }

            if (erQuery != null && erQuery.Text.IsNotBlank())
            {
                OutData = erQuery.Text;
                _logger.Error($"throw new NoSuchElementException. Reason erQuery.Text.IsNotBlank = {erQuery.Text}");
                throw new NoSuchElementException();
            }
            _logger.Info($"End CheckForError. Error = {Error}");
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
