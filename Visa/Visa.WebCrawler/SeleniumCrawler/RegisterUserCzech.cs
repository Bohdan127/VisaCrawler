//#define GoWithoutDates
//#define UseDefaultSelenium
//#define UseSeleniumWithGeckodriver
//#define UsePhantomJSDriver
//#define ClientPerformanceRequest

#if UseDefaultSelenium
using Selenium;
#endif
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using GlobalResources;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.WebCrawler.Interfaces;

#if UsePhantomJSDriver
using OpenQA.Selenium.PhantomJS;
#else
using OpenQA.Selenium.Firefox;
#endif


namespace Visa.WebCrawler.SeleniumCrawler
{
    public class RegisterUserCzech : ICrawler
    {
        public RegisterUserCzech()
        {
            _logger.Trace("Start RegisterUserCzech constructor");
#if UsePhantomJSDriver
            _driver = new PhantomJSDriver();
#else
            var prof = new FirefoxProfile();
            prof.SetPreference("browser.startup.homepage_override.mstone",
                "ignore");
            prof.SetPreference("startup.homepage_welcome_url.additional",
                "about:blank");
            prof.EnableNativeEvents = false;
            _driver = new FirefoxDriver(prof);
#endif
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(15));
            _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(15));
            _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromMinutes(15));
            Error = false;
            Canceled = false;
            RegistrarionDateAvailability = DialogResult.None;
#if UseDefaultSelenium
            int port = 4444; //2310;
            ISelenium selenium = new DefaultSelenium("localhost", port, "*firefox C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe", mainUrl);
#endif
            _logger.Trace("End RegisterUserCzech constructor");
        }

        public void GoToUrl()
        {
            _logger.Info($"Start GoToUrl. Error = {Error}.");

            _driver.Navigate().GoToUrl(MainUrl);

            _logger.Info($"End GoToUrl. Error = {Error}.");
        }

        public void StartRegistration()
        {
            _logger.Info($"Start StartRegistration. Error = {Error}. ");
            var query = FindElementWithChecking(By.Id(RegistryId));
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
            FindElementWithChecking(By.Id(VisaCity))
                .FindElement(
                    By.CssSelector($"option[value=\"{dataRow.VisaCity}\"]"))
                .Click();
            _logger.Info(
                $"SelectCityAndReason. visaCity option[value={dataRow.VisaCity}]  Click");
            //always be 1 - Подача документів
            //FindElementWithChecking(By.Id(Reason))
            //    .FindElement(By.CssSelector("option[value='1']"))
            //    .Click();
            //_logger.Info(
            //    "SelectCityAndReason. reason option[value='1'] Click");
            _logger.Info($"End SelectCityAndReason. Error = {Error}. ");
        }

        /// <summary>
        ///     Require Captcha after it
        /// </summary>
        public void ProvidePeopleCount(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start ProvidePeopleCount. Error = {Error}. ");
            var query = FindElementWithChecking(By.Id(NumOfApplicants));
            _logger.Info(
                $"ProvidePeopleCount. numOfApplicants Clear and set {dataRow.PeopleCount}");

            if (dataRow.PeopleCount != "1")
            {
                query.Clear();
                query.SendKeys(dataRow.PeopleCount);
            }

            //query = FindElementWithChecking(By.Id(NumOfChildrens));
            //_logger.Info(
            //    $"ProvidePeopleCount. numOfChildrens Clear and set {dataRow.ChildsCount}");

            //if (dataRow.ChildsCount != "0")
            //{
            //    query.Clear();
            //    query.SendKeys(dataRow.ChildsCount);
            //}
            _logger.Info($"End ProvidePeopleCount. Error = {Error}");
        }

        /// <summary>
        ///     Select visa Type and check for date
        /// </summary>
        /// <returns>False - if data was to hight or not available, True - data is good for using and Capthca is needed</returns>
        public void SelectVisaType(
            VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Trace($"Start SelectVisaType. Error = {Error}. ReEnterCaptcha = {ReEnterCaptcha}");
            //ReEnterCaptcha = false;
            var visaCat = FindElementWithChecking(By.Id(VisaCategory))
                 .FindElement(
                     By.CssSelector($"option[value=\"{dataRow.VisaType}\"]"));

            if (visaCat.Selected)
            {
                //ReEnterCaptcha = true;
                //-Оберіть візову категорію-
                FindElementWithChecking(By.Id(VisaCategory))
                    .FindElement(
                        By.CssSelector($"option[value=\"-1\"]"))
                    .Click();
                _logger.Info("SelectVisaType. visaCategory " +
                             $"option[value=-1]  Click");
            }
            else
            {
                visaCat.Click();
                _logger.Info("SelectVisaType. visaCategory " +
                             $"option[value={dataRow.VisaType}]  Click");
            }

            _logger.Trace($"End SelectVisaType. Error = {Error}. ReEnterCaptcha = {ReEnterCaptcha}");
        }

        public bool CheckDate(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Info($"Start CheckDate. Error = {Error}. ");
            //ReEnterCaptcha = false;
            var bRes = false;
            try
            {
                var infoText = FindElementWithChecking(By.Id(ErrorMessage)).Text;
                //if (infoText.Contains(CapchaNotFilledMessage)) //"The image you selected not match"
                //{
                //    FillCapchaFailed = true;
                //    return false;
                //}

                try
                {
                    infoText = infoText.Substring(infoText.Length - 11);
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
                        OutData =
                            string.Format(
                                ResManager.GetString(
                                    ResKeys.DateСorrect_Message),
                                availableDate.ToShortDateString());
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
            var button = FindElementWithChecking(By.Id(BtnCancel));
            try
            {
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
            var txtBox = FindElementWithChecking(By.Id(ReceiptNumber));
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
            var txtBox = FindElementWithChecking(By.Id(Email));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Email);
            _logger.Info($"ClientData. email set text {dataRow.Email}");

            txtBox = FindElementWithChecking(By.Id(PassForMail));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Password);
            _logger.Info(
                $"ClientData. passForMail set text {dataRow.Password}");
            _logger.Trace($"End EmailAndPassword. Error = {Error}");
        }

        public void ClientData(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Trace($"Start ClientData. Error = {Error}");
            var txtBox = FindElementWithChecking(By.Id(PassportNumber));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.PassportNumber);
            _logger.Info(
                $"ClientData. PassportNumber set text {dataRow.PassportNumber}");

            txtBox = FindElementWithChecking(By.Id(EndPassportDate));
            txtBox.Clear();
            txtBox.SendKeys(
                    dataRow.EndPassportDate.ToShortDateString().Replace(".",
                        "/"));
            _logger.Info(
                $"ClientData. endPassportDate set text {dataRow.EndPassportDate.ToShortDateString().Replace(".", "/")}");

            FindElementWithChecking(By.Id(StatusField))
                .FindElement(
                    By.CssSelector($"option[value=\"{dataRow.Status}\"]"))
                .Click();
            _logger.Info(
                $"ClientData. statusField option[value={dataRow.Status}] Click");

            txtBox = FindElementWithChecking(By.Id(PersonName));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Name);
            _logger.Info($"ClientData. personName set text {dataRow.Name}");

            txtBox = FindElementWithChecking(By.Id(PersonLastName));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.LastName);
            _logger.Info(
                $"ClientData. personLastName set text {dataRow.LastName}");

            txtBox = FindElementWithChecking(By.Id(PersonBirthday));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.Birthday.ToShortDateString().Replace(".",
                   "/"));
            _logger.Info( //.ToString("dd/MM/yy")
                $"ClientData. personBirthday set text {dataRow.Birthday.ToShortDateString().Replace(".", "/")}");

            txtBox = FindElementWithChecking(By.Id(ReturnDate));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.ReturnData.ToShortDateString()
                .Replace(".",
                    "/"));
            _logger.Info(
                $"ClientData. returnDate set text {dataRow.ReturnData.ToShortDateString().Replace(".", "/")}");

            txtBox = FindElementWithChecking(By.Id(CellNumber));
            txtBox.Clear();
            txtBox.SendKeys(dataRow.CellNumber);
            _logger.Info(
                $"ClientData. CellNumber set text {dataRow.CellNumber}");

            FindElementWithChecking(By.Id(Nationality))
                .FindElement(
                    By.CssSelector(
                        $"option[value=\"{dataRow.Nationality}\"]"))
                .Click();
            _logger.Info(
                $"ClientData. nationality option[value=\"{dataRow.Nationality}\"] Click");
            _logger.Trace($"End ClientData. Error = {Error}");
        }

        #region Members

        private const string VisaCategory = "ctl00_plhMain_cboVisaCategory";
        //Візова категорія

        private const string ButtonSubmit = "ctl00_plhMain_btnSubmit";
        //Підтвердити

        private const string VisaCity = "ctl00_plhMain_cboVAC";
        //Візовий Сервіс Центр i Пункт Прийому Візових Анкетx

        private const string MainUrl =
            "https://www.vfsvisaonline.com/czech-ukraine-app/AppScheduling/AppWelcome.aspx?p=ri7FHohe3VirNKmyLaRu34LwQqySyHA1BJbq9qOXYNc=";

        private const string RegistryId = "ctl00_plhMain_lnkSchApp";
        // Призначити дату подачі документів

        private const string Reason = "ctl00_plhMain_cboPurpose"; //Мета візиту

        private const string NumOfApplicants =
            "ctl00_plhMain_tbxNumOfApplicants"; //Кількість заявників

        private const string NumOfChildrens = "ctl00_plhMain_txtChildren";
        //К-сть дітей вписаних у паспорт батьків

        private const string ReceiptNumber =
            "ctl00_plhMain_repAppReceiptDetails_ctl01_txtReceiptNumber";

        //Номер квитанції

        private const string ButtonSubmitEmail =
            "ctl00_plhMain_btnSubmitDetails"; //Підтвердити імейл

        private const string Email = "ctl00_plhMain_txtEmailID";
        private const string PassForMail = "ctl00_plhMain_txtPassword";

        private const string ErrorMessage = "ctl00_plhMain_lblMsg";
        //Ок текст - на коли можна зареєструватись

        private const string PassportNumber =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPassportNo";
        //номер закордонного паспорта (лише цифри RegularExpression "^([\\w]|[ ])*$")

        private const string EndPassportDate =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPPTEXPDT";
        //Дата закінчення терміну дії паспорту

        private const string StatusField =
            "ctl00_plhMain_repAppVisaDetails_ctl01_cboTitle"; //Статус

        private const string PersonName =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxFName";
        //Ім'я (тільки букви RegularExpression "^([a-zA-Z])+$")

        private const string PersonLastName =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxLName";
        //Прізвище (тільки букви RegularExpression "^([a-zA-Z])+$")

        private const string PersonBirthday =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxDOB"; //Дата народження

        private const string ReturnDate =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxReturn"; //Дата повернення

        private const string CellNumber =
            "ctl00_plhMain_repAppVisaDetails_ctl01_tbxContactNumber";
        //Контактний номер телефону (RegularExpression "^([0-9]|[-])+$")

        private const string Nationality =
            "ctl00_plhMain_repAppVisaDetails_ctl01_cboNationality";
        //Національність

        private const string AvailableData = "OpenDateAllocated";
        //Будь ласка, оберіть Вільну дату

        private const string RegistryTime =
            "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot"; //Будь ласка, оберіть Час

        private const string BtnCancel = "ctl00_plhMain_btnCancel";

        private const string AspnetForm = "aspnetForm";

        private const string CapchaNotFilledMessage = "The image you selected not match";

        // ReSharper disable once InconsistentNaming
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

#if UsePhantomJSDriver
        private readonly PhantomJSDriver _driver;
#else
        private readonly FirefoxDriver _driver;
#endif
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

        public bool ReEnterCaptcha { get; set; }

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
                    if (_driver.Title.IsNotNullOrEmpty() && !_driver.Title.Contains("Czech Visa"))
                    {
                        _logger.Error("IsServerDown => True. Title not Contains(\"Czech Visa\")");
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
                    return IsServerDown;
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
                ? FindElementWithChecking(By.Id(ButtonSubmitEmail))
                : FindElementWithChecking(By.Id(ButtonSubmit));
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

#if ClientPerformanceRequest
        public void RunNextStepV2(
            Action regAction)
        {
            _logger.Trace($"Start RunNextStepV2. Error = {Error}.");

            try
            {
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
            _logger.Trace($"End RunNextStepV2. Error = {Error}.");
        }
#endif

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
            var shouldAlertAccept = (0 != String.Compare(_driver.Url, MainUrl, StringComparison.Ordinal));
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
            _logger.Trace($"Start GetCurrentPage. Error = {Error}");
            string vp = "", pp = "";
            try
            {
                vp = _driver.Url.Substring(106, 30);
                vp = vp.Substring(0, vp.IndexOf('.'));
            }
            catch { }
            try
            {
                var postPage = FindElementWithChecking(By.Id(AspnetForm));
                pp = postPage.GetAttribute("action");
                pp = pp.Substring(0, pp.LastIndexOf('.'));
                vp += pp.Substring(pp.LastIndexOf('/'));
            }
            catch { }
            _logger.Trace($"End GetCurrentPage. Error = {Error}");
            return vp;
        }

        public IWebElement FindElementWithChecking(By by)
        {
            if (!Canceled)
                return _driver.FindElement(@by);
            _logger.Info(
                "Interrupted by Canceled flag. throw new WebDriverException");
            throw new WebDriverException();
        }

        public virtual void CheckForError()
        {
            _logger.Trace($"Start CheckForError. Error = {Error}");
            IWebElement erQuery = null;
            try
            {
                if (_driver.PageSource.Contains(ErrorMessage))
                    erQuery = _driver.FindElement(By.Id(ErrorMessage));
            }
            catch (Exception ex)
                when (ex is NoSuchElementException || ex is WebDriverException)
            {
                _logger.Info($"Error element not found. Error ={Error}");
            }
            if (IsErrorExist(erQuery))
            {
                ValidationError = true;
                OutData = erQuery?.Text;
                _logger.Error(
                    $"throw new NoSuchElementException. Reason erQuery.Text.IsNotBlank = {erQuery?.Text}");
                //Error = true;
                throw new NoSuchElementException();
            }
            _logger.Trace($"End CheckForError. Error = {Error}");
        }

        protected virtual bool IsErrorExist(IWebElement element)
        {
            DateTime dateValue;

            var bRes = element != null;
            bRes = bRes && element.Text.IsNotBlank();
            bRes = bRes && !DateTime.TryParse(element.Text.Substring(element.Text.Length - 11),
                out dateValue);	// last 11 char may be date
            bRes = bRes && element.Text != CapchaNotFilledMessage; // it is available date shown - not error
            return bRes;
        }

        public void CloseBrowser()
        {
            _logger.Trace("CloseBrowser");
            _driver?.Quit();
#if UseSeleniumWithGeckodriver
            _driver?.Dispose();//this is needed for close geckodriver.exe during closing application
#endif
        }

        /// <summary>
        /// Click on link for changing month on 0 column if left or in 2 if right
        /// </summary>
        /// <param name="scrollLeft">perform scroll left</param>
        protected virtual void ScrollMonth(bool scrollLeft)
        {
            _logger.Trace($"Start ScrollMonth. Error = {Error} scrollLeft = {scrollLeft}");

            var element = GetCalendarHeader()[scrollLeft ? 0 : 2];
            if (element.Text.IsBlank())
            {
                OutData = ResManager.GetString(ResKeys.NoDate_Message);
                ValidationError = true;
                throw new NoSuchElementException();
            }
            element.Click();
            _logger.Info("ScrollMonth => Click Performed!");
            _logger.Trace($"End ScrollMonth. Error = {Error} scrollLeft = {scrollLeft}");
        }

        protected virtual ReadOnlyCollection<IWebElement> GetCalendarHeader()
        {
            _logger.Trace($"Try to GetCalendarHeader. Error = {Error}");
            return
                FindElementWithChecking(By.Id("ctl00_plhMain_cldAppointment"))
                    .FindElement(By.TagName("table"))
                    .FindElements(By.TagName("td"));
        }

        /// <summary>
        /// Checks, if month for searching registration date is between registry from and to dates
        /// </summary>
        /// <param name="dateToCheck">date for checking Year and Month</param>
        /// <param name="registryFom">lower checking line</param>
        /// <param name="registryTo">higher checking line</param>
        /// <returns></returns>
        protected virtual MonthChange CheckMounth(DateTime dateToCheck,
            DateTime registryFom,
            DateTime registryTo)
        {
            _logger.Trace($"Start CheckMounth. Error = {Error} dateToCheck = {dateToCheck} registryFom = {registryFom} registryTo = {registryTo}");
            if (dateToCheck.Year < registryFom.Year)
            {
                _logger.Info("End CheckMounth. return MonthChange.Rigth;");
                return MonthChange.Rigth;
            }

            if (dateToCheck.Year > registryTo.Year)
            {
                _logger.Info("End CheckMounth. return MonthChange.Left;");
                return MonthChange.Left;
            }

            if (registryTo.Year == registryFom.Year)
            {
                if (dateToCheck.Month < registryFom.Month)
                {
                    _logger.Info("End CheckMounth. return MonthChange.Rigth;");
                    return MonthChange.Rigth;
                }

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (dateToCheck.Month > registryTo.Month)
                {
                    _logger.Info("End CheckMounth. return MonthChange.Left;");
                    return MonthChange.Left;
                }

                _logger.Info("End CheckMounth. return MonthChange.None;");
                return MonthChange.None;
            }

            if (dateToCheck.Year == registryFom.Year)
            {
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (dateToCheck.Month < registryFom.Month)
                {
                    _logger.Info("End CheckMounth. return MonthChange.Rigth;");
                    return MonthChange.Rigth;
                }

                _logger.Info("End CheckMounth. return MonthChange.None;");
                return MonthChange.None;
            }

            // ReSharper disable once InvertIf
            if (dateToCheck.Year == registryTo.Year)
            {
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (dateToCheck.Month > registryTo.Month)
                {
                    _logger.Info("End CheckMounth. return MonthChange.Left;");
                    return MonthChange.Left;
                }

                _logger.Info("End CheckMounth. return MonthChange.None;");
                return MonthChange.None;
            }
            _logger.Warn("End CheckMounth. return MonthChange.Failed;");
            return MonthChange.Failed;
        }

        /// <summary>
        /// Select first available for registration date from UI calendar
        /// </summary>
        /// <param name="currentMonth">Current calendar month</param>
        /// <param name="registryFom">lower checking line</param>
        /// <param name="registryTo">higher checking line</param>
        /// <returns></returns>
        protected virtual bool SelectRegistrationDate(DateTime currentMonth,
            DateTime registryFom,
            DateTime registryTo)
        {
            _logger.Trace($"Start SelectRegistrationDate. Error = {Error}. currentMonth = {currentMonth}. registryFom = {registryFom}. registryTo = {registryTo}");
            ReadOnlyCollection<IWebElement> queryCollection;
            try
            {
                var infoText = FindElementWithChecking(By.Id(ErrorMessage)).Text;
                if (infoText.IsNotBlank())
                {
                    _logger.Warn("label with error message is not blank. Thrown new NoSuchElementException");
                    throw new NoSuchElementException();
                }
                queryCollection =
                    _driver.FindElements(By.ClassName(AvailableData));
            }
            catch (Exception ex)
                when (ex is NoSuchElementException || ex is WebDriverException)
            {
                _logger.Warn("Here is no available dates!!!");
                return false;
            }

            var availableRange = GetAvailableRange(currentMonth, registryFom, registryTo);


            foreach (var element in queryCollection)
            {
                var date = element.Text.ConvertToIntOrNull();

                if (date == null
                    || date.Value > availableRange.Item2 //endDate
                    || date.Value < availableRange.Item1) //startDate
                    continue;

                if (ReEnterCaptcha)
                {
                    _logger.Info($"GetFirstDate. First available date.Value = {date.Value}. But ReEnterCaptcha = True");
                    return true;
                }

                _logger.Info(
                    $"GetFirstDate. date.Value = {date.Value} element Click");
                OutData = string.Format(ResManager.GetString(ResKeys.DateSelected_Message), $"{date.Value} {GetCalendarHeader()[1].Text}");
                element.Click();
                return true;
            }
            return false;
        }

        protected virtual Tuple<int, int> GetAvailableRange(DateTime currentMonth, DateTime registryFom, DateTime registryTo)
        {
            _logger.Trace($"Start GetAvailableRange. Error = {Error} currentMonth = {currentMonth} registryFom = {registryFom} registryTo = {registryTo}");
            int startDate,
                endDate;

            if (currentMonth.Year > registryTo.Year
                || currentMonth.Year < registryFom.Year)
            {
                throw new NotImplementedException();
            }
            //todo make refactoring for code below
            if (currentMonth.Year == registryFom.Year)
            {
                if (currentMonth.Month == registryFom.Month)
                {
                    startDate = registryFom.Day;
                }
                else if (currentMonth.Month < registryFom.Month)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    startDate = 1;
                }
            }
            else
            {
                startDate = 1;
            }

            if (currentMonth.Year == registryTo.Year)
            {
                if (currentMonth.Month == registryTo.Month)
                {
                    endDate = registryTo.Day;
                }
                else if (currentMonth.Month > registryTo.Month)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    endDate = 31;
                }
            }
            else
            {
                endDate = 31;
            }
            _logger.Info($"GetAvailableRange returned startDate = {startDate}. endDate = {endDate}.");
            return new Tuple<int, int>(startDate, endDate);
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
            const string format = "MMMM yyyy р.";
            ReEnterCaptcha = false;

            MonthChange monthChange;
            DateTime monthToCheck;
            do
            {
                var dateString = GetCalendarHeader()[1].Text;
                try
                {
                    _logger.Info(
                        $"GetFirstDate Try Parse Month. element.Text = {dateString}.");
                    monthToCheck = DateTime.ParseExact(dateString,
                               format,
                               //here we use uk-UA culture instead on CurrentCulture, because in site it will always be uk-UA
                               CultureInfo.CreateSpecificCulture("uk-UA"));
                    _logger.Info(
                        $"GetFirstDate element.Text = {dateString} is parsed as {monthToCheck.ToString("d MMM yyyy")}");
                    monthChange = CheckMounth(monthToCheck, dataRow.RegistryFom, dataRow.RegistryTo);
                }
                catch (FormatException)
                {
                    _logger.Error($"GetFirstDate month value \"{dateString}\" is not in the correct Date format.");
                    Error = true;
                    return;
                }

                switch (monthChange)
                {
                    case MonthChange.Left:
                        _logger.Info("Scroll Left");
                        ScrollMonth(true);
                        ReEnterCaptcha = true;
                        break;
                    case MonthChange.None:
                        _logger.Info("Correct month");
                        //month is in range now, all OK
                        break;
                    case MonthChange.Rigth:
                        _logger.Info("Scroll Right");
                        ReEnterCaptcha = true;
                        ScrollMonth(false);
                        break;
                    case MonthChange.Failed:
                        //if we get this status than we do something wrong
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (monthChange != MonthChange.None);

            //todo this first condition probably can not work correctly
            while (monthToCheck <= dataRow.RegistryTo
                   && !SelectRegistrationDate(monthToCheck,
                       dataRow.RegistryFom,
                       dataRow.RegistryTo))
            {
                ScrollMonth(false);
            }

            //CheckForError();//todo we need to check if that sate is returned
            _logger.Trace($"End GetFirstDate. Error = {Error}. dateFrom: {dataRow.RegistryFom.ToShortDateString()}, dateTo: {dataRow.RegistryTo.ToShortDateString()}, OutData: {OutData}");
        }

        public void SelectRegistrationTime(VisaDataSet.ClientDataRow dataRow) // state 16
        {
            _logger.Trace($"Start SelectRegistrationTime. Error = {Error}.");

            OutData = string.Empty;
            FindElementWithChecking(By.Id(RegistryTime)).Click();
            var scr = _driver.GetScreenshot();
            var fileName = $"{dataRow.Name}_{dataRow.LastName}.jpg";
            scr.SaveAsFile(fileName, ImageFormat.Jpeg);
            EmailManager.SendEmailWithPhoto(fileName);
            EmailManager.SendEmailWithMoneyRequest();
            _logger.Trace($"End SelectRegistrationTime. Error = {Error}");
        }

        public string SendRecaptchav2Request(string goggleKey)
        {
            _logger.Trace($"Start SendRecaptchav2Request");
            //POST
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                var request = (HttpWebRequest)WebRequest.Create("http://rucaptcha.com/in.php"); // "http://2captcha.com/in.php");
                var ruCaptchaID = SetupManager.GetOptions().RuCaptchaID;
                var pageUrl = _driver.Url;//Uri.EscapeUriString(_driver.Url) Uri.EscapeDataString(_driver.Url)
                var postData = $"key={ruCaptchaID}&method=userrecaptcha&googlekey={goggleKey}&pageurl={pageUrl}";
                var data = System.Text.Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var readStream = new System.IO.StreamReader(response.GetResponseStream());
                var responseString = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                //  GET
                if (responseString.Contains("OK|"))
                {
                    _logger.Trace($"responseString: {responseString}");
                    var param = "?key=" + ruCaptchaID + "&action=get&id=" + responseString.Substring(3);
                    string captchaResponse;
                    int count = 0;
                    (_driver as IJavaScriptExecutor).ExecuteScript(
                        "document.getElementById('g-recaptcha-response').setAttribute('style', " +
                        "document.getElementById('g-recaptcha-response').getAttribute('style').replace('display: none;',''))");
                    var gRecaptchaResponse = FindElementWithChecking(By.Id("g-recaptcha-response"));
                    do
                    {
                        Thread.Sleep(5000);
                        var request2 = (HttpWebRequest)WebRequest.Create("http://rucaptcha.com/res.php" + param);
                        {
                            using (var response2 = (HttpWebResponse)request2.GetResponse())
                            {
                                System.IO.Stream receiveStream = response2.GetResponseStream();

                                // Pipes the stream to a higher level stream reader with the required encoding format. 
                                using (var readStream2 = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
                                {
                                    captchaResponse = readStream2.ReadToEnd();
                                }
                            }
                        }
                        if (captchaResponse.IsNotBlank()) _logger.Trace($"GetCaptchaResponse count == {count}, captchaResponse: {captchaResponse}");
                        if (count > 15)
                        {
                            _logger.Trace($"End SendRecaptchav2Request with Error: count == {count} of try get response: {captchaResponse}");
                            return "Error: Try count==" + count;
                        }
                        count++;
                    } while (!captchaResponse.Contains("OK"));
                    gRecaptchaResponse.SendKeys(captchaResponse.Substring(3));
                    _logger.Trace($"End SendRecaptchav2Request");
                    return captchaResponse;
                }
                else
                {
                    _logger.Trace($"End SendRecaptchav2Request with Error: {responseString}");
                    return "Error: " + responseString;
                }
            }
            catch (Exception ex)
            {
                string tt = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                _logger.Trace($"End SendRecaptchav2Request with Exception {tt}");
                return tt;
            }
        }

        public string GetRecaptchaResult(string captchaId)
        {
            string documentText = "";
            do
            {
                Thread.Sleep(5000);
                //HtmlDocument document = Jsoup.connect("http://rucaptcha.com/res.php?key=КЛЮЧ СЕРВИСА РУКАПЧИ&action=get&id=" + captchaId).get();
                //documentText = document.text();
            } while (!documentText.Contains("OK"));
            string captchaResponse = documentText.Substring(3);
            return captchaResponse;
        }

        public bool SpecialTmpCheckForCapchaError()
        {
            //here all is bad, just tmp fix
            _logger.Warn("SpecialTmpCheckForCapchaError");
            try
            {
                if (IsServerDown)
                {
                    Error = true;
                    _logger.Warn($"End CheckDate. Error = {Error}.");
                    return false;
                }
                return FindElementWithChecking(By.Id(ErrorMessage)).Text.Contains(CapchaNotFilledMessage);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return false;
            }
        }
    }
}
