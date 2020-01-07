using Xamarin.Auth;
using Xamarin.Forms;
using PoulpApp.Services;

namespace PoulpApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {

        public Command GoogleLoginCommand { get; set; }

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

        public GoogleAuthenticator Auth;
        private MessageService MS;

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