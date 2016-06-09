using System;
using System.Linq;
using System.Windows.Forms;
using Visa.Database;

namespace Visa.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lookUpEditServiceCenter.Properties.DataSource = InstanceProvider.DataSet.ServiceCenter.ToList();
            lookUpEditVisaCategory.Properties.DataSource = InstanceProvider.DataSet.VisaCategory.ToList();
        }
    }
}
