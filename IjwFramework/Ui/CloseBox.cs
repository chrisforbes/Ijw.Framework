using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using IjwFramework.Delegates;

namespace IjwFramework.Ui
{
	public class CloseBox
	{
		Control host;
		bool hover, visible;

		public bool Visible
		{
			get { return visible; }
			set
			{
				if (value != visible)
				{
					visible = value;
					host.Invalidate();
				}
			}
		}

		Rectangle bounds
		{
			get { return new Rectangle(host.Width - 18, 2, 16, host.Height - 4); }
		}

		public event Action Clicked = delegate { };

		public CloseBox(Control host)
		{
			this.host = host;

			host.Resize += delegate { host.Invalidate(); };

			host.MouseLeave += delegate
			{
				hover = false;
				host.Invalidate();
			};

			host.MouseMove += delegate(object sender, MouseEventArgs e)
			{
				bool h = bounds.Contains(e.Location);
				
				if (hover != h)
				{
					hover = h;
					host.Invalidate();
				}
			};

			host.MouseUp += delegate(object sender, MouseEventArgs e)
			{
				if (bounds.Contains(e.Location) && e.Button == MouseButtons.Left)
					Clicked();
			};
		}

		public void Paint(Graphics g)
		{
			if (!Visible) return;

			if (hover)
			{
				g.FillRectangle(SystemBrushes.ButtonHighlight, bounds);
				g.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			}

			g.DrawString("\x72", new Font("Marlett", 8.5f), Brushes.Black, new PointF(bounds.X + 1, bounds.Y + 3));
		}
	}
}
