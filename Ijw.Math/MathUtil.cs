using System;
using System.Collections.Generic;
using System.Text;

namespace Ijw.Math
{
	public static class MathUtil
	{
		public static float CatmullRom(float t, float v0, float v1, float v2, float v3)
		{
			float tt = t * t;
			float ttt = tt * t;

			return 0.5f * ((2 * v1) +
				(v2 - v0) * t +
				(2 * v0 - 5 * v1 + 4 * v2 - v3) * tt +
				(-v0 + 3 * v1 - 3 * v2 + v3) * ttt);
		}

		public static float CatmullRomDeriv(float t, float v0, float v1, float v2, float v3)
		{
			float tt = t * t;

			return 0.5f * ((v2 - v0) +
				2 * (2 * v0 - 5 * v1 + 4 * v2 - v3) * t +
				3 * (-v0 + 3 * v1 - 3 * v2 + v3) * tt);
		}
	}
}
