namespace Visa.LicenseManager
{
    partial class LicensesForm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.licensesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.visaLicensesDataSet = new Visa.LicenseManager.VisaLicensesDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditReadOnly = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.licensesTableAdapter = new Visa.LicenseManager.VisaLicensesDataSetTableAdapters.LicensesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditReadOnly)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.licensesBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEditReadOnly});
            this.gridControl1.Size = new System.Drawing.Size(284, 261);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // licensesBindingSource
            // 
            this.licensesBindingSource.DataMember = "Licenses";
            this.licensesBindingSource.DataSource = this.visaLicensesDataSet;
            // 
            // visaLicensesDataSet
            // 
            this.visaLicensesDataSet.DataSetName = "VisaLicensesDataSet";
            this.visaLicensesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuid,
            this.colPcName,
            this.colCustomerID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colGuid
            // 
            this.colGuid.ColumnEdit = this.repositoryItemTextEditReadOnly;
            this.colGuid.FieldName = "Guid";
            this.colGuid.Name = "colGuid";
            this.colGuid.Visible = true;
            this.colGuid.VisibleIndex = 0;
            // 
            // repositoryItemTextEditReadOnly
            // 
            this.repositoryItemTextEditReadOnly.AutoHeight = false;
            this.repositoryItemTextEditReadOnly.Name = "repositoryItemTextEditReadOnly";
            this.repositoryItemTextEditReadOnly.ReadOnly = true;
            // 
            // colPcName
            // 
            this.colPcName.ColumnEdit = this.repositoryItemTextEditReadOnly;
            this.colPcName.FieldName = "PcName";
            this.colPcName.Name = "colPcName";
            this.colPcName.Visible = true;
            this.colPcName.VisibleIndex = 1;
            // 
            // colCustomerID
            // 
            this.colCustomerID.ColumnEdit = this.repositoryItemTextEditReadOnly;
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.Visible = true;
            this.colCustomerID.VisibleIndex = 2;
            // 
            // licensesTableAdapter
            // 
            this.licensesTableAdapter.ClearBeforeFill = true;
            // 
            // LicensesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.gridControl1);
            this.Name = "LicensesForm";
            this.Text = "LicensesForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visaLicensesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditReadOnly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private VisaLicensesDataSet visaLicensesDataSet;
        private System.Windows.Forms.BindingSource licensesBindingSource;
        private VisaLicensesDataSetTableAdapters.LicensesTableAdapter licensesTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colGuid;
        private DevExpress.XtraGrid.Columns.GridColumn colPcName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditReadOnly;
    }
}