using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	public class ViewBase
	{
		protected MultipleViewManager host;
		protected Control self;

		public ViewBase(MultipleViewManager host, Control self)
		{
			this.host = host;
			this.self = self;
		}

		public void Show()
		{
			host.host.Controls.Add(self);
			self.Anchor = MultipleViewManager.ClientArea;
			self.Bounds = host.ViewBounds;
		}

		public void Hide()
		{
			host.host.Controls.Remove(self);
		}
	}
}
