using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoulpApp.Models;

namespace PoulpApp.Services
{
    public class MockUserStore : IDataStore<User>
    {
        readonly List<User> _users;

        public MockUserStore()
        {
            _users = new List<User>()
            {
                new User() {Id = Guid.NewGuid().ToString(), Name = "Martin Devreese", Email = "mdevreese@ensc.fr", Password = "salut"},
            };
        }
        public async Task<bool> AddItemAsync(User user)
        {
            _users.Add(user);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User user)
        {
            var oldUser = _users.Where((User arg) => arg.Id == user.Id).FirstOrDefault();
            _users.Remove(oldUser);
            _users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldUser = _users.Where((User arg) => arg.Id == id).FirstOrDefault();
            _users.Remove(oldUser);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(string id)
        {
            return await Task.FromResult(_users.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_users);
        }

    }
}