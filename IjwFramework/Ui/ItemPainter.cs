using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	class ItemPainter
	{
		public Graphics g;
		static readonly Brush selectedBrush = new SolidBrush(Color.FromArgb(0xee, 0xee, 0xff));
		protected ImageProvider provider = new ImageProvider("");

		public ItemPainter(ImageProvider provider)
		{
			this.provider = provider;
		}

		public void PaintItem(Node n, Rectangle bounds, bool selected)
		{
			PaintBackground(bounds, selected);
			PaintExpander(bounds, n);
		}

		void PaintBackground(Rectangle bounds, bool selected)
		{
			g.FillRectangle(selected ? selectedBrush : SystemBrushes.Window,
				bounds);
		}

		void PaintExpander(Rectangle bounds, Node n)
		{
			if (n.IsLeaf)
				return;

			Image image = provider.GetImage(n.Expanded ? "collapse" : "expand");
			g.DrawImage(image, bounds.Left + bounds.Height * (n.Depth - 1), bounds.Top);
		}
	}
}
