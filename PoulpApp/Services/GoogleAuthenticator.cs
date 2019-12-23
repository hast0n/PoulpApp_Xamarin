using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.Services
{
    public class NoStoredUserException : Exception
    {
        public NoStoredUserException()
        {
        }

        public NoStoredUserException(string message)
            : base(message)
        {
        }

        public NoStoredUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    public class NotAbleToAuthenticate : Exception
    {
        public NotAbleToAuthenticate()
        {
        }

        public NotAbleToAuthenticate(string message)
            : base(message)
        {
        }

        public NotAbleToAuthenticate(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class GoogleAuthenticator
    {
        public OAuth2Authenticator Auth { get; private set; }

        public GoogleAuthenticator()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            Auth = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);


        }

        public void Authenticate()
        {
            var authenticator = Auth;

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        public async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;

            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();

                SecureStorage.Remove(Constants.serviceId);

                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);

                    await SecureStorage.SetAsync(Constants.serviceId, e.Account.Serialize());

                    MessagingCenter.Send(new MessageService(), Constants.AuthenticationSuccess, user);
                }
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }

        public async Task<string> AskNewTokenAsync(Account account)
        {
            var queryValuesDictionary = new Dictionary<string, string>
            {
                {"refresh_token", account.Properties["refresh_token"]},
                {"client_id", Auth.ClientId},
                {"grant_type", "refresh_token"},
                {"client_secret", null},
            };

            var response = await Auth.RequestAccessTokenAsync(queryValuesDictionary);
            return response["access_token"];
        }

        public static async Task<Response> AskUserInfoAsync(Account account)
        {
            var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, account);
            return await request.GetResponseAsync();
        }

        public async Task<User> VerifyAndGetUserAsync()
        {
            User user = null;
            string serializedAccount = await SecureStorage.GetAsync(Constants.serviceId);

            if (string.IsNullOrEmpty(serializedAccount))
            {
                throw new NoStoredUserException("No value for user in disk");
            }
            else
            {
                var account = Account.Deserialize(serializedAccount);
                user = await GetUserAsync(account);

                if (user == null)
                {
                    throw new NotAbleToAuthenticate("Couldn't get valid user credentials, need to re-authenticate.");
                }
            }

            return user;
        }

        private async Task<User> GetUserAsync(Account account)
        {
            User user = null;
            var response1 = await AskUserInfoAsync(account);

            if (response1.StatusCode == HttpStatusCode.Unauthorized)
            {
                string newToken = await AskNewTokenAsync(account);

                account.Properties["access_token"] = newToken;
                response1 = await AskUserInfoAsync(account);
            }

            string userJson = await response1.GetResponseTextAsync();
            user = JsonConvert.DeserializeObject<User>(userJson);

            return user;
        }
    }
}
