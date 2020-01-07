using PoulpApp.Services;
using PoulpApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public User CurrentUser { get; }
        public Command ShowProfileCommand { get; set; }
        public Command AskLogoutCommand { get; set; }
        private MessageService MS;
        
        public MainPageViewModel(User user)
        {
            CurrentUser = user;

            ShowProfileCommand = new Command(() =>
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchProfileView);
            });
            
            AskLogoutCommand = new Command(() =>
            {
                MessagingCenter.Send(new MessageService(), Constants.AskLogoutCommandTriggered);
            });

            MessagingCenter.Subscribe<MessageService>(this, Constants.EventLogoutRequest, Logout);
        }

        private void Logout(object sender)
        {
            SecureStorage.RemoveAll();
            MessagingCenter.Send(new MessageService(), Constants.EventLaunchLoginPage);
        }
    }
}
