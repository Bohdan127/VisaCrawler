//#define UseNewUI

using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Visa.WinForms.ErrorProvider;
using NLog;

namespace Visa.WinForms
{
    internal static class Program
    {
        private static readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create a new object, representing the German culture.
            var culture = CultureInfo.CreateSpecificCulture("uk-UA");

            // The following line provides localization for the application's user interface.
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.ThreadException += Application_ThreadException;

            // Set the unhandled exception mode to force all Windows Forms
            // errors to go through our handler.
            Application.SetUnhandledExceptionMode(
                UnhandledExceptionMode.CatchException);

#if UseNewUI
            Application.Run(new MainFormNew());
#else
            Application.Run(new MainForm());
#endif
        }

        private static void Application_ThreadException(object sender,
            ThreadExceptionEventArgs e)
        {
            _logger.Fatal(e.Exception.Message);
            _logger.Fatal(e.Exception.StackTrace);
            ExceptionHandlerForm.ShowException(e.Exception);
        }
    }
}