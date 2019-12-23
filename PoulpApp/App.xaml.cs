using PoulpApp.Services;
using PoulpApp.ViewModels;
using PoulpApp.Views;
using System;
using FormsControls.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockUserStore>();

            MessagingCenter.Subscribe<MessageService, User>(this, Constants.EventLaunchMainPage, SetMainPageAsRootPage);

            MessagingCenter.Subscribe<MessageService>(this, Constants.EventLaunchLoginPage, SetLoginPageAsRootPage);

            MainPage = new LoadingPage();
        }

        private void SetLoginPageAsRootPage(Object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetMainPageAsRootPage(Object sender, User user)
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
