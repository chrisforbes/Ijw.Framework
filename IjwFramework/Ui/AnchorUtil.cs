using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IjwFramework.Ui
{
	public static class AnchorUtil
	{
		public const AnchorStyles TopEdge = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
		public const AnchorStyles ClientArea = TopEdge | AnchorStyles.Bottom;
		public const AnchorStyles BottomEdge = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
		public const AnchorStyles LeftEdge = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
		public const AnchorStyles RightEdge = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

		public const AnchorStyles TopLeftCorner = AnchorStyles.Left | AnchorStyles.Top;
		public const AnchorStyles TopRightCorner = AnchorStyles.Right | AnchorStyles.Top;
		public const AnchorStyles BottomLeftCorner = AnchorStyles.Left | AnchorStyles.Bottom;
		public const AnchorStyles BottomRightCorner = AnchorStyles.Bottom | AnchorStyles.Right;

		public const AnchorStyles HorizontalBand = AnchorStyles.Left | AnchorStyles.Right;
		public const AnchorStyles VerticalBand = AnchorStyles.Top | AnchorStyles.Bottom;
	}
}
