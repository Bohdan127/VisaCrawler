//#define GoWithoutDates
//#define UseDefaultSelenium

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
#if UseDefaultSelenium
using Selenium;
#endif
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NLog;
using ToolsPortable;
using Visa.Database;
using Visa.Resources;
using Visa.WebCrawler.Interfaces;

namespace Visa.WebCrawler.SeleniumCrawler
{
    public class RegisterUser : ICrawler
    {
        public RegisterUser()
        {
            _logger.Trace("Start RegisterUser constructor");
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone",
                "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional",
                "about:blank");
            RegistrarionDateAvailability = DialogResult.None;
            _driver = new FirefoxDriver(prof);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(15));
            _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromMinutes(15));
            Error = false;
            Canceled = false;
#if UseDefaultSelenium
            int port = 4444; //2310;
            ISelenium selenium = new DefaultSelenium("localhost", port, "*firefox C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe", mainUrl);
#endif
            _logger.Trace("End RegisterUser constructor");
        }

        public void GoToUrl()
        {
            _logger.Info($"Start GoToUrl. Error = {Error}.");

            _driver.Navigate().GoToUrl(mainUrl);

            _logger.Info($"End GoToUrl. Error = {Error}.");
        }

        public void StartRegistration()
        {
            _logger.Info($"Start StartRegistration. Error = {Error}. ");
            var query = FindElementWithChecking(By.Id(registryId));
            _logger.Info("PartOne. registryId Click");
            try
            {
                query.Click();
            }
            catch (Exception ex)
            {
                _logger.Error("Error during click in StartRegistration");
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            _logger.Info($"End StartRegistration. Error = {Error}. ");
        }

        public void SelectCityAndReason(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start SelectCityAndReason. Error = {Error}. ");
            FindElementWithChecking(By.Id(visaCity))
                .FindElement(
                    By.CssSelector($"option[value=\"{dataRow.VisaCity}\"]"))
                .Click();
            _logger.Info(
                $"SelectCityAndReason. visaCity option[value={dataRow.VisaCity}]  Click");
            //always be 1 - Подача документів
            FindElementWithChecking(By.Id(reason))
                .FindElement(By.CssSelector("option[value='1']"))
                .Click();
            _logger.Info(
                "SelectCityAndReason. reason option[value='1'] Click");
            _logger.Info($"End SelectCityAndReason. Error = {Error}. ");
        }

        /// <summary>
        ///     Require Captcha after it
        /// </summary>
        public void ProvidePeopleCount(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start ProvidePeopleCount. Error = {Error}. ");
            var query = FindElementWithChecking(By.Id(numOfApplicants));
            _logger.Info(
                $"ProvidePeopleCount. numOfApplicants Clear and set {dataRow.PeopleCount}");

            if (dataRow.PeopleCount != "1")
            {
                query.Clear();
                query.SendKeys(dataRow.PeopleCount);
            }

            query = FindElementWithChecking(By.Id(numOfChildrens));
            _logger.Info(
                $"ProvidePeopleCount. numOfChildrens Clear and set {dataRow.ChildsCount}");

            if (dataRow.ChildsCount != "0")
            {
                query.Clear();
                query.SendKeys(dataRow.ChildsCount);
            }
            _logger.Info($"End ProvidePeopleCount. Error = {Error}");
        }

        /// <summary>
        ///     Select visa Type and check for date
        /// </summary>
        /// <returns>False - if data was to hight or not available, True - data is good for using and Capthca is needed</returns>
        public void SelectVisaType(
            VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start SelectVisaType. Error = {Error}. ");
            FindElementWithChecking(By.Id(visaCategory))
                .FindElement(
                    By.CssSelector($"option[value=\"{dataRow.VisaType}\"]"))
                .Click();
            _logger.Info("SelectVisaType. visaCategory " +
                         $"option[value={dataRow.VisaType}]  Click");

            _logger.Info($"End SelectVisaType. Error = {Error}");
        }

        public bool CheckDate(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start CheckDate. Error = {Error}. ");
            var bRes = false;
            try
            {
                var infoText = FindElementWithChecking(By.Id(errorMessage)).Text;
                if (infoText.Contains(capchaNotFilledMessage)) //"The image you selected not match"
                {
                    FillCapchaFailed = true;
                    return false;
                }

                try
                {
                    var availableDate = DateTime.ParseExact(infoText,
                        "d.MMM.yyyy",
                        CultureInfo.CurrentCulture);
                    _logger.Info($"First date for Registration => {availableDate}");
                    _logger.Info($"dataRow.RegistryFom => { dataRow.RegistryFom}");
                    _logger.Info($"dataRow.RegistryTo => { dataRow.RegistryTo}");
#if GoWithoutDates
                    if (dataRow.RegistryFom <= availableDate
                        && availableDate <= dataRow.RegistryTo)
#else
                    if (availableDate <= dataRow.RegistryTo)
#endif
                    {
                        _logger.Info("dataRow.RegistryFom <= availableDate && availableDate <= dataRow.RegistryTo");
                        bRes = true;
                    }
                    else
                    {
                        OutData =
                            string.Format(
                                ResManager.GetString(
                                    ResKeys.DateIncorrect_Message),
                                availableDate.ToShortDateString());
                    }
                }
                catch (Exception ex)
                {
                    OutData = infoText;
                    _logger.Warn(ex.Message);
                    _logger.Warn(ex.StackTrace);
                }
            }
            catch (Exception ex)//todo if Ivan add one new property to see inner detailed True/False sub-result, we should use it here and run this function from RunNextStep
                when (ex is NoSuchElementException || ex is WebDriverException)
            {
                if (Canceled)
                    _logger.Warn($"Canceled by User. Error  = {Error}");
                else
                {
                    _logger.Error(
                        $"NoSuchElementException/WebDriverException with message = {ex.Message}");
                    Error = true;
                }
            }
            _logger.Info($"End CheckDate. Error = {Error}. ");
            return bRes;
        }

        public void BackToCityAndReason()
        {
            _logger.Info($"Start BackToCityAndReason. Error = {Error}");
            var button = FindElementWithChecking(By.Id(btnCancel));
            try
            {
                //Thread.Sleep(4000);
                button.Click();
                _logger.Info("BackToCityAndReason. buttonBack Clicked");
            }
            catch (Exception ex)
            {
                _logger.Error("Error during click in BackToCityAndReason");
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            _logger.Info($"End BackToCityAndReason. Error = {Error}");
        }

        public void Receipt(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start Receipt. Error = {Error}.");
            var txtBox = FindElementWithChecking(By.Id(receiptNumber));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.NumberOfReceipt);
            _logger.Info(
                $"Receipt. receiptNumber set text {dataRow.NumberOfReceipt}");
            _logger.Info($"End Receipt. Error = {Error}");
        }

        /// <summary>
        ///     Require Captcha after it, also we need to share additional message for user
        /// </summary>
        public void EmailAndPassword(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Trace($"Start EmailAndPassword. Error = {Error}");
            var txtBox = FindElementWithChecking(By.Id(email));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Email);
            _logger.Info($"ClientData. email set text {dataRow.Email}");

            txtBox = FindElementWithChecking(By.Id(passForMail));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Password);
            _logger.Info(
                $"ClientData. passForMail set text {dataRow.Password}");
            _logger.Trace($"End EmailAndPassword. Error = {Error}");
        }

        public void ClientData(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Trace($"Start ClientData. Error = {Error}");
            var txtBox = FindElementWithChecking(By.Id(endPassportDate));
            txtBox.Clear();
            txtBox.SendKeys(
                    dataRow.EndPassportDate.ToShortDateString().Replace(".",
                        "/"));
            _logger.Info(
                $"ClientData. endPassportDate set text {dataRow.EndPassportDate.ToShortDateString().Replace(".", "/")}");

            FindElementWithChecking(By.Id(statusField))
                .FindElement(
                    By.CssSelector($"option[value=\"{dataRow.Status}\"]"))
                .Click();
            _logger.Info(
                $"ClientData. statusField option[value={dataRow.Status}] Click");

            txtBox = FindElementWithChecking(By.Id(personName));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Name);
            _logger.Info($"ClientData. personName set text {dataRow.Name}");

            txtBox = FindElementWithChecking(By.Id(personLastName));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.LastName);
            _logger.Info(
                $"ClientData. personLastName set text {dataRow.LastName}");

            txtBox = FindElementWithChecking(By.Id(personBirthday));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Birthday.ToShortDateString().Replace(".",
                   "/"));
            _logger.Info(
                $"ClientData. personBirthday set text {dataRow.Birthday.ToShortDateString().Replace(".", "/")}");

            txtBox = FindElementWithChecking(By.Id(returnDate));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.ReturnData.ToShortDateString()
                .Replace(".",
                    "/"));
            _logger.Info(
                $"ClientData. returnDate set text {dataRow.ReturnData.ToShortDateString().Replace(".", "/")}");

            FindElementWithChecking(By.Id(nationality))
                .FindElement(
                    By.CssSelector(
                        $"option[value=\"{dataRow.Nationality}\"]"))
                .Click();
            _logger.Info(
                $"ClientData. nationality option[value=\"{dataRow.Nationality}\"] Click");
            _logger.Trace($"End ClientData. Error = {Error}");
        }

        #region Members

        private const string visaCategory = "ctl00_plhMain_cboVisaCategory";
        //Візова категорія

        private const string buttonSubmit = "ctl00_plhMain_btnSubmit";
        //Підтвердити

        private const string visaCity = "ctl00_plhMain_cboVAC";
        //Візовий Сервіс Центр i Пункт Прийому Візових Анкетx

        private const string mainUrl =
            "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";

        private const string registryId = "ctl00_plhMain_lnkSchApp";
        // Призначити дату подачі документів

        private const string reason = "ctl00_plhMain_cboPurpose"; //Мета візиту

        private const string numOfApplicants =
            "ctl00_plhMain_tbxNumOfApplicants"; //Кількість заявників

        private const string numOfChildrens = "ctl00_plhMain_txtChildren";
        //К-сть дітей вписаних у паспорт батьків

        private const string receiptNumber =
            "ctl00_plhMain_repAppReceiptDetails_ctl01_txtReceiptNumber";

        //Номер квитанції

        private const string buttonSubmitEmail =
            "ctl00_plhMain_btnSubmitDetails"; //Підтвердити імейл

        private const string email = "ctl00_plhMain_txtEmailID";
        private const string passForMail = "ctl00_plhMain_txtPassword";

        private const string errorMessage = "ctl00_plhMain_lblMsg";
        //Ок текст - на коли можна зареєструватись

        private const string endPassportDate =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPPTEXPDT";

        //Дата закінчення терміну дії паспорту

        private const string statusField =
            "ctl00_plhMain_repAppVisaDetails_ctl01_cboTitle"; //Статус

        private const string personName =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxFName"; //Ім'я

        private const string personLastName =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxLName"; //Прізвище

        private const string personBirthday =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxDOB"; //Дата народження

        private const string returnDate =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxReturn"; //Дата повернення

        private const string nationality =
            "ctl00_plhMain_repAppVisaDetails_ctl01_cboNationality";

        //Національність

        private const string availableData = "OpenDateAllocated";
        //Будь ласка, оберіть Вільну дату

        private const string registryTime =
            "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot"; //Будь ласка, оберіть Час

        private const string btnCancel = "ctl00_plhMain_btnCancel";

        private const string aspnetForm = "aspnetForm";

        private const string capchaNotFilledMessage = "The image you selected not match";

        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private readonly FirefoxDriver _driver;
#if UseDefaultSelenium
        private Selenium.ISelenium selenium;
#endif
        #endregion Members

        #region Properties

        public DialogResult RegistrarionDateAvailability { get; set; }

        public bool ValidationError { get; set; }

        public bool Error { get; set; }

        public string OutData { get; set; }

        public bool Canceled { get; set; }

        public bool GetFirstDateScroll { get; set; }

        public bool FillCapchaFailed { get; set; }

        public bool IsServerDown
        {
            get
            {
                _logger.Trace("Start IsServerDown");
                try
                {
                    if (_driver.Title.IsNotNullOrEmpty() && _driver.Title.Contains("Session Expired"))
                    {
                        ValidationError = true; // we need full restart
                        _logger.Error("IsServerDown => True. Title.Contains(\"Session Expired\")");
                        return true;
                    }
                    if (_driver.Title.IsNotNullOrEmpty() && !_driver.Title.Contains("Poland Visa"))
                    {
                        _logger.Error("IsServerDown => True. Title not Contains(\"Poland Visa\")");
                        return true;
                    }
                    var pageBody = _driver.FindElement(By.TagName("body"));
                    if (pageBody.Text.Contains("The service is unavailable"))
                    {
                        _logger.Error("IsServerDown => True");
                        return true;
                    }
                    if (pageBody.Text.Contains("We are sorry for the inconvenience"))
                    {
                        ValidationError = true; // we need full restart
                        _logger.Error("IsServerDown => True. page.Contains(\"We are sorry for the inconvenience\")");
                        return true;
                    }
                }
                catch (Exception ex)
                    when (ex is NoSuchElementException || ex is WebDriverException)
                {
                    _logger.Error($"IsServerDown Error: ex.Message={ex.Message}");
                    _logger.Error("IsServerDown => True");
                    return true;
                }
                _logger.Info("IsServerDown => False");
                return false;
            }
        }

        #endregion Properties

        #region Help Functions

        public void PressSubmitButton(bool emailSubmit = false)
        {
            _logger.Info($"Start PressSubmitButton. Error = {Error}. ");
            var submit = emailSubmit
                ? FindElementWithChecking(By.Id(buttonSubmitEmail))
                : FindElementWithChecking(By.Id(buttonSubmit));
            try
            {
                submit.Click();
                _logger.Trace("PressSubmitButton. buttonSubmit Click");
            }
            catch (Exception ex)
            {
                _logger.Error("Error during click in PressSubmitButton");
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
            _logger.Info($"End PressSubmitButton. Error = {Error}. ");
        }

        public void RunNextStep(
            Action regAction)
        {
            _logger.Trace($"Start RunNextStep. Error = {Error}.");

            if (IsServerDown)
            {
                Error = true;
                _logger.Warn($"End RunNextStep. Error = {Error}.");
                return;
            }

            try
            {
                CheckForError();
                regAction();
            }
            catch (Exception ex)
                when (
                    ex is NoSuchElementException || ex is WebDriverException
                    )
            {
                if (Canceled)
                    _logger.Warn($"Canceled by User. Error  = {Error}");
                else
                {
                    _logger.Error(
                        $"NoSuchElementException/WebDriverException with message = {ex.Message}");
                    Error = true;
                }
            }
            _logger.Trace($"End RunNextStep. Error = {Error}.");
        }

        public void ReloadPage()
        {
            _logger.Info("Start ReloadPage.");
            var result = "OK";
            var shouldAlertAccept = (0 != String.Compare(_driver.Url, mainUrl, StringComparison.Ordinal));
            try
            {
                _driver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(
                        $"Navigate().RefreshException with message = {ex.Message}");
                if (!ex.Message.StartsWith("Unexpected modal dialog"))
                {
                    result = "ERROR";
                    shouldAlertAccept = false;
                }
            }
            if (shouldAlertAccept)
            {
                Thread.Sleep(500);
                try
                {
                    if (_driver.SwitchTo().Alert() != null)
                        _driver.SwitchTo().Alert().Accept();
                }
                catch (NoAlertPresentException ex)
                {
                    _logger.Trace(// Alert not present
                            $"SwitchTo().NoAlertPresentException with message = {ex.Message}");
                }
            }
            _logger.Info($"End ReloadPage. Status={result}");
        }

        /// <summary>
        ///     Gets "Name of Current Page" + '.' + "Name of Page for post requests".
        ///     For Example: "AppSchedulingGetInfo.AppSchedulingReceiptDetails"
        /// </summary>
        public string GetCurrentPage()
        {
            string vp="", pp="";
            try
            {
                vp = _driver.Url.Substring(106, 30);
                vp = vp.Substring(0, vp.IndexOf('.'));
            }
            catch { }
            try
            {
                var postPage = FindElementWithChecking(By.Id(aspnetForm));
                pp = postPage.GetAttribute("action");
                vp += pp.Substring(0, pp.IndexOf('.'));
            }
            catch { }
            return vp;
        }

        public IWebElement FindElementWithChecking(By by)
        {
            if (Canceled)
            {
                _logger.Info(
                    "Interrupted by Canceled flag. throw new WebDriverException");
                throw new WebDriverException();
            }
            //var wait = new WebDriverWait(_driver,
            //    TimeSpan.FromSeconds(20));
            //return wait.Until(d => d.FindElement(by));.
            return _driver.FindElement(by);
        }

        private void CheckForError()
        {
            _logger.Trace($"Start CheckForError. Error = {Error}");
            IWebElement erQuery = null;
            try
            {
                erQuery = _driver.FindElement(By.Id(errorMessage));
            }
            catch (Exception ex)
                when (ex is NoSuchElementException || ex is WebDriverException)
            {
                _logger.Info($"Error element not found. Error ={Error}");
            }
            DateTime dateValue;
            if (erQuery != null
                && erQuery.Text.IsNotBlank()
                && !(DateTime.TryParse(erQuery.Text, out dateValue))
                && erQuery.Text != capchaNotFilledMessage)// it is available date shown - not error
            {
                ValidationError = true;
                OutData = erQuery.Text;
                _logger.Error(
                    $"throw new NoSuchElementException. Reason erQuery.Text.IsNotBlank = {erQuery.Text}");
                //Error = true;
                throw new NoSuchElementException();
            }
            _logger.Trace($"End CheckForError. Error = {Error}");
        }

        public void CloseBrowser()
        {
            _logger.Trace("CloseBrowser");
            _driver?.Quit();
        }

        #endregion Help Functions


        public void GetFirstDate(VisaDataSet.ClientDataRow dataRow) // state 13
        {
            _logger.Trace($"Start GetFirstDate. Error = {Error}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            /* 
             * #ctl00_plhMain_cldAppointment 
             *  > tbody:nth-child(1) 
             *      > tr:nth-child(1) 
             *          > td:nth-child(1) 
             *              > table:nth-child(1) 
             *                  > tbody:nth-child(1) 
             *                      > tr:nth-child(1) 
             *                          > td:nth-child(2)"
             */
            var tableOuter = FindElementWithChecking(By.Id("ctl00_plhMain_cldAppointment"));
            var tableInner = tableOuter.FindElement(By.TagName("table"));
            var tdCollection = tableInner.FindElements(By.TagName("td"));
            var dateString = tdCollection[1].Text;
            const string format = "MMMM yyyy р.";
            GetFirstDateScroll = false;
            try
            {
                _logger.Info(
                    $"GetFirstDate Try Parse Month. element.Text = {dateString}.");
                var result = DateTime.ParseExact(dateString,
                    format,
                    CultureInfo.CreateSpecificCulture("uk-UA"));
                _logger.Info(
                    $"GetFirstDate element.Text = {dateString} is parsed as {result.ToString("d MMM yyyy")}");

                if (dataRow.RegistryTo.Month < result.Month
                    || dataRow.RegistryFom.Month > result.Month)
                {
                    var colIndex = 0;
                    if (dataRow.RegistryTo.Month < result.Month)
                        colIndex = 2;
                    tdCollection[colIndex].Click();
                    _logger.Info(
                        $"GetFirstDate. Scroll Calendar {tdCollection[colIndex].Text}");
                    GetFirstDateScroll = true;
                }
                else
                {
                    var queryCollection =
                        _driver.FindElements(By.ClassName(availableData));
                    if (queryCollection.Count == 0)
                        _logger.Warn(
                            $"no dates Available this month:{dateString}");
                    _logger.Info(
                        $"GetFirstDate. minDate = {dataRow.RegistryFom.Day}. maxDate = {dataRow.RegistryTo.Day}");
                    OutData =
                        string.Format(
                            ResManager.GetString(ResKeys.DateIncorrect_Message),
                            dataRow.RegistryFom.Day + " & "
                            + dataRow.RegistryTo.Day);
                    foreach (var element in queryCollection)
                    {
                        var date = element.Text.ConvertToIntOrNull();

                        if (date == null
                            || date.Value > dataRow.RegistryTo.Day
                            || date.Value < dataRow.RegistryFom.Day)
                            continue;

                        _logger.Info(
                            $"GetFirstDate. date.Value = {date.Value} element Click");
                        OutData = date.Value.ToString();
                        element.Click();
                        break;
                    }
                }
            }
            catch (FormatException)
            {
                _logger.Error(
                    $"GetFirstDate \"{dateString}\" is not in the correct Date format.");
                Error = true;
            }
            catch (Exception ex)
            {
                //todo Bohdan127 we really need it?
                _logger.Error($"GetFirstDate Exception{ex.Message}");
                Error = true;
            }
            //CheckForError();//todo we need to check if that sate is returned
            _logger.Trace($"End GetFirstDate. Error = {Error}. dateFrom: {dataRow.RegistryFom.ToShortDateString()}, dateTo: {dataRow.RegistryTo.ToShortDateString()}, OutData: {OutData}");
        }

        public void SelectRegistrationTime() // state 16
        {
            _logger.Trace($"Start SelectRegistrationTime. Error = {Error}.");

            OutData = string.Empty;
            FindElementWithChecking(By.Id(registryTime)).Click();

            _logger.Trace($"End SelectRegistrationTime. Error = {Error}");
        }
    }
}
