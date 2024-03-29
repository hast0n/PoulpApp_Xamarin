﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using PoulpApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoulpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public IAsyncCommand VerifyAsyncCommand { get; private set; }
        private readonly GoogleAuthenticator _GoogleAuth;

        public LoadingPage()
        {
            InitializeComponent();
            VerifyAsyncCommand = new AsyncCommand(ExecuteVerifyAsync, CanExecuteVerify);
            _GoogleAuth = new GoogleAuthenticator();

            MessagingCenter.Subscribe<MessageService>(this, Constants.EventInformNetworkIssues, async (sender) =>
            {
                await DisplayAlert("Oops...",
                    "Il semble que nous ne parvenons pas à joindre l'Internet ! Vérifiez votre connexion et réessayez.",
                    "Réessayer");

                await VerifyAsyncCommand.ExecuteAsync();
            });

        }

        private bool CanExecuteVerify(object arg)
        {
            throw new NotImplementedException();
        }

        private async Task ExecuteVerifyAsync()
        {
            try
            {
                IsBusy = true;
                User user = await _GoogleAuth.VerifyAndGetUserAsync();

                try
                {

                    MessagingCenter.Send(new MessageService(), Constants.EventLaunchMainPage, user);
                }
                catch (Exception e)
                {

                }
            }
            catch (NoStoredUserException e)
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchLoginPage);
            }
            catch (NotAbleToAuthenticate e)
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchLoginPage);
            }
            catch (NetworkIssuesException)
            {
                MessagingCenter.Send(new MessageService(), Constants.EventInformNetworkIssues);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VerifyAsyncCommand.ExecuteAsync();
        }
    }
}