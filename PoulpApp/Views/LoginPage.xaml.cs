using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoulpApp.Services;
using PoulpApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoulpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainPageViewModel();
        }
    }
}