using App1;
using App1.Droid;
using Android.App;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace App1.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {	
            base.OnElementChanged(e);

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;
            var appInstance = App.Instance;
            var oAuthSettings = appInstance.OAuthSettings;
                        
            var auth = new OAuth2Authenticator(
      clientId: oAuthSettings.ClientId,
      scope: oAuthSettings.Scope,
      authorizeUrl: oAuthSettings.AuthorizeUrl,
      redirectUrl: oAuthSettings.RedirectUrl);

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    appInstance.SuccessfulLoginAction.Invoke();
                    // Use eventArgs.Account to do wonderful things
                    appInstance.SaveToken(eventArgs.Account.Properties["access_token"]);
                }
                else
                {
                    // The user cancelled
                }
            };
			var ui = auth.GetUI (this.Context);	
            activity.StartActivity(ui);
        }
    }
}