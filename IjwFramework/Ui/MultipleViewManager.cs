using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IjwFramework.Ui
{
	public class MultipleViewManager
	{
		internal readonly Control host;
		readonly TabStrip<ViewBase> tabStrip = new TabStrip<ViewBase>();

		ViewBase currentView = null;

		public const AnchorStyles TopEdge = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
		public const AnchorStyles ClientArea = TopEdge | AnchorStyles.Bottom;

		void OnViewChanged()
		{
			if (currentView == tabStrip.Current)
				return;

			if (currentView != null)
				currentView.Hide();

			if (null != (currentView = tabStrip.Current))
				currentView.Show();
		}

		internal Rectangle ViewBounds
		{
			get
			{
				return new Rectangle(1, tabStrip.Height, host.ClientSize.Width - 2,
					host.ClientSize.Height - tabStrip.Height - 1);
			}
		}

		public MultipleViewManager(Control host)
		{
			this.host = host;

			host.BackColor = SystemColors.AppWorkspace;
			host.Controls.Add(tabStrip);
			tabStrip.Anchor = TopEdge;
			tabStrip.Bounds = new Rectangle(0, 0, host.ClientSize.Width, 20);

			tabStrip.Changed += OnViewChanged;
			tabStrip.CurrentTabChanged += OnViewChanged;
		}

		public void Add(ViewBase v) { tabStrip.Add(v); }
		public void Select(ViewBase v) { tabStrip.Select(v); }
		public void Invalidate() { tabStrip.Invalidate(); }
		public ViewBase Current { get { return currentView; } }

		public void MoveNext() { tabStrip.SelectNext(); }
		public void MovePrevious() { tabStrip.SelectPrevious(); }
	}
}
