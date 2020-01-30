using PoulpApp.Models;
using PoulpApp.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
    public class BeerDetailViewModel: BaseViewModel
    {
        public Beer Item { get; set; }
        private MessageService MS;
        public Product CurrentProduct { get; set; }

        private int _currentAmount;
        public int CurrentAmount
        {
            get => _currentAmount;
            set
            {
                SetProperty(ref _currentAmount, value);
            }
        }

        public Command AddOneCommand { get; set; }
        public Command DelOneCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        
        public BeerDetailViewModel(Beer item)
        {
            Item = item;
            MS = new MessageService();
            CurrentAmount = 1;
            CurrentProduct = new Product(Item.Id, CurrentAmount, Item.Price);

            AddOneCommand = new Command(() =>
            {
                int value = ++CurrentProduct.Amount;
                CurrentProduct.Amount = CurrentAmount = (value > Item.Quantity) ? Item.Quantity : value;
            });
            
            DelOneCommand = new Command(() =>
            {
                int value = --CurrentProduct.Amount;
                CurrentProduct.Amount = CurrentAmount = (value < 0) ? 0 : value;
            });

            AddToCartCommand = new Command(async () =>
            {
                MessagingCenter.Send(MS, Constants.AddCartItemEvent, CurrentProduct);
                await PopupNavigation.Instance.PopAsync();
            });
        }
    }
}
