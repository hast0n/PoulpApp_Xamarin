using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoulpApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoulpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeerDetailPage : ContentPage
    {
        private BeerDetailViewModel viewModel;

        public BeerDetailPage(BeerDetailViewModel _vm)
        {
            InitializeComponent();
            BindingContext = viewModel = _vm;
        }

        private async void ReturnToListView_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}