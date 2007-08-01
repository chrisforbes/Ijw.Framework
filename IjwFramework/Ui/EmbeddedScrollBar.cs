using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IjwFramework.Ui
{
	public class EmbeddedScrollBar
	{
		ScrollableView host;
		ScrollOrientation type;
		ScrollInfo info = new ScrollInfo();

		public EmbeddedScrollBar(ScrollableView host, ScrollOrientation type)
		{
			this.host = host;
			this.type = type;

			info.size = Marshal.SizeOf(typeof(ScrollInfo));
			info.min = 0;
			info.max = 100;
			info.position = 0;
			info.trackPosition = 0;
			info.page = 10;
			info.flags = ScrollBarFlags.All;
		}

		enum ScrollBarFlags : int
		{
			Range = 1,
			Page = 2,
			Position = 4,
			DisableNoScroll = 8,
			TrackPos = 16,

			All = Range | Page | Position | TrackPos | DisableNoScroll,
		}

		[StructLayout(LayoutKind.Sequential)]
		struct ScrollInfo
		{
			public int size;
			public ScrollBarFlags flags;
			public int min, max, page, position, trackPosition;
		}

		[DllImport("user32.dll")]
		static extern int SetScrollInfo(IntPtr window,
			ScrollOrientation bar,
			[In] ref ScrollInfo info,
			bool redraw);

		[DllImport("user32.dll")]
		static extern int GetScrollInfo(IntPtr window,
			ScrollOrientation bar,
			out ScrollInfo info);

		void Update()
		{
			info.flags = ScrollBarFlags.All;
			SetScrollInfo(host.Handle, type, ref info, true);
		}

		public int Min
		{
			get { return info.min; }
			set
			{
				info.min = value;
				Update();
			}
		}

		public int Maximum
		{
			get { return info.max; }
			set
			{
				info.max = value;
				Update();
			}
		}

		public int Position
		{
			get { return info.position; }
			set
			{
				info.position = value;
				Update();
			}
		}

		public int PageSize
		{
			get { return info.page; }
			set
			{
				info.page = value;
				Update();
			}
		}

		public int TrackPosition
		{
			get { return info.trackPosition; }
			set
			{
				info.trackPosition = value;
				Update();
			}
		}

		void SynchronizeFromScrollbar()
		{
			info.flags = ScrollBarFlags.All;
			GetScrollInfo(host.Handle, type, out info);
		}

		public void HandleMessage(Message m)
		{
			int oldPosition = info.position;

			SynchronizeFromScrollbar();

			ScrollEventType sbMessage = (ScrollEventType)(short)m.WParam.ToInt32();
			switch (sbMessage)
			{
				case ScrollEventType.SmallDecrement:
					--Position;
					break;

				case ScrollEventType.SmallIncrement:
					++Position;
					break;

				case ScrollEventType.LargeDecrement:
					Position -= PageSize;
					break;

				case ScrollEventType.LargeIncrement:
					Position += PageSize;
					break;

				case ScrollEventType.First:
					Position = Min;
					break;

				case ScrollEventType.Last:
					Position = Maximum;
					break;

				case ScrollEventType.ThumbTrack:
					Position = info.trackPosition;
					break;

				case ScrollEventType.ThumbPosition:
					Position = info.trackPosition;
					break;
			}

			SynchronizeFromScrollbar();

			if (oldPosition != info.position)
				Scroll(this, new ScrollEventArgs(sbMessage, oldPosition, info.position));
		}

		public event EventHandler<ScrollEventArgs> Scroll = delegate { };
	}
}
