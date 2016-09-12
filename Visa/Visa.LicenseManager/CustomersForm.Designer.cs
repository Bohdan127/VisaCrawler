namespace Visa.LicenseManager
{
    partial class CustomersForm
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
            this.gridControlMain = new DevExpress.XtraGrid.GridControl();
            this.visaLicensesDataSet = new Visa.LicenseManager.VisaLicensesDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMainGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.customersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customersTableAdapter = new Visa.LicenseManager.VisaLicensesDataSetTableAdapters.CustomersTableAdapter();
            this.repositoryItemTextEditId = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEditName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMainGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditName)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlMain
            // 
            this.gridControlMain.DataMember = "Customers";
            this.gridControlMain.DataSource = this.visaLicensesDataSet;
            this.gridControlMain.Location = new System.Drawing.Point(12, 12);
            this.gridControlMain.MainView = this.gridView1;
            this.gridControlMain.Name = "gridControlMain";
            this.gridControlMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEditId,
            this.repositoryItemTextEditName});
            this.gridControlMain.Size = new System.Drawing.Size(570, 355);
            this.gridControlMain.TabIndex = 4;
            this.gridControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // visaLicensesDataSet
            // 
            this.visaLicensesDataSet.DataSetName = "VisaLicensesDataSet";
            this.visaLicensesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName});
            this.gridView1.GridControl = this.gridControlMain;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            // 
            // colID
            // 
            this.colID.ColumnEdit = this.repositoryItemTextEditId;
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            // 
            // colName
            // 
            this.colName.ColumnEdit = this.repositoryItemTextEditName;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.gridControlMain);
            this.dataLayoutControl1.DataMember = "Customers";
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(594, 379);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMainGrid});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(594, 379);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemMainGrid
            // 
            this.layoutControlItemMainGrid.Control = this.gridControlMain;
            this.layoutControlItemMainGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMainGrid.Name = "layoutControlItemMainGrid";
            this.layoutControlItemMainGrid.Size = new System.Drawing.Size(574, 359);
            this.layoutControlItemMainGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMainGrid.TextVisible = false;
            // 
            // customersBindingSource
            // 
            this.customersBindingSource.DataMember = "Customers";
            this.customersBindingSource.DataSource = this.visaLicensesDataSet;
            // 
            // customersTableAdapter
            // 
            this.customersTableAdapter.ClearBeforeFill = true;
            // 
            // repositoryItemTextEditId
            // 
            this.repositoryItemTextEditId.AutoHeight = false;
            this.repositoryItemTextEditId.Name = "repositoryItemTextEditId";
            this.repositoryItemTextEditId.ReadOnly = true;
            // 
            // repositoryItemTextEditName
            // 
            this.repositoryItemTextEditName.AutoHeight = false;
            this.repositoryItemTextEditName.MaxLength = 50;
            this.repositoryItemTextEditName.Name = "repositoryItemTextEditName";
            // 
            // CustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 379);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "CustomersForm";
            this.Text = "CustomersForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMainGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private VisaLicensesDataSet visaLicensesDataSet;
        private System.Windows.Forms.BindingSource customersBindingSource;
        private VisaLicensesDataSetTableAdapters.CustomersTableAdapter customersTableAdapter;
        private DevExpress.XtraGrid.GridControl gridControlMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditId;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditName;
    }
}