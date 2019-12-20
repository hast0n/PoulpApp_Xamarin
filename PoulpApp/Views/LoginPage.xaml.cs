using System;
using PoulpApp.ViewModels;
using Xamarin.Forms;

namespace PoulpApp.Views
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;

		public LoginPage()
		{
			InitializeComponent();
            BindingContext = _viewModel = new LoginPageViewModel();

            MessagingCenter.Subscribe<LoginPageViewModel, User>(this, "EVENT_LAUNCH_MAIN_PAGE", async(sender, user) =>
            {
                await DisplayAlert("Email address", user.Email, "OK");
            });
        }
    }
}