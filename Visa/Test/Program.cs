using System.Linq;

namespace Test
{
    class Program
    {
        static DataSet1 ds = new DataSet1();

        static void Main(string[] args)
        {
            ds.EnforceConstraints = false;
            DataSet1.DataTable1Row row = ds.DataTable1.NewDataTable1Row();
            row.DataColumn1 = "A";
            row.DataColumn2 = "B";
            ds.DataTable1.Rows.Add(row);
            row = ds.DataTable1.NewDataTable1Row();
            row.DataColumn1 = "B";
            row.DataColumn2 = "C";
            ds.DataTable1.Rows.Add(row);
            row = ds.DataTable1.NewDataTable1Row();
            row.DataColumn1 = "C";
            row.DataColumn2 = "A";
            ds.DataTable1.Rows.Add(row);
            ds.EnforceConstraints = true;
            ds.AcceptChanges();
        }


        private static string obj;
        public static bool Check()
        {
            var bres = true;

            foreach (DataSet1.DataTable1Row row in ds.DataTable1.Rows)
            {
                if (row.DataColumn2 != null)
                {
                    if (row.DataColumn2 == obj)
                    {
                        bres = false;
                        break;
                    }
                    if (obj == null)
                    {
                        obj = row.DataColumn1;
                    }
                    var row2 =
                        ds.DataTable1.Rows.Cast<DataSet1.DataTable1Row>()
                            .First(r => r.DataColumn1 == row.DataColumn2);

                }
            }

            return bres;
        }
    }
}