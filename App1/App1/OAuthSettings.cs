using System;

namespace App1
{
	public class OAuthSettings
	{
		public readonly Uri AuthorizeUrl;
		public readonly string ClientId;
		public readonly string Scope;
		public readonly Uri RedirectUrl;

		public OAuthSettings(string clientId, string scope, string authorizeUrl, string redirectUrl)
		{
			RedirectUrl = new Uri(redirectUrl);
			ClientId = clientId;
			Scope = scope;
			AuthorizeUrl = new Uri(authorizeUrl);
		}
	}
}
