using System;
using System.Collections.Generic;
using System.Text;
using PoulpApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public User CurrentUser { get; }
        public Command AskLogoutCommand { get; set; }
        
        public MainPageViewModel(User user)
        {
            CurrentUser = user;

            AskLogoutCommand = new Command(() =>
            {
                MessagingCenter.Send(this, "ASK_LOGOUT_COMMAND_TRIGGERED");
            });

            MessagingCenter.Subscribe<MainPage>(this, "LOGOUT_REQUEST", Logout);
        }

        private void Logout(object sender)
        {
            SecureStorage.Remove(Constants.serviceId);
            MessagingCenter.Send(this, "EVENT_LAUNCH_LOGIN_PAGE");
        }
    }
}
