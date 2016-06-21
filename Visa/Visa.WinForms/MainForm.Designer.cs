namespace Visa.WinForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.lookUpEditVisaCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditServiceCenter = new DevExpress.XtraEditors.LookUpEdit();
            this.buttonShow = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemServiceCenter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStartButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemVisaCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCheckingProgress = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupFirst = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemClientRegistry = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupSecond = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVisaCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditServiceCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServiceCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVisaCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckingProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClientRegistry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.gridControl1);
            this.dataLayoutControl1.Controls.Add(this.progressBarControl1);
            this.dataLayoutControl1.Controls.Add(this.lookUpEditVisaCategory);
            this.dataLayoutControl1.Controls.Add(this.lookUpEditServiceCenter);
            this.dataLayoutControl1.Controls.Add(this.buttonShow);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(955, 498);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(24, 90);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(175, 18);
            this.progressBarControl1.StyleController = this.dataLayoutControl1;
            this.progressBarControl1.TabIndex = 9;
            // 
            // lookUpEditVisaCategory
            // 
            this.lookUpEditVisaCategory.Location = new System.Drawing.Point(122, 66);
            this.lookUpEditVisaCategory.Name = "lookUpEditVisaCategory";
            this.lookUpEditVisaCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditVisaCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Категорія Візи")});
            this.lookUpEditVisaCategory.Properties.DisplayMember = "Name";
            this.lookUpEditVisaCategory.Properties.NullText = "-Оберіть візову категорію-";
            this.lookUpEditVisaCategory.Properties.ValueMember = "Value";
            this.lookUpEditVisaCategory.Size = new System.Drawing.Size(217, 20);
            this.lookUpEditVisaCategory.StyleController = this.dataLayoutControl1;
            this.lookUpEditVisaCategory.TabIndex = 6;
            // 
            // lookUpEditServiceCenter
            // 
            this.lookUpEditServiceCenter.Location = new System.Drawing.Point(122, 42);
            this.lookUpEditServiceCenter.Name = "lookUpEditServiceCenter";
            this.lookUpEditServiceCenter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditServiceCenter.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Візовий Сервіс Центр")});
            this.lookUpEditServiceCenter.Properties.DisplayMember = "Name";
            this.lookUpEditServiceCenter.Properties.NullText = "-Оберіть ППВА-";
            this.lookUpEditServiceCenter.Properties.ValueMember = "Value";
            this.lookUpEditServiceCenter.Size = new System.Drawing.Size(217, 20);
            this.lookUpEditServiceCenter.StyleController = this.dataLayoutControl1;
            this.lookUpEditServiceCenter.TabIndex = 5;
            // 
            // buttonShow
            // 
            this.buttonShow.Image = ((System.Drawing.Image)(resources.GetObject("buttonShow.Image")));
            this.buttonShow.Location = new System.Drawing.Point(203, 90);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(136, 22);
            this.buttonShow.StyleController = this.dataLayoutControl1;
            this.buttonShow.TabIndex = 4;
            this.buttonShow.Text = "Запустити перевірку";
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupFirst,
            this.layoutControlGroupSecond,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(955, 498);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemServiceCenter
            // 
            this.layoutControlItemServiceCenter.Control = this.lookUpEditServiceCenter;
            this.layoutControlItemServiceCenter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemServiceCenter.Name = "layoutControlItemServiceCenter";
            this.layoutControlItemServiceCenter.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2);
            this.layoutControlItemServiceCenter.Size = new System.Drawing.Size(319, 24);
            this.layoutControlItemServiceCenter.Text = "Сервісний ценрт:";
            this.layoutControlItemServiceCenter.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItemStartButton
            // 
            this.layoutControlItemStartButton.Control = this.buttonShow;
            this.layoutControlItemStartButton.Location = new System.Drawing.Point(179, 48);
            this.layoutControlItemStartButton.Name = "layoutControlItemStartButton";
            this.layoutControlItemStartButton.Size = new System.Drawing.Size(140, 26);
            this.layoutControlItemStartButton.Text = "Запустити перевірку";
            this.layoutControlItemStartButton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemStartButton.TextVisible = false;
            // 
            // layoutControlItemVisaCategory
            // 
            this.layoutControlItemVisaCategory.Control = this.lookUpEditVisaCategory;
            this.layoutControlItemVisaCategory.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemVisaCategory.Name = "layoutControlItemVisaCategory";
            this.layoutControlItemVisaCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2);
            this.layoutControlItemVisaCategory.Size = new System.Drawing.Size(319, 24);
            this.layoutControlItemVisaCategory.Text = "Візова категорія:";
            this.layoutControlItemVisaCategory.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItemCheckingProgress
            // 
            this.layoutControlItemCheckingProgress.Control = this.progressBarControl1;
            this.layoutControlItemCheckingProgress.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemCheckingProgress.Name = "layoutControlItemCheckingProgress";
            this.layoutControlItemCheckingProgress.Size = new System.Drawing.Size(179, 26);
            this.layoutControlItemCheckingProgress.Text = "Процес перевірки";
            this.layoutControlItemCheckingProgress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCheckingProgress.TextVisible = false;
            // 
            // layoutControlGroupFirst
            // 
            this.layoutControlGroupFirst.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemServiceCenter,
            this.layoutControlItemVisaCategory,
            this.layoutControlItemStartButton,
            this.layoutControlItemCheckingProgress});
            this.layoutControlGroupFirst.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupFirst.Name = "layoutControlGroupFirst";
            this.layoutControlGroupFirst.Size = new System.Drawing.Size(343, 116);
            this.layoutControlGroupFirst.Text = "Перевірка найближчої доступної дати";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 158);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(907, 316);
            this.gridControl1.TabIndex = 10;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // layoutControlItemClientRegistry
            // 
            this.layoutControlItemClientRegistry.Control = this.gridControl1;
            this.layoutControlItemClientRegistry.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemClientRegistry.Name = "layoutControlItemClientRegistry";
            this.layoutControlItemClientRegistry.Size = new System.Drawing.Size(911, 320);
            this.layoutControlItemClientRegistry.Text = "Реєстрація клієнтів";
            this.layoutControlItemClientRegistry.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClientRegistry.TextVisible = false;
            // 
            // layoutControlGroupSecond
            // 
            this.layoutControlGroupSecond.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemClientRegistry});
            this.layoutControlGroupSecond.Location = new System.Drawing.Point(0, 116);
            this.layoutControlGroupSecond.Name = "layoutControlGroupSecond";
            this.layoutControlGroupSecond.Size = new System.Drawing.Size(935, 362);
            this.layoutControlGroupSecond.Text = "Реєстрація клієнтів";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(343, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(592, 116);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 498);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visa Helper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVisaCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditServiceCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServiceCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVisaCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckingProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClientRegistry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditVisaCategory;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditServiceCenter;
        private DevExpress.XtraEditors.SimpleButton buttonShow;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemServiceCenter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStartButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemVisaCategory;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCheckingProgress;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFirst;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupSecond;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClientRegistry;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}

