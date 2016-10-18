using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GlobalResources;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.Database.Enums;

namespace Visa.WinForms.Views
{
    public partial class SetupForm : Form
    {
        private static OpenFileDialog openFileDialog;
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        public SetupForm()
        {
            _logger.Trace("Start Setup Form CTOR");
            InitializeComponent();
            Load += SetupForm_Load;
            _logger.Trace("End Setup Form CTOR");
        }

        private void SetupForm_Load(object sender,
            EventArgs e)
        {
            _logger.Trace("Start SetupForm_Load");
            InitNames();
            SetDataSourceForLookUps();
            MapData();
            _logger.Trace("End SetupForm_Load");
        }

        private void MapData()
        {
            _logger.Trace("Start MapData");
            var options = SetupManager.GetOptions();
            textEditPassword.Text = options.Password;
            lookUpEditNationality.EditValue = options.Nationality;
            toggleSwitchCloseBrowser.IsOn = options.CloseBrowser;
            toggleSwitchRepeatIfCrash.IsOn = options.RepeatIfCrash;
            spinEditPeopleCount.EditValue = options.PeopleCount;
            spinEditChildCount.EditValue = options.ChildCount;
            textEditUrl.EditValue = options.AvailabilityUrl;
            textEditEmail.EditValue = options.Email;
            listBoxControlProxies.Items.Clear();
            listBoxControlProxies.Items.AddRange(options.Proxies);
            listBoxControlProxies.SelectAll();
            textEditRuCaptchaID.Text = options.RuCaptchaID;
            _logger.Trace("End MapData");
        }

        private void InitNames()
        {
            _logger.Trace("Start InitNames");
            Text = ResManager.GetString(ResKeys.BarButtonItemSetup_Caption);

            layoutControlItemPeopleCount.Text =
                ResManager.GetString(ResKeys.colPeopleCount);
            layoutControlItemChildCount.Text =
                ResManager.GetString(ResKeys.colChildsCount);
            layoutControlItemPassword.Text =
                ResManager.GetString(ResKeys.Password_Text);
            layoutControlItemNationality.Text =
                ResManager.GetString(ResKeys.Nationality_Text);
            layoutControlItemRepeatIfCrash.Text =
                ResManager.GetString(ResKeys.RepeatIfCrash_Text);
            layoutControlItemCloseBrower.Text =
                ResManager.GetString(ResKeys.lblCloseBrowser);
            layoutControlItemUrl.Text =
                ResManager.GetString(ResKeys.lblAvailabilityUrl);
            layoutControlItemEmail.Text =
                ResManager.GetString(ResKeys.lblEmail);
            layoutControlItemProxies.Text =
                ResManager.GetString(ResKeys.lblChooseProxies); ;

            lookUpEditNationality.Properties.NullText =
                ResManager.GetString(ResKeys.Nationality_NullText);

            toggleSwitchCloseBrowser.Properties.OnText =
                ResManager.GetString(ResKeys.ToggleSwitch_OnText);
            toggleSwitchCloseBrowser.Properties.OffText =
                ResManager.GetString(ResKeys.ToggleSwitch_OffText);
            toggleSwitchRepeatIfCrash.Properties.OnText =
                ResManager.GetString(ResKeys.ToggleSwitch_OnText);
            toggleSwitchRepeatIfCrash.Properties.OffText =
                ResManager.GetString(ResKeys.ToggleSwitch_OffText);

            simpleButtonLoadProxies.Text =
                ResManager.GetString(ResKeys.ButtonLoadProxies_Text);
            simpleButtonOk.Text = ResManager.GetString(ResKeys.ButtonOk_Text);
            simpleButtonCancel.Text =
                ResManager.GetString(ResKeys.ButtonCancel_Text);

            lookUpEditNationality.Properties.NullText =
                ResManager.GetString(ResKeys.Nationality_NullText);
            _logger.Trace("End InitNames");
        }

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            lookUpEditNationality.Properties.DataSource =
                InstanceProvider.DataSet.Choice.Where(
                    c => c.Type == (short) ChoicesType.Country).ToList();
            _logger.Trace("End SetDataSourceForLookUps");
        }

        private void simpleButton1_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start simpleButton1_Click");
            var options = new SetupOptions
            {
                CloseBrowser = toggleSwitchCloseBrowser.IsOn,
                RepeatIfCrash = toggleSwitchRepeatIfCrash.IsOn,
                Nationality =
                    lookUpEditNationality.EditValue.ConvertToStringOrNull(),
                Password = textEditPassword.Text,
                PeopleCount =
                    spinEditPeopleCount.EditValue.ConvertToStringOrNull(),
                ChildCount =
                    spinEditChildCount.EditValue.ConvertToStringOrNull(),
                AvailabilityUrl = textEditUrl.EditValue.ConvertToStringOrNull(),
                Email = textEditEmail.EditValue.ConvertToStringOrNull(),
                Proxies = ToStringList(listBoxControlProxies.SelectedItems),
                RuCaptchaID = textEditRuCaptchaID.EditValue.ConvertToStringOrNull()
            };
            SetupManager.SaveOptions(options);
            Close();
            _logger.Trace("End simpleButton1_Click");
        }

        private string[] ToStringList(BaseListBoxControl.SelectedItemCollection items)
        {
            string[] _out = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
                    _out[i] = items[i].ConvertToStringOrNull();
            return _out;
        }

        private void simpleButton2_Click(object sender,
            EventArgs e)
        {
            _logger.Trace("Start simpleButton2_Click");
            Close();
            _logger.Trace("End simpleButton2_Click");
        }

        private void LoadProxies_Click(object sender, EventArgs e)
        {
            _logger.Trace("Start LoadProxies_Click");
            openFileDialog = new OpenFileDialog
            {
                Title = ResManager.GetString(ResKeys.OpenFileDialog_Title),
                Filter = "Text document|*.txt|All files|*.*",
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logger.Info($"ImportProxies _openFileDialog.ShowDialog() == DialogResult.OK  _openFileDialog.FileName = {openFileDialog.FileName}");
                    string[] filelines = File.ReadAllLines(openFileDialog.FileName);
                    foreach(string s in filelines)
                        listBoxControlProxies.Items.Add(s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Can't read the file");
                }
            }
            _logger.Trace("End LoadProxies_Click");
        }
    }
}