using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PoulpApp.ViewModels
{
	public partial class LoginPageViewModel : BaseViewModel
	{
        private string storedJson;


        public bool _displayLogInButton;
        public bool DisplayLogInButton
        {
            get => _displayLogInButton;
			set => SetProperty(ref _displayLogInButton, value);
		}

        public bool _isLoadingMainPage;
        public bool IsLoadingMainPage
        {
            get => _isLoadingMainPage;
            set => SetProperty(ref _isLoadingMainPage, value);
        }

        public Command GoogleLoginCommand { get; set; }

		public LoginPageViewModel()
        {
            GoogleLoginCommand = new Command(() => OnLoginClicked());
            IsLoadingMainPage = false;
            DisplayLogInButton = true;
        }

		async void OnLoginClicked()
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

            storedJson = await SecureStorage.GetAsync(Constants.serviceId);
			
			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				Constants.Scope,
				new Uri(Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(Constants.AccessTokenUrl),
				null,
				true);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
            if (sender is OAuth2Authenticator authenticator)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			User user = null;

			if (e.IsAuthenticated)
            {
                DisplayLogInButton = false;
                IsLoadingMainPage = true;
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
				var response = await request.GetResponseAsync();

				if (storedJson != null)
                {
                    SecureStorage.Remove(Constants.serviceId);
                }

				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<User>(userJson);
                    await SecureStorage.SetAsync(Constants.serviceId, e.Account.Properties["refresh_token"]);
				    MessagingCenter.Send(this, "EVENT_LAUNCH_MAIN_PAGE", user);
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
	}
}