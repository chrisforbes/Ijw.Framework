using System;
using System.Collections.Generic;
using System.Text;

namespace Ijw.Math
{
	using Math = System.Math;

	public struct Quaternion
	{
		public float x, y, z, w;

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
			return new Quaternion(
				a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
				a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
				a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
				a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);
		}

		public static Quaternion operator *(Quaternion a, Vector3 b)
		{
			return new Quaternion(
				a.w * b.x + a.y * b.z - a.z * b.y,
				a.w * b.y - a.x * b.z + a.z * b.x,
				a.w * b.z + a.x * b.y - a.y * b.x,
				- a.x * b.x - a.y * b.y - a.z * b.z);
		}

		public float Norm
		{
			get { return x * x + y * y + z * z + w * w; }
		}

		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static Quaternion Blend(float t, Quaternion a, Quaternion b)
		{
			float norm = Quaternion.Dot(a, b);
			bool flip = norm < 0;
			if (flip) norm = -norm;

			float inv_t;
			if (1 - norm < 1e-6f)
				inv_t = 1 - t;
			else
			{
				float theta = (float)Math.Acos(norm);
				float s = (1 / (float)Math.Sin(theta));
				inv_t = (float)Math.Sin((1 - t) * theta) * s;
			}

			if (flip)
				t = -t;

			return new Quaternion(
				inv_t * a.x + t * b.x,
				inv_t * a.y + t * b.y,
				inv_t * a.z + t * b.z,
				inv_t * a.w + t * b.w);
		}

		public Quaternion Conjugate
		{
			get { return new Quaternion(-x, -y, -z, w); }
		}

		public Quaternion Inverse
		{
			get
			{
				float norm = Norm;
				if (norm == 0.0f)
					return this.Conjugate;

				float inorm = 1 / norm;
				return new Quaternion(x * inorm, y * inorm, z * inorm, w * inorm);
			}
		}

		public Vector3 Xyz { get { return new Vector3(x, y, z); } }

		public Matrix ToMatrix()
		{
			float xx2 = x * x * 2;
			float yy2 = y * y * 2;
			float zz2 = z * z * 2;
			float xy2 = x * y * 2;
			float zw2 = z * w * 2;
			float xz2 = x * z * 2;
			float yw2 = y * w * 2;
			float yz2 = y * z * 2;
			float xw2 = x * w * 2;

			return new Matrix(
				1 - yy2 - zz2, xy2 - zw2, xz2 + yw2, 0,
				xy2 + zw2, 1 - xx2 - zz2, yz2 - xw2, 0,
				xz2 - yw2, yz2 + xw2, 1 - xx2 - yy2, 0,
				0, 0, 0, 1);
		}
	}
}
