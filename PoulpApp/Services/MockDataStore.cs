using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoulpApp.Models;

namespace PoulpApp.Services
{
    public class MockDataStore : IDataStore<Beer>
    {
        readonly List<Beer> _items;

        public MockDataStore()
        {
            _items = new List<Beer>()
            {
                new Beer { Id = Guid.NewGuid().ToString(), Name = "first item", Description="This is a beer description." },
                new Beer { Id = Guid.NewGuid().ToString(), Name = "Second item", Description="This is another beer description." },
                new Beer { Id = Guid.NewGuid().ToString(), Name = "Third item", Description="This is an item description." },
                new Beer { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Description="This is an item description." },
                new Beer { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Description="This is an item description." },
                new Beer { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Description="This is an item description." }
            };
        }



        public async Task<bool> AddItemAsync(Beer item)
        {
            _items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Beer item)
        {
            var oldItem = _items.Where((Beer arg) => arg.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where((Beer arg) => arg.Id == id).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Beer> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Beer>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}