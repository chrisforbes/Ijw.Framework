using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace IjwFramework.Ui
{
	public class Scroller
	{
		Control host;

		public Scroller(Control host)
		{
			this.host = host;
		}

		public void ScrollRectangle(Rectangle r, int dx, int dy)
		{
			unsafe
			{
				Rect rr = new Rect(r);
				ScrollWindowEx(host.Handle, dx, dy, &rr,
					IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, swErase | swInvalidate);
			}
		}

		[DllImport("user32.dll")]
		static extern unsafe int ScrollWindowEx(IntPtr window, int dx, int dy,
			[In] Rect * scrollRect,
			IntPtr unused1,
			IntPtr unused2,
			IntPtr unused3,
			uint flags);

		const int swErase = 0x4;
		const int swInvalidate = 0x2;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct Rect
	{
		public int left, top, right, bottom;
		public Rect(Rectangle r)
		{
			left = r.Left;
			top = r.Top;
			right = r.Right;
			bottom = r.Bottom;
		}
	}
}
