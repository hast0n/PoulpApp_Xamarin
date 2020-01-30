using System;
using PoulpApp.Models;
using PoulpApp.Services;
using PoulpApp.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoulpApp.Views.Tools
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeerDetailPopupPage
    {
        public BeerDetailPopupPage(BeerDetailViewModel _vm)
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}