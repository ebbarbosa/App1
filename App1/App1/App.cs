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
		//const string facebookAppId = "231850827189753"; prd
		const string facebookAppId = "231850827189753";

		// just a singleton pattern so I can have the concept of an app instance
		static volatile App _Instance;
		static object _SyncRoot = new Object ();

		public static App Instance {
			get {
				if (_Instance == null) {
					lock (_SyncRoot) {
						if (_Instance == null) {

							_Instance = new App ();

							_Instance.OAuthSettings =
								new OAuthSettings (
									clientId: "231850827189753",
									scope: "public_profile,email,user_about_me,user_friends",
								authorizeUrl: "https://m.facebook.com/dialog/oauth/",
								redirectUrl: "https://localhost:85/app1" 
									//"http://www.facebook.com/connect/login_success.html"
								);

							var profilePage = new ProfilePage ();
							_Instance._NavPage = new NavigationPage (profilePage);
							_Instance.MainPage = _Instance._NavPage;
						}
					}
				}

				return _Instance;
			}
		}

		public OAuthSettings OAuthSettings { get; private set; }

		NavigationPage _NavPage;

		public bool IsAuthenticated {
			get { return !string.IsNullOrWhiteSpace (_Token); }
		}

		string _Token;

		public string Token {
			get { return _Token; }
		}

		public void SaveToken (string token)
		{
			_Token = token;

			// broadcast a message that authentication was successful 
			MessagingCenter.Send<App> (this, "Authenticated");

			//_NavPage.Navigation.PushAsync (new CourtPage ());
		}

		public Action SuccessfulLoginAction {
			get {
				return new Action (() => _NavPage.Navigation.PopModalAsync ());
			}
		}
	}
}