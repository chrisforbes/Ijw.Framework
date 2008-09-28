using System;
using System.Collections.Generic;
using System.Text;

namespace Ijw.Math
{
	public struct Ray
	{
		public Vector3 Origin;
		public Vector3 Direction;

		public Ray(Vector3 origin, Vector3 direction)
		{
			Origin = origin;
			Direction = direction;
		}

		public static Ray operator *(Ray r, Matrix m)
		{
			return new Ray(
				r.Origin.TransformAsCoordinate(m),
				r.Direction.TransformAsNormal(m));
		}
	}
}
