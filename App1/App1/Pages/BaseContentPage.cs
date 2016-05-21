using System;
using Xamarin.Forms;

namespace App1
{
	public class BaseContentPage : ContentPage
	{
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (!App.Instance.IsAuthenticated)
				Navigation.PushModalAsync (new LoginPage ());
		}
	}
}