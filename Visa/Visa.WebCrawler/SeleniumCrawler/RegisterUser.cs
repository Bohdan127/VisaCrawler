using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Globalization;
using System.Threading;
using ToolsPortable;
using Visa.Database;
using Visa.Resources;
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
        private const string btnCancel = "ctl00_plhMain_btnCancel";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IWebDriver _driver;

        #endregion

        public bool Error { get; set; }
        public bool Rollback { get; set; }
        public string OutData { get; set; }
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

        public void GoToUrl()
        {
            _logger.Info($"Start GoToUrl. Error = {Error}.");
            try
            {
                _driver.Navigate().GoToUrl(mainUrl);
                Thread.Sleep(2000);
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
            _logger.Info($"End GoToUrl. Error = {Error}.");
        }

        public void StartRegistration()
        {
            _logger.Info($"Start StartRegistration. Error = {Error}. ");
            try
            {
                var query = FindElementWithChecking(By.Id(registryId));
                _logger.Info("PartOne. registryId Click");
                query.Click();
                Thread.Sleep(2000);
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
            _logger.Info($"End StartRegistration. Error = {Error}. ");
        }

        public void SelectCityAndReason(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start SelectCityAndReason. Error = {Error}. ");
            try
            {
                FindElementWithChecking(By.Id(visaCity))
                    .FindElement(By.CssSelector($"option[value=\"{dataRow.VisaCity}\"]"))
                    .Click();
                _logger.Info($"SelectCityAndReason. visaCity option[value={dataRow.VisaCity}]  Click");
                //always be 1 - Подача документів
                FindElementWithChecking(By.Id(reason))
                    .FindElement(By.CssSelector("option[value='1']"))
                    .Click();
                _logger.Info("SelectCityAndReason. reason option[value='1'] Click");
                FindElementWithChecking(By.Id(buttonSubmit))
                    .Click();
                _logger.Info("SelectCityAndReason. buttonSubmit Click");
                Thread.Sleep(2000);
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
            _logger.Info($"End SelectCityAndReason. Error = {Error}. ");
        }

        /// <summary>
        /// Require Captcha after it
        /// </summary>
        public void ProvidePeopleCount(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start ProvidePeopleCount. Error = {Error}. ");
            try
            {
                var query = FindElementWithChecking(By.Id(numOfApplicants));
                _logger.Info($"ProvidePeopleCount. numOfApplicants Clear and set {dataRow.PeopleCount}");

                if (dataRow.PeopleCount != "1")
                {
                    query.Clear();
                    query.SendKeys(dataRow.PeopleCount);
                }

                query = FindElementWithChecking(By.Id(numOfChildrens));
                _logger.Info($"ProvidePeopleCount. numOfChildrens Clear and set {dataRow.ChildsCount}");

                if (dataRow.ChildsCount != "0")
                {
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
            _logger.Info($"End ProvidePeopleCount. Error = {Error}");
        }

        /// <summary>
        /// Select visa Type and check for date
        /// </summary>
        /// <returns>False - if data was to hight or not available, True - data is good for using</returns>
        public bool SelectVisaTypeAndCheckForDate(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start SelectVisaTypeAndCheckForDate. Error = {Error}. ");
            var bRes = false;
            try
            {
                FindElementWithChecking(By.Id(visaCategory))
                    .FindElement(By.CssSelector($"option[value=\"{dataRow.VisaType}\"]"))
                    .Click();
                _logger.Info($"SelectVisaTypeAndCheckForDate. visaCategory option[value={dataRow.VisaType}]  Click");

                Thread.Sleep(2000);

                var infoText = FindElementWithChecking(By.Id(errorMessage)).Text;

                try
                {
                    var availableDate = DateTime.ParseExact(infoText, "d.MMM.yyyy", CultureInfo.CurrentCulture);
                    _logger.Info($"First date for Registration => {availableDate}");
                    if (availableDate < dataRow.RegistryTo)
                    {
                        _logger.Info($"availableDate < dataRow.RegistryTo => {availableDate.ToShortDateString()} < {dataRow.RegistryTo}");
                        bRes = true;
                    }
                    else
                    {
                        OutData = string.Format(ResManager.GetString(ResKeys.DateIncorrect_Message),
                            availableDate.ToShortDateString());
                    }
                }
                catch (Exception ex)
                {
                    OutData = infoText;
                    _logger.Warn(ex.Message);
                    _logger.Warn(ex.StackTrace);
                }

                if (bRes)
                {
                    FindElementWithChecking(By.Id(buttonSubmit)).Click();
                    _logger.Info("SelectVisaTypeAndCheckForDate. buttonSubmit Click");
                    Thread.Sleep(2000);
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
            _logger.Info($"End SelectVisaTypeAndCheckForDate. Error = {Error}");
            return bRes;
        }

        public void BackToCityAndReason()
        {
            _logger.Info($"Start BackToCityAndReason. Error = {Error}");
            try
            {
                FindElementWithChecking(By.Id(btnCancel)).Click();
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
            _logger.Info($"End BackToCityAndReason. Error = {Error}");
        }

        public void Receipt(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start Receipt. Error = {Error}.");
            try
            {
                FindElementWithChecking(By.Id(receiptNumber))
                    .SendKeys(dataRow.NumberOfReceipt);
                _logger.Info($"Receipt. receiptNumber set text {dataRow.NumberOfReceipt}");


                FindElementWithChecking(By.Id(buttonSubmit))
                    .Click();
                _logger.Info("Receipt. buttonSubmit Click");

                Thread.Sleep(2000);
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
            _logger.Info($"End Receipt. Error = {Error}");
        }

        /// <summary>
        ///  Require Captcha after it, also we need to share additional message for user
        /// </summary>
        public void ClientData(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start ClientData. Error = {Error}");
            try
            {
                FindElementWithChecking(By.Id(email))
                    .SendKeys(dataRow.Email);
                _logger.Info($"ClientData. email set text {dataRow.Email}");

                FindElementWithChecking(By.Id(passForMail))
                        .SendKeys(dataRow.Password);
                _logger.Info($"ClientData. passForMail set text {dataRow.Password}");

                FindElementWithChecking(By.Id(buttonSubmitEmail))
                        .Click();
                _logger.Info("ClientData. buttonSubmitEmail Click");

                Thread.Sleep(2000);
                FindElementWithChecking(By.Id(endPassportDate))
                    .SendKeys(dataRow.EndPassportDate.ToShortDateString().Replace(".", "/"));
                _logger.Info($"ClientData. endPassportDate set text {dataRow.EndPassportDate.ToShortDateString().Replace(".", "/")}");

                FindElementWithChecking(By.Id(statusField))
                        .FindElement(By.CssSelector($"option[value=\"{dataRow.Status}\"]"))
                        .Click();
                _logger.Info($"ClientData. statusField option[value={dataRow.Status}] Click");

                FindElementWithChecking(By.Id(personName))
                    .SendKeys(dataRow.Name);
                _logger.Info($"ClientData. personName set text {dataRow.Name}");

                FindElementWithChecking(By.Id(personLastName))
                    .SendKeys(dataRow.LastName);
                _logger.Info($"ClientData. personLastName set text {dataRow.LastName}");

                FindElementWithChecking(By.Id(personBirthday))
                    .SendKeys(dataRow.Birthday.ToShortDateString().Replace(".", "/"));
                _logger.Info($"ClientData. personBirthday set text {dataRow.Birthday.ToShortDateString().Replace(".", "/")}");

                FindElementWithChecking(By.Id(returnDate))
                    .SendKeys(dataRow.ReturnData.ToShortDateString().Replace(".", "/"));
                _logger.Info($"ClientData. returnDate set text {dataRow.ReturnData.ToShortDateString().Replace(".", "/")}");

                FindElementWithChecking(By.Id(nationality))
                    .FindElement(By.CssSelector($"option[value=\"{dataRow.Nationality}\"]"))
                    .Click();
                _logger.Info($"ClientData. nationality option[value=\"{dataRow.Nationality}\"] Click");
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
            _logger.Info($"End ClientData. Error = {Error}");
        }


        #region Is Not Used Now but will be in the future
        //public void PartThree()
        //{
        //    _logger.Info($"Start PartThree. Error = {Error}.");
        //    try
        //    {
        //        FindElementWithChecking(By.Id(buttonSubmit))
        //            .Click();
        //        _logger.Info("PartThree. buttonSubmit Click");
        //    }
        //    catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
        //    {
        //        _logger.Error($"NoSuchElementException with message = {ex.Message}");
        //        Error = true;
        //    }
        //    _logger.Info($"End PartThree. Error = {Error}");
        //}

        //public void PartFour(VisaDataSet.ClientDataRow dataRow)

        //{
        //    _logger.Info($"Start PartFour. Error = {Error}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
        //    try
        //    {
        //        Thread.Sleep(2000);
        //        {
        //            var queryCollection = _driver.FindElements(By.ClassName(availableData));
        //            _logger.Info($"PartFour. maxDate = { dataRow.RegistryFom.Day}. minDate = {dataRow.RegistryTo.Day}");
        //            foreach (var element in queryCollection)
        //            {
        //                var date = element.Text.ConvertToIntOrNull();

        //                if (date == null || date.Value > dataRow.RegistryTo.Day || date.Value < dataRow.RegistryFom.Day) continue;

        //                _logger.Info($"PartFour. date.Value = {date.Value} element Click");
        //                OutData = date.Value.ToString();
        //                element.Click();
        //                break;
        //            }
        //        }
        //        CheckForError();
        //    }
        //    catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
        //    {
        //        if (Canceled)
        //            _logger.Warn($"Canceled by User. Error  = {Error}");
        //        else
        //        {
        //            _logger.Error($"NoSuchElementException with message = {ex.Message}");
        //            Error = true;
        //        }
        //    }
        //    _logger.Info($"End PartThree. Error = {Error}");
        //}

        //public void PartFive()
        //{
        //    _logger.Info($"Start PartFive. Error = {Error}.");
        //    try
        //    {
        //        OutData = string.Empty;
        //        Thread.Sleep(2000);
        //        {
        //            FindElementWithChecking(By.Id(registryTime)).Click();
        //            _logger.Info("PartFive. registryTime Click");
        //        }
        //    }
        //    catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException)
        //    {
        //        if (Canceled)
        //            _logger.Warn($"Canceled by User. Error  = {Error}");
        //        else
        //        {
        //            _logger.Error($"NoSuchElementException with message = {ex.Message}");
        //            Error = true;
        //        }
        //    }
        //    _logger.Info($"End PartFive. Error = {Error}");
        //}
        #endregion

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
