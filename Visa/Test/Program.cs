using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Threading;
using ToolsPortable;

namespace Test
{
    class Program
    {
        //main link - https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=
        private static string mainUrl = "https://polandonline.vfsglobal.com/poland-ukraine-appointment/%28S%28vvzibb45kxnimzfrnhuavib1%29%29/AppScheduling/AppWelcome.aspx?P=s2x6znRcBRv7WQQK7h4MTjZiPRbOsXKqJzddYBh3qCA=";
        private static string checkAvailableData = "ctl00_plhMain_lnkChkAppmntAvailability";       //Перевірити доступні для реєстрації дати в кол-центрі
        private static string visaCity = "ctl00_plhMain_cboVAC"; //Візовий Сервіс Центр i Пункт Прийому Візових Анкетx
        private static string visaCategory = "ctl00_plhMain_cboVisaCategory"; //Візова категорія
        private static string buttonSubmit = "ctl00_plhMain_btnSubmit";//Підтвердити                                      private static string regData = "ctl00_plhMain_lblAvailableDateMsg";//Найближча доступна дата для реєстрації
        //second part
        private static string registryId = "ctl00_plhMain_lnkSchApp";// Призначити дату подачі документів
        private static string reason = "ctl00_plhMain_cboPurpose";//Мета візиту
        private static string numOfApplicants = "ctl00_plhMain_tbxNumOfApplicants";//Кількість заявників
        private static string numOfChildrens = "ctl00_plhMain_txtChildren";//К-сть дітей вписаних у паспорт батьків
        private static string receiptNumber = "ctl00_plhMain_repAppReceiptDetails_ctl01_txtReceiptNumber";//Номер квитанції
        private static string buttonSubmitEmail = "ctl00_plhMain_btnSubmitDetails";//Підтвердити імейл
        private static string email = "ctl00_plhMain_txtEmailID";
        private static string passForMail = "ctl00_plhMain_txtPassword";
        private static string infoText = "ctl00_plhMain_lblMsg";//Ок текст - на коли можна зареєструватись
        private static string endPassportDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxPPTEXPDT";//Дата закінчення терміну дії паспорту
        private static string statusField = "ctl00_plhMain_repAppVisaDetails_ctl01_cboTitle";//Статус
        private static string personName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxFName";//Ім'я
        private static string personLastName = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxLName";//Прізвище
        private static string personBirthday = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxDOB";//Дата народження
        private static string returnDate = "ctl00_plhMain_repAppVisaDetails_ctl01_tbxReturn";//Дата повернення
        private static string nationality = "ctl00_plhMain_repAppVisaDetails_ctl01_cboNationality";//Національність
        private static string availableData = "OpenDateAllocated";//Будь ласка, оберіть Вільну дату
        private static string registryTime = "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot";//Будь ласка, оберіть Час

        static void Main(string[] args)
        {
            //Countries();
            SecondPart();
            //ResManager.RegisterResource("uk_UA", uk_UA.ResourceManager);
            //Console.WriteLine(ResManager.GetString("test"));
            //FirefoxDriver driver = new FirefoxDriver();
            ////new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"),
            ////    new FirefoxProfile(@"C:\Users\BohdanPC\AppData\Roaming\Mozilla\Firefox\Profiles\0igfgrhe.default"));
            //driver.Navigate().GoToUrl(mainUrl);
            //Thread.Sleep(3000);
            //IWebElement query = driver.FindElement(By.Id(checkAvailableData));
            //query.Click();//Console.WriteLine("Please type Enter when you finish entering Capcha");
            //Console.ReadLine();
            //query = driver.FindElement(By.Id(visaCity)).FindElement(By.CssSelector("option[value='5']"));
            //query.Click();
            //Thread.Sleep(3000);
            //query = driver.FindElement(By.Id(visaCategory)).FindElement(By.CssSelector("option[value='235']"));
            //query.Click();
            //Console.WriteLine("Please type Enter when you finish entering Capcha");
            //Console.ReadLine();
            //query = driver.FindElement(By.Id(buttonSubmit));
            //query.Click();
            //Thread.Sleep(3000);
            ////query = driver.FindElement(By.Id(regData));
            //Console.WriteLine(query.Text);
            //Console.ReadLine();
            //driver.Quit();
        }

        private static void SecondPart()
        {
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(mainUrl);
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(registryId));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(visaCity)).FindElement(By.CssSelector("option[value='13']"));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(reason)).FindElement(By.CssSelector("option[value='1']"));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(buttonSubmit));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(numOfApplicants));
                query.Clear();
                query.SendKeys("1");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(numOfChildrens));
                query.Clear();
                query.SendKeys("0");
            }
            Thread.Sleep(2000);
            //Console.WriteLine("Capcha");
            //Console.ReadLine();
            {
                var query = driver.FindElement(By.Id(visaCategory)).FindElement(By.CssSelector("option[value='235']"));
                query.Click();
            }
            //Thread.Sleep(2000);
            //{
            //    var query = driver.FindElement(By.Id(infoText));
            //    Console.WriteLine(query.Text);
            //}
            Console.WriteLine("Capcha");
            Console.ReadLine();
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(buttonSubmit));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(receiptNumber));
                query.SendKeys("1409/0164/1651");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(buttonSubmit));
                query.Click();
            }
            //Thread.Sleep(2000);//todo this is need to be checked in release version
            //{
            //var query = driver.FindElement(By.Id(infoText));
            //    Console.WriteLine(query.Text);
            //    Console.ReadLine();
            //}
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(email));
                query.SendKeys("savko76@i.ua");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(passForMail));
                query.SendKeys("QWE1@3ewq");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(buttonSubmitEmail));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(endPassportDate));
                query.SendKeys("18/01/2022");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(statusField)).FindElement(By.CssSelector("option[value='Mr.']"));
                query.Click();
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(personName));
                query.SendKeys("SERHII");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(personLastName));
                query.SendKeys("SAVKO");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(personBirthday));
                query.SendKeys("02/09/1976");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(returnDate));
                query.SendKeys("30/06/2017");
            }
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(nationality));
                query.SendKeys("UKRAINE");
            }
            Console.WriteLine("Capcha");
            Console.ReadLine();
            Thread.Sleep(2000);
            {
                var query = driver.FindElement(By.Id(buttonSubmit));
                query.Click();
            }
            Thread.Sleep(2000);
            Console.WriteLine("Capcha");
            Console.ReadLine();
            Thread.Sleep(2000);
            {
                var queryCollection = driver.FindElements(By.ClassName(availableData));
                foreach (var element in queryCollection)
                {
                    var date = element.Text.ConvertToIntOrNull();
                    if (date != null && date.Value >= 12)
                    {
                        element.Click();
                        break;
                    }
                }
            }
            Thread.Sleep(2000);
            Console.WriteLine("Capcha");
            Console.ReadLine();
            Thread.Sleep(2000);
            //Thread.Sleep(2000);//todo this is need to be checked in release version
            //{
            //var query = driver.FindElement(By.Id(infoText));
            //    Console.WriteLine(query.Text);
            //    Console.ReadLine();
            //}
            {
                var query = driver.FindElement(By.Id(registryTime));
                query.Click();
            }
            Thread.Sleep(2000);
        }

        static void Countries()
        {
            var builder = new List<string>();

            builder.Add("<option value=\"1\">AFGHANISTAN</option>");
            builder.Add("<option value=\"2\">ALBANIA</option>");
            builder.Add("<option value=\"3\">ALGERIA</option>");
            builder.Add("<option value=\"4\">ANDORRA</option>");
            builder.Add("<option value=\"5\">ANGOLA</option>");
            builder.Add("<option value=\"6\">ANGUILLA</option>");
            builder.Add("<option value=\"7\">ANTIGUA &amp; BARBUDA</option>");
            builder.Add("<option value=\"8\">ARGENTINA</option>");
            builder.Add("<option value=\"9\">ARMENIA</option>");
            builder.Add("<option value=\"10\">ARUBA</option>");
            builder.Add("<option value=\"11\">AUSTRALIA</option>");
            builder.Add("<option value=\"12\">AUSTRIA</option>");
            builder.Add("<option value=\"13\">AZERBAIJAN</option>");
            builder.Add("<option value=\"14\">BAHAMAS</option>");
            builder.Add("<option value=\"15\">BAHRAIN</option>");
            builder.Add("<option value=\"16\">BANGLADESH</option>");
            builder.Add("<option value=\"17\">BARBADOS</option>");
            builder.Add("<option value=\"18\">BELARUS</option>");
            builder.Add("<option value=\"19\">BELGIUM</option>");
            builder.Add("<option value=\"20\">BELIZE</option>");
            builder.Add("<option value=\"21\">BENIN</option>");
            builder.Add("<option value=\"22\">BERMUDA ISLANDS</option>");
            builder.Add("<option value=\"23\">BHUTAN</option>");
            builder.Add("<option value=\"24\">BOLIVIA</option>");
            builder.Add("<option value=\"25\">BOSNIA-HERCEGOVINA</option>");
            builder.Add("<option value=\"26\">BOTSWANA</option>");
            builder.Add("<option value=\"27\">BRAZIL</option>");
            builder.Add("<option value=\"28\">BRUNEI</option>");
            builder.Add("<option value=\"29\">BULGARIA</option>");
            builder.Add("<option value=\"30\">BURKINA FASO</option>");
            builder.Add("<option value=\"31\">BURUNDI</option>");
            builder.Add("<option value=\"32\">CAMBODIA</option>");
            builder.Add("<option value=\"33\">CAMEROON</option>");
            builder.Add("<option value=\"34\">CANADA</option>");
            builder.Add("<option value=\"35\">CAPE VERDE</option>");
            builder.Add("<option value=\"36\">CAYMAN ISLANDS</option>");
            builder.Add("<option value=\"37\">CENTRAL AFRICAN REP.</option>");
            builder.Add("<option value=\"38\">CHAD</option>");
            builder.Add("<option value=\"39\">CHILE</option>");
            builder.Add("<option value=\"40\">CHINA</option>");
            builder.Add("<option value=\"41\">COLOMBIA</option>");
            builder.Add("<option value=\"42\">COMOROS</option>");
            builder.Add("<option value=\"43\">CONGO, DEM. REP.</option>");
            builder.Add("<option value=\"44\">CONGO, REP.</option>");
            builder.Add("<option value=\"45\">COSTA RICA</option>");
            builder.Add("<option value=\"46\">CROATIA</option>");
            builder.Add("<option value=\"47\">CUBA</option>");
            builder.Add("<option value=\"48\">CYPRUS</option>");
            builder.Add("<option value=\"49\">CZECH REPUBLIC</option>");
            builder.Add("<option value=\"50\">DENMARK</option>");
            builder.Add("<option value=\"51\">DJIBOUTI</option>");
            builder.Add("<option value=\"52\">DOMINICA</option>");
            builder.Add("<option value=\"53\">DOMINICAN REPUBLIC</option>");
            builder.Add("<option value=\"54\">EAST TIMOR</option>");
            builder.Add("<option value=\"55\">ECUADOR</option>");
            builder.Add("<option value=\"56\">EGYPT</option>");
            builder.Add("<option value=\"57\">EL SALVADOR</option>");
            builder.Add("<option value=\"58\">EQUATORIAL GUINEA</option>");
            builder.Add("<option value=\"59\">ERITREA</option>");
            builder.Add("<option value=\"60\">ESTONIA</option>");
            builder.Add("<option value=\"61\">ETHIOPIA</option>");
            builder.Add("<option value=\"62\">FEDERATED STATES OF MICRONESIA</option>");
            builder.Add("<option value=\"63\">FEDERATION OF SAINT KITTS,CHRISTOPHER AND NEVIS</option>");
            builder.Add("<option value=\"64\">FIJI</option>");
            builder.Add("<option value=\"65\">FINLAND</option>");
            builder.Add("<option value=\"66\">FRANCE</option>");
            builder.Add("<option value=\"67\">GABON</option>");
            builder.Add("<option value=\"68\">GAMBIA</option>");
            builder.Add("<option value=\"69\">GEORGIA</option>");
            builder.Add("<option value=\"70\">GERMANY</option>");
            builder.Add("<option value=\"71\">GHANA</option>");
            builder.Add("<option value=\"72\">GREECE</option>");
            builder.Add("<option value=\"73\">GRENADA</option>");
            builder.Add("<option value=\"74\">GRENADINES</option>");
            builder.Add("<option value=\"75\">GUATEMALA</option>");
            builder.Add("<option value=\"76\">GUINEA</option>");
            builder.Add("<option value=\"77\">GUINEA-BISSAU</option>");
            builder.Add("<option value=\"78\">GUYANA</option>");
            builder.Add("<option value=\"79\">HAITI</option>");
            builder.Add("<option value=\"80\">HONDURAS</option>");
            builder.Add("<option value=\"81\">HONGKONG AND MACAO</option>");
            builder.Add("<option value=\"82\">HUNGARY</option>");
            builder.Add("<option value=\"83\">ICELAND</option>");
            builder.Add("<option value=\"84\">INDIA</option>");
            builder.Add("<option value=\"85\">INDONESIA</option>");
            builder.Add("<option value=\"86\">IRAN</option>");
            builder.Add("<option value=\"87\">IRAQ</option>");
            builder.Add("<option value=\"88\">IRELAND</option>");
            builder.Add("<option value=\"89\">ISRAEL</option>");
            builder.Add("<option value=\"90\">ITALY</option>");
            builder.Add("<option value=\"91\">IVORY COAST</option>");
            builder.Add("<option value=\"92\">JAMAICA</option>");
            builder.Add("<option value=\"93\">JAPAN</option>");
            builder.Add("<option value=\"94\">JORDAN</option>");
            builder.Add("<option value=\"95\">KAZAKSTAN</option>");
            builder.Add("<option value=\"96\">KENYA</option>");
            builder.Add("<option value=\"97\">KIRIBATI</option>");
            builder.Add("<option value=\"98\">KOREA (NORTH-)</option>");
            builder.Add("<option value=\"99\">KUWAIT</option>");
            builder.Add("<option value=\"100\">KYRGYSTAN</option>");
            builder.Add("<option value=\"101\">LAOS</option>");
            builder.Add("<option value=\"102\">LATVIA</option>");
            builder.Add("<option value=\"103\">LEBANON</option>");
            builder.Add("<option value=\"104\">LESOTHO</option>");
            builder.Add("<option value=\"105\">LIBERIA</option>");
            builder.Add("<option value=\"106\">LIBYA</option>");
            builder.Add("<option value=\"107\">LIECHTENSTEIN</option>");
            builder.Add("<option value=\"108\">LITHUANIA</option>");
            builder.Add("<option value=\"109\">LUXEMBOURG</option>");
            builder.Add("<option value=\"110\">MACAU</option>");
            builder.Add("<option value=\"111\">MACEDONIA</option>");
            builder.Add("<option value=\"112\">MADAGASCAR</option>");
            builder.Add("<option value=\"113\">MALAWI</option>");
            builder.Add("<option value=\"114\">MALAYSIA</option>");
            builder.Add("<option value=\"115\">MALDIVES</option>");
            builder.Add("<option value=\"116\">MALI</option>");
            builder.Add("<option value=\"117\">MALTA</option>");
            builder.Add("<option value=\"118\">MARSHALL ISLANDS</option>");
            builder.Add("<option value=\"119\">MAURITANIA</option>");
            builder.Add("<option value=\"120\">MAURITIUS</option>");
            builder.Add("<option value=\"121\">MEXICO</option>");
            builder.Add("<option value=\"122\">MICRONESIA</option>");
            builder.Add("<option value=\"123\">MOLDAVIA</option>");
            builder.Add("<option value=\"124\">MOLDOVA</option>");
            builder.Add("<option value=\"125\">MONACO</option>");
            builder.Add("<option value=\"126\">MONGOLIA</option>");
            builder.Add("<option value=\"127\">MONTENEGRO</option>");
            builder.Add("<option value=\"128\">MONTSERRAT</option>");
            builder.Add("<option value=\"129\">MOROCCO</option>");
            builder.Add("<option value=\"130\">MOZAMBIQUE</option>");
            builder.Add("<option value=\"131\">MYANMAR (BURMA)</option>");
            builder.Add("<option value=\"132\">NA</option>");
            builder.Add("<option value=\"133\">NAMIBIA</option>");
            builder.Add("<option value=\"134\">NAURU</option>");
            builder.Add("<option value=\"135\">NEPAL</option>");
            builder.Add("<option value=\"136\">NETHERLANDS</option>");
            builder.Add("<option value=\"137\">NETHERLANDS ANTILLES</option>");
            builder.Add("<option value=\"138\">NEW ZEALAND</option>");
            builder.Add("<option value=\"139\">NICARAGUA</option>");
            builder.Add("<option value=\"140\">NIGER</option>");
            builder.Add("<option value=\"141\">NIGERIA</option>");
            builder.Add("<option value=\"142\">NON-RUSSIAN</option>");
            builder.Add("<option value=\"143\">NORWAY</option>");
            builder.Add("<option value=\"144\">NOTHERN MARIANA ISLANDS</option>");
            builder.Add("<option value=\"145\">OMAN</option>");
            builder.Add("<option value=\"146\">OTHERS</option>");
            builder.Add("<option value=\"147\">PAKISTAN</option>");
            builder.Add("<option value=\"148\">PALAU ISLANDS</option>");
            builder.Add("<option value=\"149\">PALESTINE</option>");
            builder.Add("<option value=\"150\">PANAMA</option>");
            builder.Add("<option value=\"151\">PAPUA NEW GUINEA</option>");
            builder.Add("<option value=\"152\">PARAGUAY</option>");
            builder.Add("<option value=\"153\">PERU</option>");
            builder.Add("<option value=\"154\">PHILIPPINES</option>");
            builder.Add("<option value=\"155\">POLAND</option>");
            builder.Add("<option value=\"156\">PORTUGAL</option>");
            builder.Add("<option value=\"157\">QATAR</option>");
            builder.Add("<option value=\"158\">REPUBLIC DE COTE DIVOIRE</option>");
            builder.Add("<option value=\"159\">REPUBLIC OF BURUNDI</option>");
            builder.Add("<option value=\"160\">REPUBLIC OF CONGO</option>");
            builder.Add("<option value=\"161\">REPUBLIC OF CROATIA</option>");
            builder.Add("<option value=\"162\">REPUBLIC OF KIRIBATI</option>");
            builder.Add("<option value=\"163\">REPUBLIC OF KOREA</option>");
            builder.Add("<option value=\"164\">REPUBLIC OF KOSOVO</option>");
            builder.Add("<option value=\"165\">REPUBLIC OF MACEDONIA</option>");
            builder.Add("<option value=\"166\">REPUBLIC OF PALAU</option>");
            builder.Add("<option value=\"167\">REPUBLIC OF SLOVENIA</option>");
            builder.Add("<option value=\"168\">REPUBLIC OF THE MARSHALL ISLANDS</option>");
            builder.Add("<option value=\"169\">REUNION ISLANDS</option>");
            builder.Add("<option value=\"170\">ROM</option>");
            builder.Add("<option value=\"171\">ROMANIA</option>");
            builder.Add("<option value=\"174\">RUSSIAN FEDERATION</option>");
            builder.Add("<option value=\"175\">RWANDA</option>");
            builder.Add("<option value=\"176\">SAINT LUCIA</option>");
            builder.Add("<option value=\"177\">SAMOA</option>");
            builder.Add("<option value=\"178\">SAN MARINO</option>");
            builder.Add("<option value=\"179\">SAO TOMÉ &amp; PRINCIPE</option>");
            builder.Add("<option value=\"180\">SAUDI ARABIA</option>");
            builder.Add("<option value=\"181\">SENEGAL</option>");
            builder.Add("<option value=\"182\">SERBIA</option>");
            builder.Add("<option value=\"183\">SEYCHELLES</option>");
            builder.Add("<option value=\"184\">SIERRA LEONE</option>");
            builder.Add("<option value=\"185\">SINGAPORE</option>");
            builder.Add("<option value=\"186\">SLOVAK REPUBLIC</option>");
            builder.Add("<option value=\"187\">SLOVENIA</option>");
            builder.Add("<option value=\"188\">SOLOMON ISLANDS</option>");
            builder.Add("<option value=\"189\">SOMALIA</option>");
            builder.Add("<option value=\"190\">SOUTH AFRICA</option>");
            builder.Add("<option value=\"191\">SPAIN</option>");
            builder.Add("<option value=\"192\">SRI LANKA</option>");
            builder.Add("<option value=\"193\">ST. KITTS &amp; NEVIS</option>");
            builder.Add("<option value=\"194\">ST. LUCIA</option>");
            builder.Add("<option value=\"195\">ST. VINCENT &amp; THE</option>");
            builder.Add("<option value=\"196\">STATE OF ERITREA</option>");
            builder.Add("<option value=\"197\">SUDAN</option>");
            builder.Add("<option value=\"198\">SURINAM</option>");
            builder.Add("<option value=\"199\">SWAZILAND</option>");
            builder.Add("<option value=\"200\">SWEDEN</option>");
            builder.Add("<option value=\"201\">SWITZERLAND</option>");
            builder.Add("<option value=\"202\">SYRIA</option>");
            builder.Add("<option value=\"203\">TAIWAN</option>");
            builder.Add("<option value=\"204\">TAJIKISTAN</option>");
            builder.Add("<option value=\"205\">TANZANIA</option>");
            builder.Add("<option value=\"206\">THAILAND</option>");
            builder.Add("<option value=\"207\">THE BAHAMAS</option>");
            builder.Add("<option value=\"208\">THE PHILIPPINES</option>");
            builder.Add("<option value=\"209\">TIBET</option>");
            builder.Add("<option value=\"210\">TOGO</option>");
            builder.Add("<option value=\"211\">TONGA</option>");
            builder.Add("<option value=\"212\">TRINIDAD &amp; TOBAGO</option>");
            builder.Add("<option value=\"213\">TUNISIA</option>");
            builder.Add("<option value=\"214\">TURKEY</option>");
            builder.Add("<option value=\"215\">TURKMENISTAN</option>");
            builder.Add("<option value=\"217\">TUVALU</option>");
            builder.Add("<option value=\"218\">UGANDA</option>");
            builder.Add("<option value=\"219\">UKRAINE</option>");
            builder.Add("<option value=\"216\">Ukrainians</option>");
            builder.Add("<option value=\"220\">UN NATION</option>");
            builder.Add("<option value=\"221\">UN OFFICIAL</option>");
            builder.Add("<option value=\"222\">UNITED ARAB EMIRATES</option>");
            builder.Add("<option value=\"223\">UNITED KINGDOM</option>");
            builder.Add("<option value=\"224\">UNITED NATIONS ORGANIZATION</option>");
            builder.Add("<option value=\"225\">UNITED STATES OF AMERICA</option>");
            builder.Add("<option value=\"226\">URUGUAY</option>");
            builder.Add("<option value=\"227\">UZBEKISTAN</option>");
            builder.Add("<option value=\"228\">VANUATU</option>");
            builder.Add("<option value=\"229\">VATICAN CITY (HOLY SEE)</option>");
            builder.Add("<option value=\"230\">VENEZUELA</option>");
            builder.Add("<option value=\"231\">VIETNAM</option>");
            builder.Add("<option value=\"232\">YEMEN</option>");
            builder.Add("<option value=\"233\">YUGOSLAVIA</option>");
            builder.Add("<option value=\"234\">ZAMBIA</option>");
            builder.Add("<option value=\"235\">ZIMBABWE</option>");

            GenerateCode(builder);
        }

        static void GenerateCode(List<string> builder)
        {
            var resList = new List<string>();

            foreach (var line in builder)
            {
                var splitOne = line.Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                var value = splitOne[0].Split(new[] { '=', '"' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var name = splitOne[1].Split(new[] { '<' }, StringSplitOptions.RemoveEmptyEntries)[0];
                resList.Add(" countryRow = _dataSet.Choice.NewChoiceRow();");
                resList.Add($" countryRow.Name = \"{name}\";");
                resList.Add($" countryRow.Value = \"{value}\";");
                resList.Add(" countryRow.Type = (short)ChoicesType.Country;");
                resList.Add(" _dataSet.Choice.AddChoiceRow(countryRow);");
                resList.Add(" ");
            }
            System.IO.File.WriteAllLines(@".\out.txt", resList);
        }
    }
}
