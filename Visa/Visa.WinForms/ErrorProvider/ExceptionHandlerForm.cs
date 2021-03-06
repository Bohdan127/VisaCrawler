﻿using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using GlobalResources;
using Visa.BusinessLogic.Managers;

namespace Visa.WinForms.ErrorProvider
{
    public sealed partial class ExceptionHandlerForm : XtraForm
    {
        public ExceptionHandlerForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            if (DesignMode)
                return;
            Text = ResManager.GetString(ResKeys.ExceptionHanler_Title);
            textBox2.Text =
                ResManager.GetString(ResKeys.ExceptionHanler_Text);
        }

        public string StackTrace
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public static void ShowException(Exception ex)
        {
            var form = new ExceptionHandlerForm
            {
                StackTrace = ex.Message + ex.StackTrace
            };
            form.ShowDialog();

            var sendMailResult =
               EmailManager.SendErrorMail(ex); //sending mail message
            MessageBox.Show(sendMailResult, "Sending e-mail result:");
        }

        private void simpleButton1_Click(object sender,
            EventArgs e)
        {
            Close();
        }
    }
}