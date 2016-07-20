namespace Visa.WinForms.Views
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditNationality = new DevExpress.XtraEditors.LookUpEdit();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.toggleSwitchCloseBrowser = new DevExpress.XtraEditors.ToggleSwitch();
            this.toggleSwitchRepeatIfCrash = new DevExpress.XtraEditors.ToggleSwitch();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemNationality = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCloseBrower = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRepeatIfCrash = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditNationality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchCloseBrowser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchRepeatIfCrash.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNationality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCloseBrower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRepeatIfCrash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonCancel);
            this.layoutControl1.Controls.Add(this.simpleButtonOk);
            this.layoutControl1.Controls.Add(this.lookUpEditNationality);
            this.layoutControl1.Controls.Add(this.textEditPassword);
            this.layoutControl1.Controls.Add(this.toggleSwitchCloseBrowser);
            this.layoutControl1.Controls.Add(this.toggleSwitchRepeatIfCrash);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(358, 150);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonCancel.Image")));
            this.simpleButtonCancel.Location = new System.Drawing.Point(180, 116);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(166, 22);
            this.simpleButtonCancel.StyleController = this.layoutControl1;
            this.simpleButtonCancel.TabIndex = 9;
            this.simpleButtonCancel.Text = "simpleButtonCancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonOk.Image")));
            this.simpleButtonOk.Location = new System.Drawing.Point(12, 116);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(164, 22);
            this.simpleButtonOk.StyleController = this.layoutControl1;
            this.simpleButtonOk.TabIndex = 8;
            this.simpleButtonOk.Text = "simpleButtonOk";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lookUpEditNationality
            // 
            this.lookUpEditNationality.Location = new System.Drawing.Point(173, 36);
            this.lookUpEditNationality.Name = "lookUpEditNationality";
            this.lookUpEditNationality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditNationality.Size = new System.Drawing.Size(173, 20);
            this.lookUpEditNationality.StyleController = this.layoutControl1;
            this.lookUpEditNationality.TabIndex = 7;
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(173, 12);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Size = new System.Drawing.Size(173, 20);
            this.textEditPassword.StyleController = this.layoutControl1;
            this.textEditPassword.TabIndex = 6;
            // 
            // toggleSwitchCloseBrowser
            // 
            this.toggleSwitchCloseBrowser.Location = new System.Drawing.Point(173, 60);
            this.toggleSwitchCloseBrowser.Name = "toggleSwitchCloseBrowser";
            this.toggleSwitchCloseBrowser.Properties.OffText = "Off";
            this.toggleSwitchCloseBrowser.Properties.OnText = "On";
            this.toggleSwitchCloseBrowser.Size = new System.Drawing.Size(173, 24);
            this.toggleSwitchCloseBrowser.StyleController = this.layoutControl1;
            this.toggleSwitchCloseBrowser.TabIndex = 4;
            // 
            // toggleSwitchRepeatIfCrash
            // 
            this.toggleSwitchRepeatIfCrash.Location = new System.Drawing.Point(173, 88);
            this.toggleSwitchRepeatIfCrash.Name = "toggleSwitchRepeatIfCrash";
            this.toggleSwitchRepeatIfCrash.Properties.OffText = "Off";
            this.toggleSwitchRepeatIfCrash.Properties.OnText = "On";
            this.toggleSwitchRepeatIfCrash.Size = new System.Drawing.Size(173, 24);
            this.toggleSwitchRepeatIfCrash.StyleController = this.layoutControl1;
            this.toggleSwitchRepeatIfCrash.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemPassword,
            this.layoutControlItemNationality,
            this.layoutControlItemCloseBrower,
            this.layoutControlItemRepeatIfCrash,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(358, 150);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.textEditPassword;
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(158, 13);
            // 
            // layoutControlItemNationality
            // 
            this.layoutControlItemNationality.Control = this.lookUpEditNationality;
            this.layoutControlItemNationality.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemNationality.Name = "layoutControlItemNationality";
            this.layoutControlItemNationality.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItemNationality.TextSize = new System.Drawing.Size(158, 13);
            // 
            // layoutControlItemCloseBrower
            // 
            this.layoutControlItemCloseBrower.Control = this.toggleSwitchCloseBrowser;
            this.layoutControlItemCloseBrower.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemCloseBrower.Name = "layoutControlItemCloseBrower";
            this.layoutControlItemCloseBrower.Size = new System.Drawing.Size(338, 28);
            this.layoutControlItemCloseBrower.TextSize = new System.Drawing.Size(158, 13);
            // 
            // layoutControlItemRepeatIfCrash
            // 
            this.layoutControlItemRepeatIfCrash.Control = this.toggleSwitchRepeatIfCrash;
            this.layoutControlItemRepeatIfCrash.CustomizationFormText = "layoutControlItemRepeatIfCrash";
            this.layoutControlItemRepeatIfCrash.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItemRepeatIfCrash.Name = "layoutControlItemRepeatIfCrash";
            this.layoutControlItemRepeatIfCrash.Size = new System.Drawing.Size(338, 28);
            this.layoutControlItemRepeatIfCrash.TextSize = new System.Drawing.Size(158, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonOk;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(168, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(168, 104);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 150);
            this.Controls.Add(this.layoutControl1);
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditNationality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchCloseBrowser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchRepeatIfCrash.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNationality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCloseBrower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRepeatIfCrash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditNationality;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitchCloseBrowser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCloseBrower;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNationality;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitchRepeatIfCrash;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRepeatIfCrash;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}