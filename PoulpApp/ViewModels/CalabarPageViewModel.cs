using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoulpApp.Models;
using PoulpApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.Services
{
    public class CalabarPageViewModel: BaseViewModel
    {
        public ObservableCollection<Beer> BeerCollection { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Cart UserCart { get; set; }
        public User CurrentUser { get; private set; }

        public CalabarPageViewModel()
        {
            Title = "Cala'Bar";
            BeerCollection = new ObservableCollection<Beer>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            UserCart = new Cart();

            CurrentUser = User.SecureGetAsyncTask().Result;

            MessagingCenter.Subscribe<MessageService, Beer>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Beer;
                BeerCollection.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<MessageService, Product>(this, Constants.AddCartItemEvent, (sender, product) =>
            {
                UserCart.AddProduct(product.Id, product.Amount, product.UnitPrice);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                BeerCollection.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    BeerCollection.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
