using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp
{
    public class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator;

        public static async Task<bool> IsValidUser()
        {
            string refresh_token = await SecureStorage.GetAsync("refresh_token");

            //var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
            //var response = await request.GetResponseAsync();
            //if (response != null)
            //{
            //    // Deserialize the data and store it in the account store
            //    // The users email address will be used to identify data in SimpleDB
            //    string userJson = await response.GetResponseTextAsync();
            //    user = JsonConvert.DeserializeObject<User>(userJson);
            //}

            // ---------
            return string.IsNullOrEmpty(refresh_token);
        }
    }

}