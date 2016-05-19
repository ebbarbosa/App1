using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Auth;

namespace App1
{
    public class App : Application
    {
        //const string facebookAppId = "1609088969410340"; prd
        const string facebookAppId = "1610747612577809";

        // just a singleton pattern so I can have the concept of an app instance 
        static volatile App _Instance;
        static object _SyncRoot = new Object();
		
        public static App Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot) 
                    {
                        if (_Instance == null)
                        {
                            _Instance = new App();
                            _Instance.OAuthSettings =
                                new OAuthSettings(
                                  clientId: facebookAppId,
                                  scope: "",
                                  authorizeUrl: "https://m.facebook.com/dialog/oauth/",
                                  redirectUrl: "http://www.facebook.com/connect/login_success.html");
                        }
                    }
                }

                return _Instance;
            }
        }

        public OAuthSettings OAuthSettings { get; private set; }
        NavigationPage _NavPage;


        public Page GetMainPage()
        {
            var profilePage = new ProfilePage();
            _NavPage = new NavigationPage(profilePage);
            return _NavPage;
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }

        string _Token;
        public string Token
        {
            get { return _Token; }
        }

        public void SaveToken(string token)
        {
            _Token = token;

			// broadcast a message that authentication was successful 
            MessagingCenter.Send<App>(this, "Authenticated");
        }

		public Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() => _NavPage.Navigation.PopModalAsync());
            }
        }
    }

    public class OAuthSettings
    {
        public readonly Uri AuthorizeUrl;
        public readonly string ClientId;
        public readonly string Scope;
        public readonly Uri RedirectUrl;

        public OAuthSettings(string clientId, string scope, string authorizeUrl, string redirectUrl)
        {
            ClientId = clientId;
            Scope = scope;
            AuthorizeUrl = new Uri(authorizeUrl);
            RedirectUrl = new Uri(redirectUrl);
        }
    }
}
