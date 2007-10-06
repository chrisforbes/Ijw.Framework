using System;
using System.Collections.Generic;
using System.Text;

namespace Ijw.Math
{
	public struct Matrix3
	{
		public float xx, yx, zx;
		public float xy, yy, zy;
		public float xz, yz, zz;

		public Matrix3(Quaternion q)
		{
			float xx2 = q.x * q.x * 2;
			float yy2 = q.y * q.y * 2;
			float zz2 = q.z * q.z * 2;
			float xy2 = q.x * q.y * 2;
			float zw2 = q.z * q.w * 2;
			float xz2 = q.x * q.z * 2;
			float yw2 = q.y * q.w * 2;
			float yz2 = q.y * q.z * 2;
			float xw2 = q.x * q.w * 2;

			xx = 1 - yy2 - zz2; xy = xy2 + zw2; xz = xz2 - yw2;
			yx = xy2 - zw2; yy = 1 - xx2 - zz2; yz = yz2 + xw2;
			zx = xz2 + yw2; zy = yz2 - xw2; zz = 1 - xx2 - yy2;
		}

		public static Matrix3 operator * (float w, Matrix3 m)
		{
			Matrix3 result = new Matrix3();
			result.xx = m.xx * w;
			result.xy = m.xy * w;
			result.xz = m.xz * w;
			result.yx = m.yx * w;
			result.yy = m.yy * w;
			result.yz = m.yz * w;
			result.zx = m.zx * w;
			result.zy = m.zy * w;
			result.zz = m.zz * w;

			return result;
		}

		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			Matrix3 result;
			result.xx = a.xx + b.xx;
			result.xy = a.xy + b.xy;
			result.xz = a.xz + b.xz;
			result.yx = a.yx + b.yx;
			result.yy = a.yy + b.yy;
			result.yz = a.yz + b.yz;
			result.zx = a.zx + b.zx;
			result.zy = a.zy + b.zy;
			result.zz = a.zz + b.zz;
			return result;
		}

		public float Determinant
		{
			get
			{
				return xx * (yy * zz - yz * zy) -
					xy * (yx * zz - zx * yz) +
					xz * (yx * zy - zx * yy);
			}
		}

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			Matrix3 result;

			result.xx = b.xx * a.xx + b.xy * a.yx + b.xz * a.zx;
			result.yx = b.yx * a.xx + b.yy * a.yx + b.yz * a.zx;
			result.zx = b.zx * a.xx + b.zy * a.yx + b.zz * a.zx;

			result.xy = b.xx * a.xy + b.xy * a.yy + b.xz * a.zy;
			result.yy = b.yx * a.xy + b.yy * a.yy + b.yz * a.zy;
			result.zy = b.zx * a.xy + b.zy * a.yy + b.zz * a.zy;

			result.xz = b.xx * a.xz + b.xy * a.yz + b.xz * a.zz;
			result.yz = b.yx * a.xz + b.yy * a.yz + b.yz * a.zz;
			result.zz = b.zx * a.xz + b.zy * a.yz + b.zz * a.zz;

			return result;
		}
	}
}
