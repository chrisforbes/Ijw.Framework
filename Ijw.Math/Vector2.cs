using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ijw.Math
{
	using Math = System.Math;

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2
	{
		public float x, y;

		public static readonly Vector2 Zero = new Vector2(0, 0);
		public static readonly Vector2 UnitX = new Vector2(1, 0);
		public static readonly Vector2 UnitY = new Vector2(0, 1);

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public float Length
		{
			get { return (float)Math.Sqrt(LengthSquared); }
		}

		public float LengthSquared
		{
			get { return x * x + y * y; }
		}

		public Vector2 Normalize()
		{
			if (Length == 0)
				return Vector2.Zero;
			return (1 / Length) * this;
		}

		public static bool operator ==(Vector2 a, Vector2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=(Vector2 a, Vector2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		public override bool Equals(object obj)
		{
			return this == (Vector2)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", x, y);
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		public static Vector2 operator *(Vector2 a, float b) { return b * a; }
		public static Vector2 operator /(Vector2 a, float b) { return (1 / b) * a; }

		public static Vector2 operator *(float b, Vector2 a)
		{
			return new Vector2(b * a.x, b * a.y);
		}

		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		public static Vector2 Minimize(Vector2 a, Vector2 b)
		{
			return new Vector2(
				a.x < b.x ? a.x : b.x,
				a.y < b.y ? a.y : b.y);
		}

		public static Vector2 Maximize(Vector2 a, Vector2 b)
		{
			return new Vector2(
				a.x > b.x ? a.x : b.x,
				a.y > b.y ? a.y : b.y);
		}

		public Vector2 Constrain(Vector2 upper, Vector2 lower)
		{
			return Maximize(Minimize(this, upper), lower);
		}

		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		public static Vector2 CatmullRom(float t, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
		{
			float tt = t * t;
			float ttt = tt * t;

			return 0.5f * ((2 * v0) +
				(v2 - v0) * t +
				(2 * v0 - 5 * v1 + 4 * v2 - v3) * tt +
				(-v0 + 3 * v1 - 3 * v2 + v3) * ttt);
		}

		public static Vector2 CatmullRomDeriv(float t, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
		{
			float tt = t * t;

			return 0.5f * ((v2 - v0) +
				2 * (2 * v0 - 5 * v1 + 4 * v2 - v3) * t +
				3 * (-v0 + 3 * v1 - 3 * v2 + v3) * tt);
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(x);
			writer.Write(y);
		}

		public static Vector2 Read(BinaryReader reader)
		{
			return new Vector2(reader.ReadSingle(), reader.ReadSingle());
		}

		public System.Drawing.Point ToPoint()
		{
			return new System.Drawing.Point((int)x, (int)y);
		}

		public static Vector2 FromAngle(float angle)
		{
			return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		}
	}
}
