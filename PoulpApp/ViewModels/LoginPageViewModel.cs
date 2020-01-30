using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Forms;
using PoulpApp.Services;
using Xamarin.Essentials;

namespace PoulpApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        public Command GoogleLoginCommand { get; set; }
        public GoogleAuthenticator Auth;
        private MessageService MS;

        private bool _isLoadingMainPage;
        public bool IsLoadingMainPage
        {
            get => _isLoadingMainPage;
            set => SetProperty(ref _isLoadingMainPage, value);
        }

        private bool _displayLogInButton;
        public bool DisplayLogInButton
        {
            get => _displayLogInButton;
            set => SetProperty(ref _displayLogInButton, value);
        }

        public LoginPageViewModel()
        {
            Auth = new GoogleAuthenticator();
            MS = new MessageService();

            IsLoadingMainPage = false;
            DisplayLogInButton = true;

            GoogleLoginCommand = new Command(() =>
            {
                Auth.Authenticate();
            });

            MessagingCenter.Subscribe<MessageService, User>(this, Constants.AuthenticationSuccess, async (sender, user) =>
            {
                // TODO: Get current registered user in secure storage and verify link with google auth
                // TODO: If user is still valid (w/ new token), retrieve from secure storage
                // TODO: Verify link between stored user and service token id

                MessagingCenter.Send(MS, Constants.EventLaunchMainPage, user);
            });

            MessagingCenter.Subscribe<MessageService>(this, Constants.IsLoadingUser, (sender) =>
            {
                IsLoadingMainPage = true;
                DisplayLogInButton = false;
            });
        }
    }
}


// Qqun se connecte -> ServiceId initialisé