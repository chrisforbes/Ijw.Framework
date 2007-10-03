using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ijw.Math
{
	using Math = System.Math;

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3
	{
		public float x, y, z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static readonly Vector3 Zero = new Vector3(0, 0, 0);
		public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
		public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
		public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

		public float LengthSquared
		{
			get { return x * x + y * y + z * z; }
		}

		public float Length
		{
			get { return (float)Math.Sqrt(LengthSquared); }
		}

		public Vector3 Normalize()
		{
			if (Length == 0)
				return Vector3.Zero;

			return (1 / Length) * this;
		}

		public static Vector3 operator *(float a, Vector3 b)
		{
			return new Vector3(a * b.x, a * b.y, a * b.z);
		}

		public static Vector3 operator *(Vector3 a, float b) { return b * a; }

		public static Vector3 operator /(Vector3 a, float b) { return (1 / b) * a; }

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator *(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3 operator /(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static Vector3 Cross(Vector3 a, Vector3 b)
		{
			return new Vector3(
				a.y * b.z - a.z * b.y,
				a.z * b.x - a.x * b.z,
				a.x * b.y - a.y * b.x);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public static bool operator ==(Vector3 a, Vector3 b)
		{
			return a.x == b.x && a.y == b.y && a.z == b.z;
		}

		public static bool operator !=(Vector3 a, Vector3 b)
		{
			return a.x != b.x || a.y != b.y || a.z != b.z;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			return (Vector3)obj == this;
		}

		public static Vector3 Minimize(Vector3 a, Vector3 b)
		{
			return new Vector3(
				a.x < b.x ? a.x : b.x,
				a.y < b.y ? a.y : b.y,
				a.z < b.z ? a.z : b.z);
		}

		public static Vector3 Maximize(Vector3 a, Vector3 b)
		{
			return new Vector3(
				a.x > b.x ? a.x : b.x,
				a.y > b.y ? a.y : b.y,
				a.z > b.z ? a.z : b.z);
		}

		public Vector3 Constrain(Vector3 upper, Vector3 lower)
		{
			return Maximize(Minimize(this, upper), lower);
		}

		public Vector3 Project(Viewport viewport, Matrix projectionMatrix, Matrix viewMatrix)
		{
			return Project(viewport, projectionMatrix, viewMatrix, Matrix.Identity);
		}

		public Vector3 Project(Viewport viewport, Matrix projectionMatrix, Matrix viewMatrix, Matrix worldMatrix)
		{
			Vector4 src = new Vector4(this, 1);
			Vector4 dest = src * (worldMatrix * viewMatrix * projectionMatrix);

			Vector3 p = new Vector3(dest.x, dest.y, dest.z) / dest.w;
			p.x = 0.5f * p.x + 0.5f;
			p.y = 0.5f * p.y + 0.5f;
			return new Vector3(viewport.Width, viewport.Height, 1) * p;
		}

		public Vector3 TransformAsCoordinate(Matrix matrix)
		{
			return TransformAsNormal(matrix) + matrix.TranslationVector;
		}

		public Vector3 TransformAsNormal(Matrix matrix)
		{
			return x * matrix.RightVector + y * matrix.UpVector + z * matrix.ForwardVector;
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
		}

		public static Vector3 Read(BinaryReader reader)
		{
			return new Vector3(
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle()
			);
		}

		public static Vector3 Random(Random randomStream)
		{
			return new Vector3(
				2 * (float)randomStream.NextDouble() - 1,
				2 * (float)randomStream.NextDouble() - 1,
				2 * (float)randomStream.NextDouble() - 1);
		}
	}
}
