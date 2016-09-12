using System;
using System.ComponentModel;

namespace Visa.LicenseManager
{
    public partial class CustomersForm : DevExpress.XtraEditors.XtraForm
    {
        private const int IncorrectRowHandle = -2147483647;

        public CustomersForm()
        {
            InitializeComponent();
            Closing += CustomersForm_Closing;
            GotFocus += CustomersForm_GotFocus;
        }

        private void CustomersForm_GotFocus(object sender, EventArgs e)
        {
            customersTableAdapter.Fill(visaLicensesDataSet.Customers);
        }

        public bool ToClose { get; set; }

        private void CustomersForm_Closing(object sender,
                CancelEventArgs e)
        {
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        public virtual void Save()
        {
            ChangeRow();
            customersTableAdapter.Update(visaLicensesDataSet.Customers);
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
            customersTableAdapter.Fill(visaLicensesDataSet.Customers);
            gridView1.RefreshData();
        }

        public virtual void Delete()
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
    }
}