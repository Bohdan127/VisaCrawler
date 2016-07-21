using NLog;
using System.Linq;
using System.Windows.Forms;
using ToolsPortable;
using Visa.BusinessLogic.Managers;
using Visa.BusinessLogic.SVN_Model;
using Visa.Database;
using Visa.Database.Enums;
using Visa.Resources;

namespace Visa.WinForms.Views
{
    public partial class SetupForm : Form
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SetupForm()
        {
            _logger.Trace("Start Setup Form CTOR");
            InitializeComponent();
            Load += SetupForm_Load;
            _logger.Trace("End Setup Form CTOR");
        }

        private void SetupForm_Load(object sender, System.EventArgs e)
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
            _logger.Trace("End MapData");
        }

        private void InitNames()
        {
            _logger.Trace("Start InitNames");
            Text = ResManager.GetString(ResKeys.BarButtonItemSetup_Caption);

            layoutControlItemPassword.Text = ResManager.GetString(ResKeys.Password_Text);
            layoutControlItemNationality.Text = ResManager.GetString(ResKeys.Nationality_Text);
            layoutControlItemRepeatIfCrash.Text = ResManager.GetString(ResKeys.RepeatIfCrash_Text);
            layoutControlItemCloseBrower.Text = ResManager.GetString(ResKeys.lblCloseBrowser);

            lookUpEditNationality.Properties.NullText = ResManager.GetString(ResKeys.Nationality_NullText);

            toggleSwitchCloseBrowser.Properties.OnText = ResManager.GetString(ResKeys.ToggleSwitch_OnText);
            toggleSwitchCloseBrowser.Properties.OffText = ResManager.GetString(ResKeys.ToggleSwitch_OffText);
            toggleSwitchRepeatIfCrash.Properties.OnText = ResManager.GetString(ResKeys.ToggleSwitch_OnText);
            toggleSwitchRepeatIfCrash.Properties.OffText = ResManager.GetString(ResKeys.ToggleSwitch_OffText);

            simpleButtonOk.Text = ResManager.GetString(ResKeys.ButtonOk_Text);
            simpleButtonCancel.Text = ResManager.GetString(ResKeys.ButtonCancel_Text);

            lookUpEditNationality.Properties.NullText = ResManager.GetString(ResKeys.Nationality_NullText);
            _logger.Trace("End InitNames");
        }

        private void SetDataSourceForLookUps()
        {
            _logger.Trace("Start SetDataSourceForLookUps");
            lookUpEditNationality.Properties.DataSource =
                InstanceProvider.DataSet.Choice.Where(c => c.Type == (short)ChoicesType.Country).ToList();
            _logger.Trace("End SetDataSourceForLookUps");
        }

        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            _logger.Trace("Start simpleButton1_Click");
            var options = new SetupOptions
            {
                CloseBrowser = toggleSwitchCloseBrowser.IsOn,
                RepeatIfCrash = toggleSwitchRepeatIfCrash.IsOn,
                Nationality = lookUpEditNationality.EditValue.ConvertToStringOrNull(),
                Password = textEditPassword.Text
            };
            SetupManager.SaveOptions(options);
            Close();
            _logger.Trace("End simpleButton1_Click");
        }

        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            _logger.Trace("Start simpleButton2_Click");
            Close();
            _logger.Trace("End simpleButton2_Click");
        }
    }
}
