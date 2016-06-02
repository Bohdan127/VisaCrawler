using System.Diagnostics;
using Visa.Universal.App.Models;
using Visa.Universal.App.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Visa.Universal.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private Account _account;

        public Login()
        {
            this.InitializeComponent();
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            SignInPassport();
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }

        private async void SignInPassport()
        {
            if (AccountHelper.ValidateAccountCredentials(UsernameTextBox.Text))
            {
                // Create and add a new local account
                _account = AccountHelper.AddAccount(UsernameTextBox.Text);
                Debug.WriteLine("Successfully signed in with traditional credentials and created local account instance!");

                if (await MicrosoftPassportHelper.CreatePassportKeyAsync(UsernameTextBox.Text))
                {
                    Debug.WriteLine("Successfully signed in with Microsoft Passport!");
                }
            }
            else
            {
                ErrorMessage.Text = "Invalid Credentials";
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check Microsoft Passport is setup and available on this machine
            if (!await MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync())
            {
                // Microsoft Passport is not setup so inform the user
                PassportStatus.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                PassportStatusText.Text = "Microsoft Passport is not setup!\n" +
                    "Please go to Windows Settings and set up a PIN to use it.";
                PassportSignInButton.IsEnabled = false;
            }
        }
    }
}
