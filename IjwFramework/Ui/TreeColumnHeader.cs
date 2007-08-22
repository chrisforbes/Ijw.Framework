using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IjwFramework.Ui
{
	public class TreeColumnHeader : Control
	{
		readonly ColumnCollection src;

		public TreeColumnHeader(ColumnCollection src)
		{
			this.src = src;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			src.WidthUpdatedHandler(ClientSize.Width);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int a2 = Height / 4;
			int a1 = 3 * a2;

			e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
			using (Brush b = new LinearGradientBrush(new Point(0, a1 - 1), 
				new Point(0, Height), Color.White, SystemColors.ButtonFace))
				e.Graphics.FillRectangle(b, 0, a1, Width, a2);

			Painter painter = new Painter(e.Graphics, new Rectangle(new Point(), ClientSize));
			src.RenderCustom(painter, null, RenderColumnHeader);

			e.Graphics.DrawLine(Pens.Black, 0, Height - 1, Width, Height - 1);
		}

		void RenderColumnHeader(IColumn c, Painter p, Node n)
		{
			p.SetPosition(c.Left);
			if (c.Left > 0)
				p.DrawSeparatorLine(Pens.Black);
			p.Pad(4);
			p.DrawString(c.Name, Font, Brushes.Black, 3, c.Left + c.Width);
		}
	}
}
