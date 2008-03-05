using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ijw.Math
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4
	{
		public float x, y, z, w;

		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Vector4(Vector3 v, float w) : this(v.x, v.y, v.z, w) { }

		public static Vector4 Read(BinaryReader reader)
		{
			return new Vector4(
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle());
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
			writer.Write(w);
		}

		public static Vector4 operator *(float a, Vector4 b)
		{
			return new Vector4(a * b.x, a * b.y, a * b.z, a * b.w);
		}

		public static Vector4 operator *(Vector4 b, float a)
		{
			return new Vector4(a * b.x, a * b.y, a * b.z, a * b.w);
		}

		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		public static Vector4 operator *(Vector4 a, Matrix m)
		{
			Vector4 right = new Vector4(m.RightVector, m.M14);
			Vector4 up = new Vector4(m.UpVector, m.M24);
			Vector4 forward = new Vector4(m.ForwardVector, m.M34);
			Vector4 trans = new Vector4(m.TranslationVector, m.M44);

			return a.x * right + a.y * up + a.z * forward + a.w * trans;
		}

		public static Vector4 CatmullRomDeriv(float t, Vector4 v0, Vector4 v1, Vector4 v2, Vector4 v3)
		{
			float tt = t * t;

			return 0.5f * ((v2 - v0) +
				2 * (2 * v0 - 5 * v1 + 4 * v2 - v3) * t +
				3 * (-v0 + 3 * v1 - 3 * v2 + v3) * tt);
		}

		public static Vector4 CatmullRom(float t, Vector4 v0, Vector4 v1, Vector4 v2, Vector4 v3)
		{
			float tt = t * t;
			float ttt = tt * t;

			return 0.5f * ((2 * v0) +
				(v2 - v0) * t +
				(2 * v0 - 5 * v1 + 4 * v2 - v3) * tt +
				(-v0 + 3 * v1 - 3 * v2 + v3) * ttt);
		}
	}
}
