using Visa.Database.Enums;

namespace Visa.Database
{
    public static class InstanceProvider
    {
        private static readonly VisaDataSet _dataSet;

        static InstanceProvider()
        {
            _dataSet = new VisaDataSet();
        }

        public static bool Initialized { get; private set; }

        public static VisaDataSet DataSet
        {
            get
            {
                if (!Initialized)
                    InitDefaults();

                return _dataSet;
            }
        }

        private static void InitDefaults()
        {
            InitServiceCenters();
            InitVicaCategories();
            InitReasonType();
            InitStatusType();
            InitCountries();
            InitRegistryTime();
            Initialized = true;
        }

        /// <summary>
        ///     Insert into dataset all default times for Registry
        /// </summary>
        private static void InitRegistryTime()
        {
            var countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "08:30";
            countryRow.Value = "ctl00_plhMain_gvSlot_ctl02_lnkTimeSlot";
            countryRow.Type = (short) ChoicesType.RegistryTime;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "09:00";
            countryRow.Value = "ctl00_plhMain_gvSlot_ctl03_lnkTimeSlot";
            countryRow.Type = (short) ChoicesType.RegistryTime;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "09:30";
            countryRow.Value = "ctl00_plhMain_gvSlot_ctl04_lnkTimeSlot";
            countryRow.Type = (short) ChoicesType.RegistryTime;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "10:00";
            countryRow.Value = "ctl00_plhMain_gvSlot_ctl05_lnkTimeSlot";
            countryRow.Type = (short) ChoicesType.RegistryTime;
            _dataSet.Choice.AddChoiceRow(countryRow);
            _dataSet.AcceptChanges();
        }

        /// <summary>
        ///     Insert into dataset all default Countries
        /// </summary>
        private static void InitCountries()
        {
            var countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "AFGHANISTAN";
            countryRow.Value = "1";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ALBANIA";
            countryRow.Value = "2";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ALGERIA";
            countryRow.Value = "3";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ANDORRA";
            countryRow.Value = "4";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ANGOLA";
            countryRow.Value = "5";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ANGUILLA";
            countryRow.Value = "6";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ANTIGUA-BARBUDA";
            countryRow.Value = "7";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ARGENTINA";
            countryRow.Value = "8";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ARMENIA";
            countryRow.Value = "9";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ARUBA";
            countryRow.Value = "10";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "AUSTRALIA";
            countryRow.Value = "11";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "AUSTRIA";
            countryRow.Value = "12";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "AZERBAIJAN";
            countryRow.Value = "13";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BAHAMAS";
            countryRow.Value = "14";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BAHRAIN";
            countryRow.Value = "15";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BANGLADESH";
            countryRow.Value = "16";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BARBADOS";
            countryRow.Value = "17";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BELARUS";
            countryRow.Value = "18";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BELGIUM";
            countryRow.Value = "19";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BELIZE";
            countryRow.Value = "20";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BENIN";
            countryRow.Value = "21";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BERMUDA ISLANDS";
            countryRow.Value = "22";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BHUTAN";
            countryRow.Value = "23";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BOLIVIA";
            countryRow.Value = "24";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BOSNIA-HERCEGOVINA";
            countryRow.Value = "25";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BOTSWANA";
            countryRow.Value = "26";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BRAZIL";
            countryRow.Value = "27";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BRUNEI";
            countryRow.Value = "28";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BULGARIA";
            countryRow.Value = "29";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BURKINA FASO";
            countryRow.Value = "30";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "BURUNDI";
            countryRow.Value = "31";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CAMBODIA";
            countryRow.Value = "32";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CAMEROON";
            countryRow.Value = "33";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CANADA";
            countryRow.Value = "34";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CAPE VERDE";
            countryRow.Value = "35";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CAYMAN ISLANDS";
            countryRow.Value = "36";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CENTRAL AFRICAN REP.";
            countryRow.Value = "37";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CHAD";
            countryRow.Value = "38";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CHILE";
            countryRow.Value = "39";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CHINA";
            countryRow.Value = "40";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "COLOMBIA";
            countryRow.Value = "41";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "COMOROS";
            countryRow.Value = "42";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CONGO, DEM. REP.";
            countryRow.Value = "43";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CONGO, REP.";
            countryRow.Value = "44";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "COSTA RICA";
            countryRow.Value = "45";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CROATIA";
            countryRow.Value = "46";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CUBA";
            countryRow.Value = "47";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CYPRUS";
            countryRow.Value = "48";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "CZECH REPUBLIC";
            countryRow.Value = "49";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "DENMARK";
            countryRow.Value = "50";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "DJIBOUTI";
            countryRow.Value = "51";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "DOMINICA";
            countryRow.Value = "52";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "DOMINICAN REPUBLIC";
            countryRow.Value = "53";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "EAST TIMOR";
            countryRow.Value = "54";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ECUADOR";
            countryRow.Value = "55";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "EGYPT";
            countryRow.Value = "56";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "EL SALVADOR";
            countryRow.Value = "57";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "EQUATORIAL GUINEA";
            countryRow.Value = "58";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ERITREA";
            countryRow.Value = "59";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ESTONIA";
            countryRow.Value = "60";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ETHIOPIA";
            countryRow.Value = "61";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "FEDERATED STATES OF MICRONESIA";
            countryRow.Value = "62";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "FEDERATION OF SAINT KITTS,CHRISTOPHER AND NEVIS";
            countryRow.Value = "63";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "FIJI";
            countryRow.Value = "64";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "FINLAND";
            countryRow.Value = "65";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "FRANCE";
            countryRow.Value = "66";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GABON";
            countryRow.Value = "67";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GAMBIA";
            countryRow.Value = "68";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GEORGIA";
            countryRow.Value = "69";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GERMANY";
            countryRow.Value = "70";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GHANA";
            countryRow.Value = "71";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GREECE";
            countryRow.Value = "72";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GRENADA";
            countryRow.Value = "73";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GRENADINES";
            countryRow.Value = "74";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GUATEMALA";
            countryRow.Value = "75";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GUINEA";
            countryRow.Value = "76";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GUINEA-BISSAU";
            countryRow.Value = "77";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "GUYANA";
            countryRow.Value = "78";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "HAITI";
            countryRow.Value = "79";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "HONDURAS";
            countryRow.Value = "80";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "HONGKONG AND MACAO";
            countryRow.Value = "81";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "HUNGARY";
            countryRow.Value = "82";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ICELAND";
            countryRow.Value = "83";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "INDIA";
            countryRow.Value = "84";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "INDONESIA";
            countryRow.Value = "85";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "IRAN";
            countryRow.Value = "86";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "IRAQ";
            countryRow.Value = "87";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "IRELAND";
            countryRow.Value = "88";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ISRAEL";
            countryRow.Value = "89";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ITALY";
            countryRow.Value = "90";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "IVORY COAST";
            countryRow.Value = "91";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "JAMAICA";
            countryRow.Value = "92";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "JAPAN";
            countryRow.Value = "93";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "JORDAN";
            countryRow.Value = "94";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KAZAKSTAN";
            countryRow.Value = "95";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KENYA";
            countryRow.Value = "96";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KIRIBATI";
            countryRow.Value = "97";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KOREA (NORTH-)";
            countryRow.Value = "98";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KUWAIT";
            countryRow.Value = "99";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "KYRGYSTAN";
            countryRow.Value = "100";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LAOS";
            countryRow.Value = "101";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LATVIA";
            countryRow.Value = "102";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LEBANON";
            countryRow.Value = "103";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LESOTHO";
            countryRow.Value = "104";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LIBERIA";
            countryRow.Value = "105";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LIBYA";
            countryRow.Value = "106";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LIECHTENSTEIN";
            countryRow.Value = "107";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LITHUANIA";
            countryRow.Value = "108";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "LUXEMBOURG";
            countryRow.Value = "109";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MACAU";
            countryRow.Value = "110";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MACEDONIA";
            countryRow.Value = "111";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MADAGASCAR";
            countryRow.Value = "112";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MALAWI";
            countryRow.Value = "113";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MALAYSIA";
            countryRow.Value = "114";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MALDIVES";
            countryRow.Value = "115";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MALI";
            countryRow.Value = "116";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MALTA";
            countryRow.Value = "117";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MARSHALL ISLANDS";
            countryRow.Value = "118";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MAURITANIA";
            countryRow.Value = "119";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MAURITIUS";
            countryRow.Value = "120";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MEXICO";
            countryRow.Value = "121";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MICRONESIA";
            countryRow.Value = "122";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MOLDAVIA";
            countryRow.Value = "123";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MOLDOVA";
            countryRow.Value = "124";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MONACO";
            countryRow.Value = "125";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MONGOLIA";
            countryRow.Value = "126";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MONTENEGRO";
            countryRow.Value = "127";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MONTSERRAT";
            countryRow.Value = "128";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MOROCCO";
            countryRow.Value = "129";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MOZAMBIQUE";
            countryRow.Value = "130";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "MYANMAR (BURMA)";
            countryRow.Value = "131";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NA";
            countryRow.Value = "132";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NAMIBIA";
            countryRow.Value = "133";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NAURU";
            countryRow.Value = "134";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NEPAL";
            countryRow.Value = "135";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NETHERLANDS";
            countryRow.Value = "136";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NETHERLANDS ANTILLES";
            countryRow.Value = "137";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NEW ZEALAND";
            countryRow.Value = "138";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NICARAGUA";
            countryRow.Value = "139";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NIGER";
            countryRow.Value = "140";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NIGERIA";
            countryRow.Value = "141";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NON-RUSSIAN";
            countryRow.Value = "142";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NORWAY";
            countryRow.Value = "143";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "NOTHERN MARIANA ISLANDS";
            countryRow.Value = "144";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "OMAN";
            countryRow.Value = "145";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "OTHERS";
            countryRow.Value = "146";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PAKISTAN";
            countryRow.Value = "147";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PALAU ISLANDS";
            countryRow.Value = "148";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PALESTINE";
            countryRow.Value = "149";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PANAMA";
            countryRow.Value = "150";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PAPUA NEW GUINEA";
            countryRow.Value = "151";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PARAGUAY";
            countryRow.Value = "152";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PERU";
            countryRow.Value = "153";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PHILIPPINES";
            countryRow.Value = "154";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "POLAND";
            countryRow.Value = "155";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "PORTUGAL";
            countryRow.Value = "156";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "QATAR";
            countryRow.Value = "157";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC DE COTE DIVOIRE";
            countryRow.Value = "158";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF BURUNDI";
            countryRow.Value = "159";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF CONGO";
            countryRow.Value = "160";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF CROATIA";
            countryRow.Value = "161";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF KIRIBATI";
            countryRow.Value = "162";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF KOREA";
            countryRow.Value = "163";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF KOSOVO";
            countryRow.Value = "164";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF MACEDONIA";
            countryRow.Value = "165";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF PALAU";
            countryRow.Value = "166";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF SLOVENIA";
            countryRow.Value = "167";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REPUBLIC OF THE MARSHALL ISLANDS";
            countryRow.Value = "168";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "REUNION ISLANDS";
            countryRow.Value = "169";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ROM";
            countryRow.Value = "170";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ROMANIA";
            countryRow.Value = "171";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "RUSSIAN FEDERATION";
            countryRow.Value = "174";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "RWANDA";
            countryRow.Value = "175";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SAINT LUCIA";
            countryRow.Value = "176";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SAMOA";
            countryRow.Value = "177";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SAN MARINO";
            countryRow.Value = "178";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SAO TOMÉ-PRINCIPE";
            countryRow.Value = "179";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SAUDI ARABIA";
            countryRow.Value = "180";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SENEGAL";
            countryRow.Value = "181";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SERBIA";
            countryRow.Value = "182";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SEYCHELLES";
            countryRow.Value = "183";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SIERRA LEONE";
            countryRow.Value = "184";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SINGAPORE";
            countryRow.Value = "185";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SLOVAK REPUBLIC";
            countryRow.Value = "186";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SLOVENIA";
            countryRow.Value = "187";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SOLOMON ISLANDS";
            countryRow.Value = "188";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SOMALIA";
            countryRow.Value = "189";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SOUTH AFRICA";
            countryRow.Value = "190";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SPAIN";
            countryRow.Value = "191";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SRI LANKA";
            countryRow.Value = "192";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ST. KITTS-NEVIS";
            countryRow.Value = "193";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ST. LUCIA";
            countryRow.Value = "194";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ST. VINCENT-THE";
            countryRow.Value = "195";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "STATE OF ERITREA";
            countryRow.Value = "196";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SUDAN";
            countryRow.Value = "197";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SURINAM";
            countryRow.Value = "198";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SWAZILAND";
            countryRow.Value = "199";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SWEDEN";
            countryRow.Value = "200";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SWITZERLAND";
            countryRow.Value = "201";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "SYRIA";
            countryRow.Value = "202";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TAIWAN";
            countryRow.Value = "203";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TAJIKISTAN";
            countryRow.Value = "204";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TANZANIA";
            countryRow.Value = "205";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "THAILAND";
            countryRow.Value = "206";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "THE BAHAMAS";
            countryRow.Value = "207";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "THE PHILIPPINES";
            countryRow.Value = "208";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TIBET";
            countryRow.Value = "209";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TOGO";
            countryRow.Value = "210";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TONGA";
            countryRow.Value = "211";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TRINIDAD-TOBAGO";
            countryRow.Value = "212";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TUNISIA";
            countryRow.Value = "213";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TURKEY";
            countryRow.Value = "214";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TURKMENISTAN";
            countryRow.Value = "215";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "TUVALU";
            countryRow.Value = "217";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UGANDA";
            countryRow.Value = "218";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UKRAINE";
            countryRow.Value = "219";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "Ukrainians";
            countryRow.Value = "216";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UN NATION";
            countryRow.Value = "220";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UN OFFICIAL";
            countryRow.Value = "221";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UNITED ARAB EMIRATES";
            countryRow.Value = "222";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UNITED KINGDOM";
            countryRow.Value = "223";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UNITED NATIONS ORGANIZATION";
            countryRow.Value = "224";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UNITED STATES OF AMERICA";
            countryRow.Value = "225";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "URUGUAY";
            countryRow.Value = "226";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "UZBEKISTAN";
            countryRow.Value = "227";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "VANUATU";
            countryRow.Value = "228";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "VATICAN CITY (HOLY SEE)";
            countryRow.Value = "229";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "VENEZUELA";
            countryRow.Value = "230";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "VIETNAM";
            countryRow.Value = "231";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "YEMEN";
            countryRow.Value = "232";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "YUGOSLAVIA";
            countryRow.Value = "233";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ZAMBIA";
            countryRow.Value = "234";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);

            countryRow = _dataSet.Choice.NewChoiceRow();
            countryRow.Name = "ZIMBABWE";
            countryRow.Value = "235";
            countryRow.Type = (short) ChoicesType.Country;
            _dataSet.Choice.AddChoiceRow(countryRow);
            _dataSet.AcceptChanges();
        }

        /// <summary>
        ///     Insert into dataset all default Status Types
        /// </summary>
        private static void InitStatusType()
        {
            var statusRow = _dataSet.Choice.NewChoiceRow();
            statusRow.Name = "Dr.";
            statusRow.Value = "Dr.";
            statusRow.Type = (short) ChoicesType.StatusType;
            _dataSet.Choice.AddChoiceRow(statusRow);

            statusRow = _dataSet.Choice.NewChoiceRow();
            statusRow.Name = "Mr.";
            statusRow.Value = "Mr.";
            statusRow.Type = (short) ChoicesType.StatusType;
            _dataSet.Choice.AddChoiceRow(statusRow);

            statusRow = _dataSet.Choice.NewChoiceRow();
            statusRow.Name = "Mrs.";
            statusRow.Value = "Mrs.";
            statusRow.Type = (short) ChoicesType.StatusType;
            _dataSet.Choice.AddChoiceRow(statusRow);

            statusRow = _dataSet.Choice.NewChoiceRow();
            statusRow.Name = "Ms.";
            statusRow.Value = "Ms.";
            statusRow.Type = (short) ChoicesType.StatusType;
            _dataSet.Choice.AddChoiceRow(statusRow);

            statusRow = _dataSet.Choice.NewChoiceRow();
            statusRow.Name = "Mstr.";
            statusRow.Value = "Mstr.";
            statusRow.Type = (short) ChoicesType.StatusType;
            _dataSet.Choice.AddChoiceRow(statusRow);
            _dataSet.AcceptChanges();
        }

        /// <summary>
        ///     Insert into dataset all default Reasons Types
        /// </summary>
        private static void InitReasonType()
        {
            var reasonRow = _dataSet.Choice.NewChoiceRow();
            reasonRow.Name = "Подача документів";
            reasonRow.Value = "1";
            reasonRow.Type = (short) ChoicesType.ReasonType;
            _dataSet.Choice.AddChoiceRow(reasonRow);

            reasonRow = _dataSet.Choice.NewChoiceRow();
            reasonRow.Name = "Консультація";
            reasonRow.Value = "2";
            reasonRow.Type = (short) ChoicesType.ReasonType;
            _dataSet.Choice.AddChoiceRow(reasonRow);

            _dataSet.AcceptChanges();
        }

        /// <summary>
        ///     Insert into dataset all default Visa Categories
        /// </summary>
        private static void InitVicaCategories()
        {
            var catRow = _dataSet.Choice.NewChoiceRow();
            catRow.Name = "Місцевий Прикордонний Рух";
            catRow.Value = "248";
            catRow.Type = (short) ChoicesType.VisaCategory;
            _dataSet.Choice.AddChoiceRow(catRow);

            catRow = _dataSet.Choice.NewChoiceRow();
            catRow.Name = "Національна Віза";
            catRow.Value = "235";
            catRow.Type = (short) ChoicesType.VisaCategory;
            _dataSet.Choice.AddChoiceRow(catRow);

            catRow = _dataSet.Choice.NewChoiceRow();
            catRow.Name = "Шенгенська Віза";
            catRow.Value = "229";
            catRow.Type = (short) ChoicesType.VisaCategory;
            _dataSet.Choice.AddChoiceRow(catRow);

            catRow = _dataSet.Choice.NewChoiceRow();
            catRow.Name = "Шенгенська туристична";
            catRow.Value = "249";
            catRow.Type = (short) ChoicesType.VisaCategory;
            _dataSet.Choice.AddChoiceRow(catRow);

            _dataSet.AcceptChanges();
        }

        /// <summary>
        ///     Insert into dataset all default Service Centers
        /// </summary>
        private static void InitServiceCenters()
        {
            var servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Івано-Франківськ";
            servCentRow.Value = "5";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Львів";
            servCentRow.Value = "7";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Рівне";
            servCentRow.Value = "9";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Луцьк";
            servCentRow.Value = "10";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Діпропетровськ";
            servCentRow.Value = "11";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Харків";
            servCentRow.Value = "12";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Київ";
            servCentRow.Value = "13";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Одеса";
            servCentRow.Value = "14";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Хмельницький";
            servCentRow.Value = "15";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Житомир";
            servCentRow.Value = "16";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Вінниця";
            servCentRow.Value = "17";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Ужгород";
            servCentRow.Value = "20";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            servCentRow = _dataSet.Choice.NewChoiceRow();
            servCentRow.Name = "Польщі Чернівці";
            servCentRow.Value = "21";
            servCentRow.Type = (short) ChoicesType.ServiceCenter;
            _dataSet.Choice.AddChoiceRow(servCentRow);

            _dataSet.AcceptChanges();
        }
    }
}