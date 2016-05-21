using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Auth;
using App1;
using App1.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace App1.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var appInstance = App.Instance;
			var oauthSettings = appInstance.OAuthSettings;

			var auth = new OAuth2Authenticator(oauthSettings.ClientId,
				oauthSettings.Scope,
				oauthSettings.AuthorizeUrl,
				oauthSettings.RedirectUrl);
			
			auth.Completed += (object sender, AuthenticatorCompletedEventArgs e) => {
		
				DismissViewController(animated, null);

				if(e.IsAuthenticated)
				{
					App.Instance.SuccessfulLoginAction.Invoke();
					App.Instance.SaveToken(e.Account.Properties["access_token"]);	
				}
			}; 
				
			PresentViewController(auth.GetUI(), true, null);
		}
	}
}

