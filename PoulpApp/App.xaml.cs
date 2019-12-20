using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PoulpApp.Services;
using PoulpApp.ViewModels;
using PoulpApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp
{
    public partial class App : Application
    {
        
        private bool isLoggedIn => !string.IsNullOrEmpty(SecureStorage.GetAsync(Constants.serviceId).Result);

        public App()
        {
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockUserStore>();

            MessagingCenter.Subscribe<LoginPageViewModel>(this, "EVENT_LAUNCH_MAIN_PAGE", SetMainPageAsRootPage);
            MessagingCenter.Subscribe<LoginPageViewModel>(this, "EVENT_LAUNCH_LOGIN_PAGE", SetLoginPageAsRootPage);
            
            if (isLoggedIn) // really bad -> need to check refresh token and get new auth token
            {
                SetMainPageAsRootPage(this);
            }
            else
            {
                SetLoginPageAsRootPage(this);
            }

        }

        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetMainPageAsRootPage(object sender)
        {
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

}
