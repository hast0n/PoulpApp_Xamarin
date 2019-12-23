using System;
using System.Collections.Generic;
using System.Linq;
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

                MessagingCenter.Send(new MessageService(), Constants.EventLaunchMainPage, user);
            }
            catch (NoStoredUserException e)
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchLoginPage);
            }
            catch (NotAbleToAuthenticate e)
            {
                MessagingCenter.Send(new MessageService(), Constants.EventLaunchLoginPage);
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