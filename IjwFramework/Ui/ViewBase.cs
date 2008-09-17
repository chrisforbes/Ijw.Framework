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
			self.Anchor = AnchorUtil.ClientArea;
			self.Bounds = host.ViewBounds;

			host.host.Controls.Add(self);
			self.Visible = true;
			Focus();
		}

		public void Hide()
		{
			host.host.Controls.Remove(self);
			self.Visible = false;
		}

		protected virtual void Focus() {}
	}
}
