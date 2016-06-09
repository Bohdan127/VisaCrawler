namespace Visa.Database
{
    public static class InstanceProvider
    {
        public static bool Initialized { get; private set; }
        private static VisaDataSet _dataSet;

        static InstanceProvider()
        {
            _dataSet = new VisaDataSet();
        }

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
            Initialized = true;
        }

        /// <summary>
        /// Insert into dataset all default Visa Categories
        /// </summary>
        private static void InitVicaCategories()
        {
            var catRow = _dataSet.VisaCategory.NewVisaCategoryRow();
            catRow.Name = "Місцевий Прикордонний Рух";
            catRow.Value = 248;
            _dataSet.VisaCategory.AddVisaCategoryRow(catRow);

            catRow = _dataSet.VisaCategory.NewVisaCategoryRow();
            catRow.Name = "Національна Віза";
            catRow.Value = 235;
            _dataSet.VisaCategory.AddVisaCategoryRow(catRow);

            catRow = _dataSet.VisaCategory.NewVisaCategoryRow();
            catRow.Name = "Шенгенська Віза";
            catRow.Value = 229;
            _dataSet.VisaCategory.AddVisaCategoryRow(catRow);

            catRow = _dataSet.VisaCategory.NewVisaCategoryRow();
            catRow.Name = "Шенгенська туристична";
            catRow.Value = 249;
            _dataSet.VisaCategory.AddVisaCategoryRow(catRow);
        }

        /// <summary>
        /// Insert into dataset all default Service Centers
        /// </summary>
        private static void InitServiceCenters()
        {
            var servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Івано-Франківськ";
            servCentRow.Value = 5;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Львів";
            servCentRow.Value = 7;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Рівне";
            servCentRow.Value = 9;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Луцьк";
            servCentRow.Value = 10;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Діпропетровськ";
            servCentRow.Value = 11;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Харків";
            servCentRow.Value = 12;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Київ";
            servCentRow.Value = 13;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Одеса";
            servCentRow.Value = 14;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Хмельницький";
            servCentRow.Value = 15;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Житомир";
            servCentRow.Value = 16;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Вінниця";
            servCentRow.Value = 17;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Ужгород";
            servCentRow.Value = 20;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            servCentRow = _dataSet.ServiceCenter.NewServiceCenterRow();
            servCentRow.Name = "Польщі Чернівці";
            servCentRow.Value = 21;
            _dataSet.ServiceCenter.AddServiceCenterRow(servCentRow);

            _dataSet.AcceptChanges();
        }
    }
}
