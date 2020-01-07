using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BottomBar.XamarinForms;
using FormsControls.Base;
using PoulpApp.Services;
using PoulpApp.ViewModels;
using Xamarin.Forms;

namespace PoulpApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : BottomBarPage, IAnimationPage
    {
        #region Animation
        public IPageAnimation PageAnimation { get; } = new FlipPageAnimation { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromTop };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
        #endregion

        private MainPageViewModel _viewModel;

        public MainPage(User user)
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainPageViewModel(user);

            MessagingCenter.Subscribe<MessageService>(this,  Constants.AskLogoutCommandTriggered, async (sender) =>
            {
                bool askLogout = await DisplayAlert("Se déconnecter",
                    "Voulez-vous vous déconnecter du service ?",
                    "Se déconnecter", "Annuler");
                if (askLogout)
                {
                    MessagingCenter.Send(new MessageService(), Constants.EventLogoutRequest);
                }
            });

            MessagingCenter.Subscribe<MessageService>(this, Constants.EventLaunchProfileView, async (sender) =>
            {
                await Navigation.PushModalAsync(new ProfilePage(_viewModel));
            });

        }
    }
}
