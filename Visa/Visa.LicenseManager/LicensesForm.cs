using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Visa.LicenseManager
{
    public partial class LicensesForm : Form
    {
        private const int IncorrectRowHandle = -2147483647;

        public LicensesForm()
        {
            InitializeComponent();
            Closing += LicensesForm_Closing;
            GotFocus += LicensesForm_GotFocus;
        }

        private void LicensesForm_GotFocus(object sender,
            System.EventArgs e)
        {
            licensesTableAdapter.Fill(visaLicensesDataSet.Licenses);
        }

        public bool ToClose { get; set; }

        private void LicensesForm_Closing(object sender,
            CancelEventArgs e)
        {
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        public virtual void Save()
        {
            ChangeRow();
            licensesTableAdapter.Update(visaLicensesDataSet.Licenses);
        }

        public virtual void Cancel()
        {
            Close();
        }

        private void ChangeRow()
        {
            var rowId = gridView1.FocusedRowHandle;
            gridView1.FocusedRowHandle = IncorrectRowHandle;
            gridView1.Focus();
            gridView1.FocusedRowHandle = rowId;
            gridView1.Focus();
        }

        public virtual void RefreshForm()
        {
            ChangeRow();
            licensesTableAdapter.Fill(visaLicensesDataSet.Licenses);
            gridView1.RefreshData();
        }

        public virtual void Delete()
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        public void AddNew(int customerId)
        {
            var newRow = visaLicensesDataSet.Licenses.NewLicensesRow();

            newRow.Guid = Guid.NewGuid()
                .ToString();
            newRow.CustomerID = customerId;

            visaLicensesDataSet.Licenses.Rows.Add(newRow);
            gridView1.RefreshData();
        }

        public void DeleteAll()
        {
            foreach (DataRow row in visaLicensesDataSet.Licenses.Rows)
                row.Delete();
            gridView1.RefreshData();
        }

        public void CopyKey()
        {
            var row = gridView1.GetFocusedDataRow()
                as VisaLicensesDataSet.LicensesRow;
            if (row != null)
                Clipboard.SetText(row.Guid);
        }
    }
}