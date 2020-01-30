using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoulpApp.Models;
using Xamarin.Essentials;

namespace PoulpApp
{
    [JsonObject]
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("cart")]
        public List<Product> Cart { get; set; }

        [JsonProperty("privilege")]
        public int Privilege { get; set; }


        public static async Task<User> SecureGetAsyncTask()
        {
            string userJson = await SecureStorage.GetAsync(Constants.CurrentUser);
            return string.IsNullOrEmpty(userJson) ? new User() : JsonConvert.DeserializeObject<User>(userJson);
        }

        public async Task SecureSetAsyncTask()
        {
            await SecureStorage.SetAsync(Constants.CurrentUser, JsonConvert.SerializeObject(this));
        }
    }
}