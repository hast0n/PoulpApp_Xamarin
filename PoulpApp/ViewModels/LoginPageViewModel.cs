using Xamarin.Auth;
using Xamarin.Forms;
using PoulpApp.Services;

namespace PoulpApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
	{
        public Command GoogleLoginCommand { get; set; }
        public GoogleAuthenticator Auth;

		public LoginPageViewModel()
        {
            Auth = new GoogleAuthenticator();

            GoogleLoginCommand = new Command(() =>
            {
                Auth.Authenticate();
            });

            MessagingCenter.Subscribe<MessageService, User>(this, Constants.AuthenticationSuccess, async (sender, user) =>
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchMainPage, user);
            });
        }
    }
}