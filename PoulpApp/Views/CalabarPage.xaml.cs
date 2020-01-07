using System;
using PoulpApp.Services;
using PoulpApp.Views.Tools;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoulpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalabarPage : ContentPage
    {
        CalabarPageViewModel viewModel;

        public CalabarPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CalabarPageViewModel();


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as Item;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        private void ClickToShowPopup_OnClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new EditPopupPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.BeerCollection.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}