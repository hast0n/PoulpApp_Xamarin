﻿namespace PoulpApp
{
    public static class Constants
    {
        public static string ServiceId = "GOOGLE_AUTHENTICATOR";
        public static string CurrentUser = "CURRENT_USER";
        public const string EventLaunchLoginPage = "EVENT_LAUNCH_LOGIN_PAGE";
        public const string EventLaunchMainPage = "EVENT_LAUNCH_MAIN_PAGE";
        public const string EventLaunchProfileView = "EVENT_LAUNCH_PROFILE_VIEW";
        public const string EventInformNetworkIssues = "EVENT_INFORM_NETWORK_ISSUES";
        public const string EventLogoutRequest = "LOGOUT_REQUEST";
        public const string AskLogoutCommandTriggered = "ASK_LOGOUT_COMMAND_TRIGGERED";
        public const string AddCartItemEvent = "ADD_CART_ITEM_EVENT";
        public const string RemoveCartItemEvent = "REMOVE_CART_ITEM_EVENT";
        public static string AuthenticationSuccess = "AUTHENTICATION_SUCCESS";
        public static string IsLoadingUser = "IS_LOADING_USER";

        public static string AppName = "PoulpApp";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "154307917586-kue1qp25oe5pd80030o53ucvl0a7qu8t.apps.googleusercontent.com";
        public static string AndroidClientId = "154307917586-0ehdaf5dblreni73b7371k0l0lllfvel.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.profile";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.154307917586-kue1qp25oe5pd80030o53ucvl0a7qu8t:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.154307917586-0ehdaf5dblreni73b7371k0l0lllfvel:/oauth2redirect";
    }
}