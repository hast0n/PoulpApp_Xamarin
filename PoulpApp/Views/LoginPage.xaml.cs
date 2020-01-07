using System;
using PoulpApp.Services;
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
        }
    }
}