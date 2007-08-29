using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	public class ScrollableView : Control
	{
		readonly EmbeddedScrollBar horizontalScrollBar, verticalScrollBar;

		public ScrollableView( bool horizontal, bool vertical )
		{
			horizontalScrollBar = horizontal ? new EmbeddedScrollBar(this, ScrollOrientation.HorizontalScroll) : null;
			verticalScrollBar = vertical ? new EmbeddedScrollBar(this, ScrollOrientation.VerticalScroll) : null;
		}

		public ScrollableView()
			: this(true, true)
		{
		}

		public EmbeddedScrollBar HorizontalScroll { get { return horizontalScrollBar; } }
		public EmbeddedScrollBar VerticalScroll { get { return verticalScrollBar; } }

		protected override CreateParams CreateParams
		{
			get
			{
				const int WS_VSCROLL = 0x00200000;
				const int WS_HSCROLL = 0x00100000;

				CreateParams p = base.CreateParams;

				if (horizontalScrollBar != null)
					p.Style |= WS_HSCROLL;
				if (verticalScrollBar != null)
					p.Style |= WS_VSCROLL;

				return p;
			}
		}

		protected override void WndProc(ref Message m)
		{
			const int WM_HSCROLL = 0x114;
			const int WM_VSCROLL = 0x115;

			switch (m.Msg)
			{
				case WM_HSCROLL:
					{
						if (horizontalScrollBar != null)
							horizontalScrollBar.HandleMessage(m);
						break;
					}

				case WM_VSCROLL:
					{
						if (verticalScrollBar != null)
							verticalScrollBar.HandleMessage(m);
						break;
					}
			}

			base.WndProc(ref m);
		}

		protected void ScrollContent(int dx, int dy)
		{
			ScrollWindowEx(Handle,
				dx, dy, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, swInvalidate);

			UpdateWindow(Handle);
		}

		[DllImport("user32.dll")]
		static extern int ScrollWindowEx(
			IntPtr handle,
			int dx,
			int dy,
			IntPtr rect,
			IntPtr clip,
			IntPtr updatedRegion,
			IntPtr updatedRect,
			uint flags);

		[DllImport("user32.dll")]
		static extern int UpdateWindow(IntPtr handle);

		const uint swInvalidate = 0x2;
	}
}
