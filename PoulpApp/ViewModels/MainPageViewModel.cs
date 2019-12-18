using System;
using System.Collections.Generic;
using System.Linq;
using PoulpApp.Services;
using PoulpApp.Models;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IGoogleManager _googleManager;

        public Command GoogleLoginCommand { get; set; }
        public Command GoogleLogoutCommand { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isLogedIn;

        public bool IsLogedIn
        {
            get { return _isLogedIn; }
            set { SetProperty(ref _isLogedIn, value); }
        }

        private GoogleUser _googleUser;

        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { SetProperty(ref _googleUser, value); }
        }

        public MainPageViewModel(IGoogleManager googleManager)
        {
            _googleManager = googleManager;

            IsLogedIn = false;
            //GoogleLoginCommand = new Command(async() => await GoogleLoginTask());
            //GoogleLogoutCommand = new Command(async() => await GoogleLogoutTask());
        }

        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }

        private void GoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);

        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                IsLogedIn = true;
            }
            else
            {
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
    }
}
