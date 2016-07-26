using System;
using System.Data;
using System.Windows.Forms;
using Visa.License.DB;

namespace Visa.LicenseManager
{
    public partial class LicenseRegManager : DevExpress.XtraEditors.XtraForm
    {
        private ModelLicenseDBDataContext _context = new ModelLicenseDBDataContext();

        public LicenseRegManager()
        {
            InitializeComponent();
        }

        private void LicenseRegManager_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'licenseDBDataSet.Instances' table. You can move, or remove it, as needed.
            this.instancesTableAdapter.Fill(this.licenseDBDataSet.Instances);
            FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void butPcName_Click(object sender, EventArgs e)
        {
            var row = gridView2.GetFocusedDataRow();

            if (row["PcName"] != null && !string.IsNullOrWhiteSpace(row["PcName"].ToString()))
            {
                row["PcName"] = DBNull.Value;
                instancesTableAdapter.Update(licenseDBDataSet);
                gridView2.RefreshData();
            }
        }

        private void butAll_Click(object sender, EventArgs e)
        {
            instancesTableAdapter.Delete(
                gridView2.GetFocusedDataRow()["Guid"].ToString());
            gridView2.GetFocusedDataRow().Delete();
            gridView2.RefreshData();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            var newRow = licenseDBDataSet.Instances.NewRow();

            newRow["Guid"] = Guid.NewGuid().ToString();
            licenseDBDataSet.Instances.Rows.Add(newRow);
            instancesTableAdapter.Update(licenseDBDataSet);
            gridView2.RefreshData();
        }

        private void butAllKeys_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in licenseDBDataSet.Instances.Rows)
                row.Delete();
            instancesTableAdapter.Update(licenseDBDataSet);
            gridView2.RefreshData();
        }

        private void butCopyKey_Click(object sender, EventArgs e)
        {
            var row = gridView2.GetFocusedDataRow();
            Clipboard.SetText(row["Guid"].ToString());
        }

        private void butUpdateNow_Click(object sender, EventArgs e)
        {
            this.instancesTableAdapter.Fill(this.licenseDBDataSet.Instances);
            gridView2.RefreshData();
        }
    }
}
