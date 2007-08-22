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

			sf.Trimming = StringTrimming.EllipsisCharacter;
			sf.FormatFlags |= StringFormatFlags.LineLimit;
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
			DrawString(s, f, b, 0, bounds.Right);
		}

		public void DrawString(string s, Font f, Brush b, int yofs, int maxx)
		{
			if (x >= bounds.Width || x >= maxx)
				return;

			if (Alignment == StringAlignment.Near)
			{
				int width = (int)g.MeasureString(s, f, bounds.Width, sf).Width;
				g.DrawString(s, f, b, new Rectangle( bounds.Left + x, bounds.Top + yofs, maxx - x, bounds.Height ), sf);
				x += width;
			}
			else
				g.DrawString(s, f, b, bounds.Left + x, bounds.Top + yofs, sf);
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
