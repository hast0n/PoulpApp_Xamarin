using PoulpApp.Services;
using PoulpApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public User CurrentUser { get; }
        public Command AskLogoutCommand { get; set; }
        private MessageService MS;
        
        public MainPageViewModel(User user)
        {
            CurrentUser = user;

            AskLogoutCommand = new Command(() =>
            {
                MessagingCenter.Send(new MessageService(), "ASK_LOGOUT_COMMAND_TRIGGERED");
            });

            MessagingCenter.Subscribe<MessageService>(this, "LOGOUT_REQUEST", Logout);
        }

        private void Logout(object sender)
        {
            SecureStorage.RemoveAll();
            MessagingCenter.Send(new MessageService(), "EVENT_LAUNCH_LOGIN_PAGE");
        }
    }
}
