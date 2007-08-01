using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	public class ToolStripGap : ToolStripSeparator
	{
		public const int DefaultWidth = 40;

		public ToolStripGap(int width)
		{
			AutoSize = false;
			Width = width;
		}

		public ToolStripGap()
			: this(DefaultWidth)
		{
		}

		protected override void OnPaint(PaintEventArgs e) { }
	}
}
