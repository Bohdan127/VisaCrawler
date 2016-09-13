using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Visa.LicenseManager.Properties;
using Visa.Resources;
using Visa.Resources.uk_UA;

namespace Visa.LicenseManager
{
    public partial class LicensesManager : RibbonForm
    {
        private CustomersForm _customersForm;
        private LicensesForm _licensesForm;

        public LicensesManager()
        {
            InitializeComponent();
            InitSkinGallery();
            SetLabels();
            UserLookAndFeel.Default.SkinName =
                Settings.Default["ApplicationSkinName"].ToString();
            Closing += LicenseManager_FormClosing;
            IsMdiContainer = true;
            ribbonPageEdit.Visible = false;
            Icon = Properties.Resources
                .key_icon;
        }

        private void SetLabels()
        {
            ResManager.RegisterResource("uk_UA",
                uk_UA.ResourceManager);
            ribbonPageMain.Text = ResManager.GetString(ResKeys.MainPage_Title);
            ribbonPageEdit.Text = ResManager.GetString(ResKeys.EditPage_Title);
            ribbonPageSkinGallery.Text =
                ResManager.GetString(ResKeys.SkinPage_Title);
            ribbonPageGroupSkins.Text =
                ResManager.GetString(ResKeys.SkinGroup_Title);
            ribbonPageGroupMain.Text =
                ResManager.GetString(ResKeys.MainGroup_Title);
            barButtonLicense.Caption =
                ResManager.GetString(ResKeys.LicensesBarButton_Caption);
            barButtonCustomer.Caption =
                ResManager.GetString(ResKeys.CustomersBarButton_Caption);
            ribbonPageGroupEdit.Text =
                ResManager.GetString(ResKeys.EditGroup_title);
            barButtonItemSave.Caption =
                ResManager.GetString(ResKeys.SaveBarButton_Caption);
            barButtonItemCancel.Caption =
                ResManager.GetString(ResKeys.CancelBarButton_Caption);
            barButtonItemRefresh.Caption =
                ResManager.GetString(ResKeys.RefreshBarButton_Caption);
            ribbonPageGroupOperations.Text =
                ResManager.GetString(ResKeys.OperationsGroup_Title);
            barButtonAddNew.Caption =
                ResManager.GetString(ResKeys.AddNewBarButton_Caption);
            barEditItemCustomer.Caption =
                ResManager.GetString(ResKeys.CustomerBarEdit_Caption);
            barButtonDelete.Caption =
                ResManager.GetString(ResKeys.DeleteBarButton_Caption);
            barButtonDeleteAll.Caption =
                ResManager.GetString(ResKeys.DeleteAllBarButton_Caption);
            barButtonCopy.Caption =
                ResManager.GetString(ResKeys.CopyBarButton_Caption);
        }

        private void LicenseManager_FormClosing(object sender,
            CancelEventArgs e)
        {
            Settings.Default["ApplicationSkinName"] =
                UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();
            CloseAll();
        }

        /// <summary>
        /// Close all child forms and main MDI form
        /// </summary>
        private void CloseAll()
        {
            _customersForm?.Close();
            _licensesForm?.Close();
            //yes, we should close the main form during closing the main form
            Close();
        }

        private void HideAll()
        {
            _customersForm?.Hide();
            _licensesForm?.Hide();
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1,
                true);
        }

        #region RibbonMainPage

        private void barButtonLicense_ItemClick(object sender,
            ItemClickEventArgs e)
        {
            HideAll();
            GetLicensesForm()
                .Show();
        }

        private void barButtonCustomer_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            HideAll();
            GetCustomersForm()
                .Show();
        }

        #endregion RibbonMainPage

        #region RibbonEditPage

        private void barButtonItemSave_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(CustomersForm):
                    GetCustomersForm()
                        .Save();
                    break;
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .Save();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void barButtonItemCancel_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(CustomersForm):
                    GetCustomersForm()
                        .Cancel();
                    break;
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .Cancel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void barButtonItemRefresh_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(CustomersForm):
                    GetCustomersForm()
                        .RefreshForm();
                    break;
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .RefreshForm();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void barButtonAddNew_ItemClick(object sender,
            ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(LicensesForm):
                    if (barEditItemCustomer.EditValue == null)
                    {
                        XtraMessageBox.Show(
                            ResManager.GetString(
                                ResKeys.ValidationError_Message_Customer),
                            ResManager.GetString(ResKeys.ValidationError_Title),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    GetLicensesForm()
                        .AddNew(
                            Convert.ToInt32(
                                barEditItemCustomer.EditValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void barButtonDeleteAll_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .DeleteAll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void barButtonCopy_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .CopyKey();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void barButtonDelete_ItemClick(object sender,
             ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
                throw new NotImplementedException();
            switch (ActiveMdiChild.Name)
            {
                case nameof(CustomersForm):
                    GetCustomersForm()
                        .Delete();
                    break;
                case nameof(LicensesForm):
                    GetLicensesForm()
                        .Delete();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion RibbonEditPage

        #region ChildForms

        public CustomersForm GetCustomersForm()
        {
            // ReSharper disable once InvertIf
            if (_customersForm == null)
            {
                _customersForm = new CustomersForm
                {
                    MdiParent = this,
                    ToClose = false,
                    WindowState = FormWindowState.Maximized
                };
                _customersForm.Closing += ChildForm_Closing;
                _customersForm.Activated += ChildForm_Activated;
            }
            return _customersForm;
        }

        public LicensesForm GetLicensesForm()
        {
            // ReSharper disable once InvertIf
            if (_licensesForm == null)
            {
                _licensesForm = new LicensesForm
                {
                    MdiParent = this,
                    ToClose = false,
                    WindowState = FormWindowState.Maximized
                };
                _licensesForm.Closing += ChildForm_Closing;
                _licensesForm.Activated += ChildForm_Activated;
            }
            return _licensesForm;
        }

        private void ChildForm_Activated(object sender, EventArgs e)
        {
            var form = sender as Form;
            if (form == null)
                throw new NotImplementedException();


            switch (form.Name)
            {
                case nameof(CustomersForm):
                    ShowCustomersMenu();
                    break;
                case nameof(LicensesForm):
                    ShowLicensesMenu();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChildForm_Closing(object sender,
            CancelEventArgs e)
        {
            ribbonPageEdit.Visible = false;
            ribbon.SelectedPage = ribbonPageMain;
        }

        private void ShowCustomersMenu()
        {
            ribbonPageEdit.Visible = true;
            ribbon.SelectedPage = ribbonPageEdit;
            barButtonDelete.Visibility = BarItemVisibility.Always;
            barButtonItemCancel.Visibility = BarItemVisibility.Always;
            barButtonItemRefresh.Visibility = BarItemVisibility.Always;
            barButtonItemSave.Visibility = BarItemVisibility.Always;
            barButtonAddNew.Visibility = BarItemVisibility.Never;
            barButtonCopy.Visibility = BarItemVisibility.Never;
            barButtonDeleteAll.Visibility = BarItemVisibility.Never;
            barEditItemCustomer.Visibility = BarItemVisibility.Never;
        }

        private void ShowLicensesMenu()
        {
            customersTableAdapter.Fill(visaLicensesDataSet.Customers);
            ribbonPageEdit.Visible = true;
            ribbon.SelectedPage = ribbonPageEdit;
            barButtonDelete.Visibility = BarItemVisibility.Always;
            barButtonItemCancel.Visibility = BarItemVisibility.Always;
            barButtonItemRefresh.Visibility = BarItemVisibility.Always;
            barButtonItemSave.Visibility = BarItemVisibility.Always;
            barButtonAddNew.Visibility = BarItemVisibility.Always;
            barButtonCopy.Visibility = BarItemVisibility.Always;
            barButtonDeleteAll.Visibility = BarItemVisibility.Always;
            barEditItemCustomer.Visibility = BarItemVisibility.Always;
        }

        #endregion ChildForms
    }
}