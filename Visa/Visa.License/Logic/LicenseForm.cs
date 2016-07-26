using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Visa.License.Logic
{
    public partial class LicenseForm : System.Windows.Forms.Form
    {
        private ModelLicenseDBDataContext context = new ModelLicenseDBDataContext();

        public LicenseForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public bool IsWrong { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRegistered { get; set; }

        public string LicenseKey { get { return textEdit1.EditValue.ToString(); } }

        public bool CheckInstance(string guid)
        {
            bool bRes = false;

            var isMatched = Regex.IsMatch(guid, textEdit1.Properties.Mask.EditMask);

            textEdit1.EditValue = guid;

            if (isMatched)
            {
                bRes = CheckIsRegistered();
            }

            return bRes;
        }

        protected bool CheckIsRegistered()
        {
            bool bRes = true;

            var isMatched = Regex.IsMatch(textEdit1.EditValue.ToString(), textEdit1.Properties.Mask.EditMask);

            if (isMatched)
            {
                Instance inst = null;
                bool complete = false;

                for (int j = 0; j < 3; j++)
                {
                    if (!complete)
                    {
                        try
                        {
                            inst = context.Instances
                                .FirstOrDefault(i => i.Guid == textEdit1.EditValue.ToString());
                            complete = true;
                        }
                        catch
                        {
                            //ignored
                        }
                    }
                }

                if (inst != null)
                {
                    if (string.IsNullOrWhiteSpace(inst.PcName))
                    {
                        inst.PcName = Environment.MachineName;
                        context.SubmitChanges();
                    }
                    else if (inst.PcName != Environment.MachineName)
                    {
                        bRes = false;
                        MessageBox.Show("Данный ключ уже используется для другого компьютера!", "Error");
                    }
                }
                else
                {
                    bRes = false;
                    MessageBox.Show("Данный ключ не найден!", "Error");
                }

            }
            IsRegistered = bRes;
            return bRes;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (CheckIsRegistered())
            {
                this.Close();
            }
        }
    }
}
