using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public class ProfilePage : BaseContentPage
	{
		public ProfilePage ()
		{
			Content = new Label () {
				Text = "Profile Page",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};
		}
	}


	public class BaseContentPage : ContentPage
	{
		
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (!App.Instance.IsAuthenticated)
				Navigation.PushModalAsync (new LoginPage ());
		}
	}

	public partial class LoginPage : ContentPage
	{
	}
}
