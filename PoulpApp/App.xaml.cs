using PoulpApp.Services;
using PoulpApp.ViewModels;
using PoulpApp.Views;
using System;
using FormsControls.Base;
using Xamarin.Forms;

namespace PoulpApp
{
    public partial class App : Application
    {
        
        public App()
        {
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockUserStore>();

            MessagingCenter.Subscribe<LoginPageViewModel, User>(this, "EVENT_LAUNCH_MAIN_PAGE", SetMainPageAsRootPage);
            MessagingCenter.Subscribe<MainPageViewModel>(this, "EVENT_LAUNCH_LOGIN_PAGE", SetLoginPageAsRootPage);
            
            MainPage = new LoginPage();
        }

        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetMainPageAsRootPage(object sender, User user)
        {
            MainPage = new AnimationNavigationPage(new MainPage(user));
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
