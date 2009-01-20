using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	public class WebView : ViewBase
	{
		readonly WebBrowser browser;

		public override string ToString()
		{
			return browser.DocumentTitle;
			//string s = browser.Document.Title;
			//return string.IsNullOrEmpty(s) ? "Loading..." : s;
		}

		public WebView(MultipleViewManager host, string uri, object controller)
			: base(host, new WebBrowser())
		{
			browser = (WebBrowser)self;
			if (controller != null)
				browser.ObjectForScripting = controller;

			browser.IsWebBrowserContextMenuEnabled = false;

			browser.Navigated += delegate { host.Invalidate(); };
			browser.Navigate(new Uri(uri));
		}

		public void Refresh()
		{
			browser.Refresh(WebBrowserRefreshOption.Completely);
		}
	}
}
