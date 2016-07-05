using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Globalization;
using System.Threading;
using ToolsPortable;
using Visa.Database;

namespace Visa.WebCrawler.SeleniumCrawler
{
    public class RegisterUser
    {
        private const string visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія

        private const string buttonSubmit = "ctl00_plhMain_btnSubmit";
        //Підтвердити                                      private static string regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації

        private const string visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр i Пункт Прийому Візових Анкетx

        private const string mainUrl =
            "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";

        private const string registryId = "ctl00_plhMain_lnkSchApp"; // Призначити дату подачі документів
        private const string reason = "ctl00_plhMain_cboPurpose"; //Мета візиту
        private const string numOfApplicants = "ctl00_plhMain_tbxNumOfApplicants"; //Кількість заявників
        private const string numOfChildrens = "ctl00_plhMain_txtChildren"; //К-сть дітей вписаних у паспорт батьків

        private const string receiptNumber = "ctl00_plhMain_repAppReceiptDetails_ctl01_txtReceiptNumber";
        //Номер квитанції

        private const string buttonSubmitEmail = "ctl00_plhMain_btnSubmitDetails"; //Підтвердити імейл
        private const string email = "ctl00_plhMain_txtEmailID";
        private const string passForMail = "ctl00_plhMain_txtPassword";
        private const string infoText = "ctl00_plhMain_lblMsg"; //Ок текст - на коли можна зареєструватись

        private const string endPassportDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPPTEXPDT";
        //Дата закінчення терміну дії паспорту

        private const string statusField = "ctl00_plhMain_repAppVisaDetails_ctl01_cboTitle"; //Статус
        private const string personName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxFName"; //Ім'я
        private const string personLastName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxLName"; //Прізвище
        private const string personBirthday = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxDOB"; //Дата народження
        private const string returnDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxReturn"; //Дата повернення
        private const string nationality = "ctl00_plhMain_repAppVisaDetails_ctl01_cboNationality"; //Національність
        private const string availableData = "OpenDateAllocated"; //Будь ласка, оберіть Вільну дату
        private const string registryTime = "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot"; //Будь ласка, оберіть Час

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver _driver;


        public bool Error { get; private set; }
        public string OutData { get; private set; }

        public RegisterUser()
        {
            _logger.Trace("Start RegisterUser constructor");
            _driver = new FirefoxDriver();
            _logger.Trace("End RegisterUser constructor");
        }

        public void PartOne(VisaDataSet.ClientDataRow dataRow, int serCenId)
        {
            _logger.Trace(
                $"Start PartOne. Error = {Error}. Service Center Id = {serCenId}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            try
            {
                _driver.Navigate().GoToUrl(mainUrl);
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(registryId));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query =
                        _driver.FindElement(By.Id(visaCity)).FindElement(By.CssSelector($"option[value={serCenId}]"));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(reason)).FindElement(By.CssSelector("option[value='1']"));
                    //always be 1 - Подача документів
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(buttonSubmit));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(numOfApplicants));
                    query.Clear();
                    query.SendKeys(dataRow.PeopleCount);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(numOfChildrens));
                    query.Clear();
                    query.SendKeys(dataRow.ChildsCount);
                }
                Thread.Sleep(2000);
                {
                    var query =
                        _driver.FindElement(By.Id(visaCategory)).FindElement(By.CssSelector("option[value='235']"));
                    query.Click();
                }
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
            _logger.Trace($"End PartOne. Error = {Error}");
        }

        public void PartTwo(VisaDataSet.ClientDataRow dataRow)
        {
            _logger.Trace($"Start PartOne. Error = {Error}. dataRow.NumberOfReceipt = {dataRow.NumberOfReceipt}");
            try
            {
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(buttonSubmit));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(receiptNumber));
                    query.SendKeys(dataRow.NumberOfReceipt);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(buttonSubmit));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    try
                    {
                        var query = _driver.FindElement(By.Id(infoText));
                        OutData = query.Text;
                        Error = true;
                        return;
                    }
                    catch (NoSuchElementException ex)
                    {
                        //todo logger
                    }
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(email));
                    query.SendKeys(dataRow.Email);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(passForMail));
                    query.SendKeys(dataRow.Password);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(buttonSubmitEmail));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(endPassportDate));
                    query.SendKeys(dataRow.EndPassportDate.ToString(CultureInfo.CurrentCulture));
                }
                Thread.Sleep(2000);
                {
                    var query =
                        _driver.FindElement(By.Id(statusField))
                            .FindElement(By.CssSelector($"option[value={dataRow.Status}]"));
                    query.Click();
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(personName));
                    query.SendKeys(dataRow.Name);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(personLastName));
                    query.SendKeys(dataRow.LastName);
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(personBirthday));
                    query.SendKeys(dataRow.Birthday.ToString(CultureInfo.CurrentCulture));
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(returnDate));
                    query.SendKeys(dataRow.ReturnData.ToString(CultureInfo.CurrentCulture));
                }
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(nationality));
                    query.SendKeys(dataRow.Nationality);
                }
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
        }

        public void PartThree(VisaDataSet.ClientDataRow dataRow)
        {
            try
            {
                Thread.Sleep(2000);
                {
                    var queryCollection = _driver.FindElements(By.ClassName(availableData));
                    foreach (var element in queryCollection)
                    {
                        var date = element.Text.ConvertToIntOrNull();
                        var minDate = dataRow.RegistryFom.Day < dataRow.RegistryTo.Day
                            ? dataRow.RegistryFom.Day
                            : dataRow.RegistryTo.Day;
                        var maxDate = dataRow.RegistryFom.Day == minDate
                            ? dataRow.RegistryTo.Day
                            : dataRow.RegistryFom.Day;
                        if (date != null &&
                            (date.Value >= maxDate || date.Value <= minDate))
                        {
                            element.Click();
                            break;
                        }
                    }
                }
                Thread.Sleep(2000);
                {
                    try
                    {
                        var query = _driver.FindElement(By.Id(infoText));
                        OutData = query.Text;
                        Error = true;
                        return;
                    }
                    catch (NoSuchElementException ex)
                    {
                        //todo logger
                    }
                }
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
        }

        public void PartFour()
        {
            try
            {
                Thread.Sleep(2000);
                {
                    var query = _driver.FindElement(By.Id(registryTime));
                    query.Click();
                } //todo maybe here we should get info from site
            }
            catch (NoSuchElementException ex)
            {
                _logger.Error($"NoSuchElementException with message = {ex.Message}");
                Error = true;
            }
        }
    }
}
