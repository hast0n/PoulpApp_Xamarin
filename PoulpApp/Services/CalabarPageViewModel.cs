using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using PoulpApp.Models;
using PoulpApp.ViewModels;
using Xamarin.Forms;

namespace PoulpApp.Services
{
    class CalabarPageViewModel: BaseViewModel
    {
        public ObservableCollection<Beer> BeerCollection { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CalabarPageViewModel()
        {
            Title = "Cala'Bar";
            BeerCollection = new ObservableCollection<Beer>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<MessageService, Beer>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Beer;
                BeerCollection.Add(newItem);
                await DataStore.AddItemAsync(newItem);
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
