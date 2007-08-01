using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IjwFramework.Ui
{
	public class PanelHeader : Control
	{
		public PanelHeader(string text)
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			UpdateStyles();
			Text = text;
			Font = new Font("MS Sans Serif", 8.5f, FontStyle.Regular);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			ControlPaint.DrawBorder(e.Graphics,
				ClientRectangle,
				SystemColors.ButtonShadow,
				ButtonBorderStyle.Solid);

			int offset = (Height - Font.Height) / 2;
			e.Graphics.DrawString(Text, Font, Brushes.Black, offset, offset);
		}
	}
}
