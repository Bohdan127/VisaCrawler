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
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEditVisaCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditServiceCenter = new DevExpress.XtraEditors.LookUpEdit();
            this.buttonShow = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemServiceCenter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStartButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemVisaCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControlItemCheckingProgress = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVisaCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditServiceCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServiceCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVisaCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckingProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.progressBarControl1);
            this.dataLayoutControl1.Controls.Add(this.dateEditTo);
            this.dataLayoutControl1.Controls.Add(this.dateEditFrom);
            this.dataLayoutControl1.Controls.Add(this.lookUpEditVisaCategory);
            this.dataLayoutControl1.Controls.Add(this.lookUpEditServiceCenter);
            this.dataLayoutControl1.Controls.Add(this.buttonShow);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(290, 165);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(102, 84);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Size = new System.Drawing.Size(176, 20);
            this.dateEditTo.StyleController = this.dataLayoutControl1;
            this.dateEditTo.TabIndex = 8;
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(102, 60);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Size = new System.Drawing.Size(176, 20);
            this.dateEditFrom.StyleController = this.dataLayoutControl1;
            this.dateEditFrom.TabIndex = 7;
            // 
            // lookUpEditVisaCategory
            // 
            this.lookUpEditVisaCategory.Location = new System.Drawing.Point(102, 36);
            this.lookUpEditVisaCategory.Name = "lookUpEditVisaCategory";
            this.lookUpEditVisaCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditVisaCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Категорія Візи")});
            this.lookUpEditVisaCategory.Properties.DisplayMember = "Name";
            this.lookUpEditVisaCategory.Properties.NullText = "-Оберіть візову категорію-";
            this.lookUpEditVisaCategory.Properties.ValueMember = "Value";
            this.lookUpEditVisaCategory.Size = new System.Drawing.Size(176, 20);
            this.lookUpEditVisaCategory.StyleController = this.dataLayoutControl1;
            this.lookUpEditVisaCategory.TabIndex = 6;
            // 
            // lookUpEditServiceCenter
            // 
            this.lookUpEditServiceCenter.Location = new System.Drawing.Point(102, 12);
            this.lookUpEditServiceCenter.Name = "lookUpEditServiceCenter";
            this.lookUpEditServiceCenter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditServiceCenter.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Візовий Сервіс Центр")});
            this.lookUpEditServiceCenter.Properties.DisplayMember = "Name";
            this.lookUpEditServiceCenter.Properties.NullText = "-Оберіть ППВА-";
            this.lookUpEditServiceCenter.Properties.ValueMember = "Value";
            this.lookUpEditServiceCenter.Size = new System.Drawing.Size(176, 20);
            this.lookUpEditServiceCenter.StyleController = this.dataLayoutControl1;
            this.lookUpEditServiceCenter.TabIndex = 5;
            // 
            // buttonShow
            // 
            this.buttonShow.Image = ((System.Drawing.Image)(resources.GetObject("buttonShow.Image")));
            this.buttonShow.Location = new System.Drawing.Point(105, 130);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(173, 22);
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
            this.layoutControlItemServiceCenter,
            this.layoutControlItemStartButton,
            this.layoutControlItemVisaCategory,
            this.layoutControlItemFrom,
            this.layoutControlItemTo,
            this.emptySpaceItem1,
            this.layoutControlItemCheckingProgress});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(290, 165);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemServiceCenter
            // 
            this.layoutControlItemServiceCenter.Control = this.lookUpEditServiceCenter;
            this.layoutControlItemServiceCenter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemServiceCenter.Name = "layoutControlItemServiceCenter";
            this.layoutControlItemServiceCenter.Size = new System.Drawing.Size(270, 24);
            this.layoutControlItemServiceCenter.Text = "Сервісний ценрт:";
            this.layoutControlItemServiceCenter.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItemStartButton
            // 
            this.layoutControlItemStartButton.Control = this.buttonShow;
            this.layoutControlItemStartButton.Location = new System.Drawing.Point(93, 118);
            this.layoutControlItemStartButton.Name = "layoutControlItemStartButton";
            this.layoutControlItemStartButton.Size = new System.Drawing.Size(177, 27);
            this.layoutControlItemStartButton.Text = "Запустити перевірку";
            this.layoutControlItemStartButton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemStartButton.TextVisible = false;
            // 
            // layoutControlItemVisaCategory
            // 
            this.layoutControlItemVisaCategory.Control = this.lookUpEditVisaCategory;
            this.layoutControlItemVisaCategory.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemVisaCategory.Name = "layoutControlItemVisaCategory";
            this.layoutControlItemVisaCategory.Size = new System.Drawing.Size(270, 24);
            this.layoutControlItemVisaCategory.Text = "Візова категорія:";
            this.layoutControlItemVisaCategory.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItemFrom
            // 
            this.layoutControlItemFrom.Control = this.dateEditFrom;
            this.layoutControlItemFrom.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemFrom.Name = "layoutControlItemFrom";
            this.layoutControlItemFrom.Size = new System.Drawing.Size(270, 24);
            this.layoutControlItemFrom.Text = "Дата від:";
            this.layoutControlItemFrom.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItemTo
            // 
            this.layoutControlItemTo.Control = this.dateEditTo;
            this.layoutControlItemTo.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemTo.Name = "layoutControlItemTo";
            this.layoutControlItemTo.Size = new System.Drawing.Size(270, 24);
            this.layoutControlItemTo.Text = "Дата до:";
            this.layoutControlItemTo.TextSize = new System.Drawing.Size(87, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 118);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(93, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(12, 108);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(266, 18);
            this.progressBarControl1.StyleController = this.dataLayoutControl1;
            this.progressBarControl1.TabIndex = 9;
            // 
            // layoutControlItemCheckingProgress
            // 
            this.layoutControlItemCheckingProgress.Control = this.progressBarControl1;
            this.layoutControlItemCheckingProgress.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemCheckingProgress.Name = "layoutControlItemCheckingProgress";
            this.layoutControlItemCheckingProgress.Size = new System.Drawing.Size(270, 22);
            this.layoutControlItemCheckingProgress.Text = "Процес перевірки";
            this.layoutControlItemCheckingProgress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCheckingProgress.TextVisible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 165);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVisaCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditServiceCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServiceCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVisaCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckingProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditVisaCategory;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditServiceCenter;
        private DevExpress.XtraEditors.SimpleButton buttonShow;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemServiceCenter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStartButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemVisaCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCheckingProgress;
    }
}

