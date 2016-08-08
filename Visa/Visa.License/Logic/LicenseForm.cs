using DevExpress.XtraEditors;
using NLog;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ToolsPortable;
using Visa.License.DB;
using Visa.Resources;

namespace Visa.License.Logic
{
    public partial class LicenseForm : Form
    {
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        private readonly ModelLicenseDBDataContext _context =
            new ModelLicenseDBDataContext();

        public LicenseForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public bool IsWrong { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRegistered { get; set; }

        public string LicenseKey => textEdit1.EditValue.ToString();

        public bool CheckInstance(string guid)
        {
            var bRes = false;

            var isMatched = Regex.IsMatch(guid,
                textEdit1.Properties.Mask.EditMask);

            textEdit1.EditValue = guid;

            if (isMatched)
                bRes = CheckIsRegistered();

            return bRes;
        }

        protected bool CheckIsRegistered()
        {
            var bRes = true;

            var isMatched =
                Regex.IsMatch(
                    textEdit1.EditValue.ConvertToStringOrNull() ?? string.Empty,
                    textEdit1.Properties.Mask.EditMask);

            if (isMatched)
            {
                Instance inst = null;

                for (var j = 0;
                    j < 3;
                    j++)
                {
                    try
                    {
                        inst = _context.Instances
                            .FirstOrDefault(
                                i => i.Guid == textEdit1.EditValue.ToString());
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                        _logger.Error(ex.StackTrace);
                    }
                }

                if (inst != null)
                {
                    if (string.IsNullOrWhiteSpace(inst.PcName))
                    {
                        inst.PcName = Environment.MachineName;
                        _context.SubmitChanges();
                    }
                    else if (inst.PcName != Environment.MachineName)
                    {
                        bRes = false;
                        XtraMessageBox.Show(
                            ResManager.GetString(ResKeys.Key_Used),
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bRes = false;
                    XtraMessageBox.Show(
                        ResManager.GetString(ResKeys.KeyNotFound),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            IsRegistered = bRes;
            return bRes;
        }

        private void applyButton_Click(object sender,
            EventArgs e)
        {
            if (CheckIsRegistered())
            {
                Close();
            }
        }
    }
}