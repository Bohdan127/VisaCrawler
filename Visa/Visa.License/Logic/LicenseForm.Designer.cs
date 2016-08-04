namespace Visa.License.Logic
{
    partial class LicenseForm
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
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.applyButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(12, 12);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Mask.EditMask = "[0-9a-f]{8}[-]([0-9a-f]{4}[-]){3}[0-9a-f]{12}";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEdit1.Properties.Mask.PlaceHolder = '*';
            this.textEdit1.Size = new System.Drawing.Size(532, 36);
            this.textEdit1.TabIndex = 0;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(550, 12);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(116, 35);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Зарегистрировать";
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // LicenseForm
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 58);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.textEdit1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пожалуйста впишите лицензионный ключ";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton applyButton;
    }
}