using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IjwFramework.Ui.Tree
{
	public class Painter
	{
		readonly Graphics g;
		readonly Rectangle bounds;
		int x;

		public StringAlignment Alignment
		{
			get { return sf.Alignment; }
			set { sf.Alignment = value; }
		}
		
		readonly StringFormat sf = StringFormat.GenericTypographic;

		public Painter(Graphics g, Rectangle bounds)
		{
			this.g = g;
			this.bounds = bounds;
		}

		public void Pad(int pixels)
		{
			x += pixels;
		}

		public void SetPosition(int pixels)
		{
			if (pixels < 0)
				x = bounds.Width + pixels;
			else
				x = pixels;
		}

		public void DrawString(string s, Font f, Brush b)
		{
			DrawString(s, f, b, 0);
		}

		public void DrawString(string s, Font f, Brush b, int yofs)
		{
			if (x >= bounds.Width)
				return;

			int width = (int)g.MeasureString(s, f, bounds.Width, sf).Width;
			g.DrawString(s, f, b, bounds.Left + x, bounds.Top + yofs, sf);
			x += width;
		}

		public void DrawImage(Image i)
		{
			if (x >= bounds.Width)
				return;

			g.DrawImage(i, bounds.Left + x, bounds.Top);
			x += i.Width;
		}

		public void DrawSeparatorLine(Pen p)
		{
			g.DrawLine(p, bounds.Left + x, bounds.Top, bounds.Left + x, bounds.Bottom);
		}
	}
}
