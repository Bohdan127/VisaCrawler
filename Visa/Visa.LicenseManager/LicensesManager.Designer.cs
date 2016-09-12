namespace Visa.LicenseManager
{
    partial class LicensesManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicensesManager));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonLicense = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCustomer = new DevExpress.XtraBars.BarButtonItem();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDeleteAll = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemCustomer = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEditCustomers = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.customersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.visaLicensesDataSet = new Visa.LicenseManager.VisaLicensesDataSet();
            this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupMain = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageEdit = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupOperations = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageSkinGallery = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSkins = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.customersTableAdapter = new Visa.LicenseManager.VisaLicensesDataSetTableAdapters.CustomersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonLicense,
            this.barButtonCustomer,
            this.skinRibbonGalleryBarItem1,
            this.barButtonItemSave,
            this.barButtonItemCancel,
            this.barButtonItemRefresh,
            this.barButtonDelete,
            this.barButtonAddNew,
            this.barButtonDeleteAll,
            this.barButtonCopy,
            this.barEditItemCustomer});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 13;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain,
            this.ribbonPageEdit,
            this.ribbonPageSkinGallery});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditCustomers});
            this.ribbon.Size = new System.Drawing.Size(768, 143);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonLicense
            // 
            this.barButtonLicense.Caption = "barButtonItem1";
            this.barButtonLicense.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonLicense.Glyph")));
            this.barButtonLicense.Id = 1;
            this.barButtonLicense.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonLicense.LargeGlyph")));
            this.barButtonLicense.Name = "barButtonLicense";
            this.barButtonLicense.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonLicense_ItemClick);
            // 
            // barButtonCustomer
            // 
            this.barButtonCustomer.Caption = "barButtonItem1";
            this.barButtonCustomer.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonCustomer.Glyph")));
            this.barButtonCustomer.Id = 2;
            this.barButtonCustomer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonCustomer.LargeGlyph")));
            this.barButtonCustomer.Name = "barButtonCustomer";
            this.barButtonCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCustomer_ItemClick);
            // 
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 3;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // barButtonItemSave
            // 
            this.barButtonItemSave.Caption = "barButtonItem1";
            this.barButtonItemSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.Glyph")));
            this.barButtonItemSave.Id = 4;
            this.barButtonItemSave.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.LargeGlyph")));
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
            // 
            // barButtonItemCancel
            // 
            this.barButtonItemCancel.Caption = "barButtonItem1";
            this.barButtonItemCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.Glyph")));
            this.barButtonItemCancel.Id = 5;
            this.barButtonItemCancel.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.LargeGlyph")));
            this.barButtonItemCancel.Name = "barButtonItemCancel";
            this.barButtonItemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancel_ItemClick);
            // 
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Caption = "barButtonItem1";
            this.barButtonItemRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.Glyph")));
            this.barButtonItemRefresh.Id = 6;
            this.barButtonItemRefresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.LargeGlyph")));
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // barButtonDelete
            // 
            this.barButtonDelete.Caption = "barButtonItem1";
            this.barButtonDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonDelete.Glyph")));
            this.barButtonDelete.Id = 7;
            this.barButtonDelete.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonDelete.LargeGlyph")));
            this.barButtonDelete.Name = "barButtonDelete";
            this.barButtonDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDelete_ItemClick);
            // 
            // barButtonAddNew
            // 
            this.barButtonAddNew.Caption = "barButtonItem1";
            this.barButtonAddNew.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonAddNew.Glyph")));
            this.barButtonAddNew.Id = 9;
            this.barButtonAddNew.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonAddNew.LargeGlyph")));
            this.barButtonAddNew.Name = "barButtonAddNew";
            this.barButtonAddNew.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.barButtonAddNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAddNew_ItemClick);
            // 
            // barButtonDeleteAll
            // 
            this.barButtonDeleteAll.Caption = "barButtonItem1";
            this.barButtonDeleteAll.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonDeleteAll.Glyph")));
            this.barButtonDeleteAll.Id = 10;
            this.barButtonDeleteAll.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonDeleteAll.LargeGlyph")));
            this.barButtonDeleteAll.Name = "barButtonDeleteAll";
            this.barButtonDeleteAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDeleteAll_ItemClick);
            // 
            // barButtonCopy
            // 
            this.barButtonCopy.Caption = "barButtonItem1";
            this.barButtonCopy.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonCopy.Glyph")));
            this.barButtonCopy.Id = 11;
            this.barButtonCopy.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonCopy.LargeGlyph")));
            this.barButtonCopy.Name = "barButtonCopy";
            this.barButtonCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCopy_ItemClick);
            // 
            // barEditItemCustomer
            // 
            this.barEditItemCustomer.Edit = this.repositoryItemLookUpEditCustomers;
            this.barEditItemCustomer.Glyph = ((System.Drawing.Image)(resources.GetObject("barEditItemCustomer.Glyph")));
            this.barEditItemCustomer.Id = 12;
            this.barEditItemCustomer.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barEditItemCustomer.LargeGlyph")));
            this.barEditItemCustomer.Name = "barEditItemCustomer";
            this.barEditItemCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // repositoryItemLookUpEditCustomers
            // 
            this.repositoryItemLookUpEditCustomers.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemLookUpEditCustomers.AutoHeight = false;
            this.repositoryItemLookUpEditCustomers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditCustomers.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEditCustomers.DataSource = this.customersBindingSource;
            this.repositoryItemLookUpEditCustomers.DisplayMember = "Name";
            this.repositoryItemLookUpEditCustomers.Name = "repositoryItemLookUpEditCustomers";
            this.repositoryItemLookUpEditCustomers.ValueMember = "ID";
            // 
            // customersBindingSource
            // 
            this.customersBindingSource.DataMember = "Customers";
            this.customersBindingSource.DataSource = this.visaLicensesDataSet;
            // 
            // visaLicensesDataSet
            // 
            this.visaLicensesDataSet.DataSetName = "VisaLicensesDataSet";
            this.visaLicensesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ribbonPageMain
            // 
            this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupMain});
            this.ribbonPageMain.Name = "ribbonPageMain";
            this.ribbonPageMain.Text = "ribbonPage1";
            // 
            // ribbonPageGroupMain
            // 
            this.ribbonPageGroupMain.Glyph = ((System.Drawing.Image)(resources.GetObject("ribbonPageGroupMain.Glyph")));
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonLicense);
            this.ribbonPageGroupMain.ItemLinks.Add(this.barButtonCustomer);
            this.ribbonPageGroupMain.Name = "ribbonPageGroupMain";
            this.ribbonPageGroupMain.Text = "ribbonPageGroup1";
            // 
            // ribbonPageEdit
            // 
            this.ribbonPageEdit.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupEdit,
            this.ribbonPageGroupOperations});
            this.ribbonPageEdit.Name = "ribbonPageEdit";
            this.ribbonPageEdit.Text = "ribbonPageEdit";
            // 
            // ribbonPageGroupEdit
            // 
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemSave);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemCancel);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
            this.ribbonPageGroupEdit.Text = "ribbonPageGroup1";
            // 
            // ribbonPageGroupOperations
            // 
            this.ribbonPageGroupOperations.ItemLinks.Add(this.barButtonAddNew);
            this.ribbonPageGroupOperations.ItemLinks.Add(this.barEditItemCustomer);
            this.ribbonPageGroupOperations.ItemLinks.Add(this.barButtonDelete);
            this.ribbonPageGroupOperations.ItemLinks.Add(this.barButtonDeleteAll);
            this.ribbonPageGroupOperations.ItemLinks.Add(this.barButtonCopy);
            this.ribbonPageGroupOperations.Name = "ribbonPageGroupOperations";
            this.ribbonPageGroupOperations.Text = "ribbonPageGroup1";
            // 
            // ribbonPageSkinGallery
            // 
            this.ribbonPageSkinGallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSkins});
            this.ribbonPageSkinGallery.Name = "ribbonPageSkinGallery";
            this.ribbonPageSkinGallery.Text = "ribbonPage1";
            // 
            // ribbonPageGroupSkins
            // 
            this.ribbonPageGroupSkins.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
            this.ribbonPageGroupSkins.Name = "ribbonPageGroupSkins";
            this.ribbonPageGroupSkins.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 451);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(768, 31);
            // 
            // customersTableAdapter
            // 
            this.customersTableAdapter.ClearBeforeFill = true;
            // 
            // LicensesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 482);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "LicensesManager";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "LicenseManager";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupMain;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonLicense;
        private DevExpress.XtraBars.BarButtonItem barButtonCustomer;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSkinGallery;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSkins;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOperations;
        private DevExpress.XtraBars.BarButtonItem barButtonDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonAddNew;
        private DevExpress.XtraBars.BarButtonItem barButtonDeleteAll;
        private DevExpress.XtraBars.BarButtonItem barButtonCopy;
        private DevExpress.XtraBars.BarEditItem barEditItemCustomer;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditCustomers;
        private VisaLicensesDataSet visaLicensesDataSet;
        private System.Windows.Forms.BindingSource customersBindingSource;
        private VisaLicensesDataSetTableAdapters.CustomersTableAdapter customersTableAdapter;
    }
}