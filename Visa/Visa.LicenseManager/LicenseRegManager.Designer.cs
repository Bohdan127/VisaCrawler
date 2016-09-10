using Visa.License.DB;

namespace Visa.LicenseManager
{
    partial class LicenseRegManager
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.instancesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.licenseDBDataSet = new Visa.License.DB.LicenseDBDataSet();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textEditKey = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textEditPcName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butUpdateNow = new DevExpress.XtraEditors.SimpleButton();
            this.butCopyKey = new DevExpress.XtraEditors.SimpleButton();
            this.lbCopy = new System.Windows.Forms.Label();
            this.butAllKeys = new DevExpress.XtraEditors.SimpleButton();
            this.lbAdd = new System.Windows.Forms.Label();
            this.lbDel = new System.Windows.Forms.Label();
            this.butAdd = new DevExpress.XtraEditors.SimpleButton();
            this.butAll = new DevExpress.XtraEditors.SimpleButton();
            this.butPcName = new DevExpress.XtraEditors.SimpleButton();
            this.instancesTableAdapter = new Visa.License.DB.LicenseDBDataSetTableAdapters.InstancesTableAdapter();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instancesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.licenseDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPcName)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(721, 319);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 304);
            this.panel1.TabIndex = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.instancesBindingSource;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.textEditKey,
            this.textEditPcName});
            this.gridControl2.Size = new System.Drawing.Size(542, 304);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // instancesBindingSource
            // 
            this.instancesBindingSource.DataMember = "Instances";
            this.instancesBindingSource.DataSource = this.licenseDBDataSet;
            // 
            // licenseDBDataSet
            // 
            this.licenseDBDataSet.DataSetName = "LicenseDBDataSet";
            this.licenseDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuid,
            this.colPcName,
            this.colCustomerID});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colGuid
            // 
            this.colGuid.Caption = "Лиценщионый ключ";
            this.colGuid.ColumnEdit = this.textEditKey;
            this.colGuid.FieldName = "Guid";
            this.colGuid.Name = "colGuid";
            this.colGuid.OptionsColumn.AllowEdit = false;
            this.colGuid.OptionsColumn.AllowMove = false;
            this.colGuid.OptionsColumn.AllowShowHide = false;
            this.colGuid.OptionsColumn.ReadOnly = true;
            this.colGuid.OptionsColumn.ShowInCustomizationForm = false;
            this.colGuid.OptionsColumn.ShowInExpressionEditor = false;
            this.colGuid.Visible = true;
            this.colGuid.VisibleIndex = 0;
            this.colGuid.Width = 231;
            // 
            // textEditKey
            // 
            this.textEditKey.AutoHeight = false;
            this.textEditKey.Name = "textEditKey";
            // 
            // colPcName
            // 
            this.colPcName.Caption = "Имя Компьютера";
            this.colPcName.ColumnEdit = this.textEditPcName;
            this.colPcName.FieldName = "PcName";
            this.colPcName.Name = "colPcName";
            this.colPcName.OptionsColumn.AllowEdit = false;
            this.colPcName.OptionsColumn.AllowMove = false;
            this.colPcName.OptionsColumn.AllowShowHide = false;
            this.colPcName.OptionsColumn.ReadOnly = true;
            this.colPcName.OptionsColumn.ShowInCustomizationForm = false;
            this.colPcName.OptionsColumn.ShowInExpressionEditor = false;
            this.colPcName.Visible = true;
            this.colPcName.VisibleIndex = 1;
            this.colPcName.Width = 195;
            // 
            // textEditPcName
            // 
            this.textEditPcName.AutoHeight = false;
            this.textEditPcName.Name = "textEditPcName";
            // 
            // colCustomerID
            // 
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.OptionsColumn.AllowMove = false;
            this.colCustomerID.OptionsColumn.AllowShowHide = false;
            this.colCustomerID.OptionsColumn.ShowInCustomizationForm = false;
            this.colCustomerID.OptionsColumn.ShowInExpressionEditor = false;
            this.colCustomerID.Visible = true;
            this.colCustomerID.VisibleIndex = 2;
            this.colCustomerID.Width = 98;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.butUpdateNow);
            this.panel2.Controls.Add(this.butCopyKey);
            this.panel2.Controls.Add(this.lbCopy);
            this.panel2.Controls.Add(this.butAllKeys);
            this.panel2.Controls.Add(this.lbAdd);
            this.panel2.Controls.Add(this.lbDel);
            this.panel2.Controls.Add(this.butAdd);
            this.panel2.Controls.Add(this.butAll);
            this.panel2.Controls.Add(this.butPcName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(551, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 304);
            this.panel2.TabIndex = 1;
            // 
            // butUpdateNow
            // 
            this.butUpdateNow.Location = new System.Drawing.Point(0, 259);
            this.butUpdateNow.Name = "butUpdateNow";
            this.butUpdateNow.Size = new System.Drawing.Size(165, 45);
            this.butUpdateNow.TabIndex = 9;
            this.butUpdateNow.Text = "Обновить";
            this.butUpdateNow.Click += new System.EventHandler(this.butUpdateNow_Click);
            // 
            // butCopyKey
            // 
            this.butCopyKey.Location = new System.Drawing.Point(83, 208);
            this.butCopyKey.Name = "butCopyKey";
            this.butCopyKey.Size = new System.Drawing.Size(75, 23);
            this.butCopyKey.TabIndex = 7;
            this.butCopyKey.Text = "Ключ";
            this.butCopyKey.Click += new System.EventHandler(this.butCopyKey_Click);
            // 
            // lbCopy
            // 
            this.lbCopy.AutoSize = true;
            this.lbCopy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCopy.Location = new System.Drawing.Point(3, 183);
            this.lbCopy.Name = "lbCopy";
            this.lbCopy.Size = new System.Drawing.Size(114, 19);
            this.lbCopy.TabIndex = 6;
            this.lbCopy.Text = "Копировать:";
            // 
            // butAllKeys
            // 
            this.butAllKeys.Location = new System.Drawing.Point(83, 107);
            this.butAllKeys.Name = "butAllKeys";
            this.butAllKeys.Size = new System.Drawing.Size(75, 23);
            this.butAllKeys.TabIndex = 5;
            this.butAllKeys.Text = "Все ключи";
            this.butAllKeys.Click += new System.EventHandler(this.butAllKeys_Click);
            // 
            // lbAdd
            // 
            this.lbAdd.AutoSize = true;
            this.lbAdd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdd.Location = new System.Drawing.Point(3, 134);
            this.lbAdd.Name = "lbAdd";
            this.lbAdd.Size = new System.Drawing.Size(95, 19);
            this.lbAdd.TabIndex = 4;
            this.lbAdd.Text = "Добавить:";
            // 
            // lbDel
            // 
            this.lbDel.AutoSize = true;
            this.lbDel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDel.Location = new System.Drawing.Point(3, 27);
            this.lbDel.Name = "lbDel";
            this.lbDel.Size = new System.Drawing.Size(85, 19);
            this.lbDel.TabIndex = 3;
            this.lbDel.Text = "Удалить:";
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(83, 157);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 23);
            this.butAdd.TabIndex = 2;
            this.butAdd.Text = "Новый";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // butAll
            // 
            this.butAll.Location = new System.Drawing.Point(83, 78);
            this.butAll.Name = "butAll";
            this.butAll.Size = new System.Drawing.Size(75, 23);
            this.butAll.TabIndex = 1;
            this.butAll.Text = "Ключ";
            this.butAll.Click += new System.EventHandler(this.butAll_Click);
            // 
            // butPcName
            // 
            this.butPcName.Location = new System.Drawing.Point(83, 49);
            this.butPcName.Name = "butPcName";
            this.butPcName.Size = new System.Drawing.Size(75, 23);
            this.butPcName.TabIndex = 0;
            this.butPcName.Text = "Компьютер";
            this.butPcName.Click += new System.EventHandler(this.butPcName_Click);
            // 
            // instancesTableAdapter
            // 
            this.instancesTableAdapter.ClearBeforeFill = true;
            // 
            // LicenseRegManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 319);
            this.Controls.Add(this.flowLayoutPanel1);
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.MaximizeBox = false;
            this.Name = "LicenseRegManager";
            this.Text = "LicenseRegManager для ValeoBet";
            this.Load += new System.EventHandler(this.LicenseRegManager_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instancesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.licenseDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPcName)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEditKey;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEditPcName;
        private System.Windows.Forms.Panel panel2;
        private LicenseDBDataSet licenseDBDataSet;
        private System.Windows.Forms.BindingSource instancesBindingSource;
        private global::Visa.License.DB.LicenseDBDataSetTableAdapters.InstancesTableAdapter instancesTableAdapter;
        private DevExpress.XtraEditors.SimpleButton butAdd;
        private DevExpress.XtraEditors.SimpleButton butAll;
        private DevExpress.XtraEditors.SimpleButton butPcName;
        private System.Windows.Forms.Label lbAdd;
        private System.Windows.Forms.Label lbDel;
        private DevExpress.XtraEditors.SimpleButton butAllKeys;
        private DevExpress.XtraEditors.SimpleButton butCopyKey;
        private System.Windows.Forms.Label lbCopy;
        private DevExpress.XtraEditors.SimpleButton butUpdateNow;
        private DevExpress.XtraGrid.Columns.GridColumn colGuid;
        private DevExpress.XtraGrid.Columns.GridColumn colPcName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
    }
}

