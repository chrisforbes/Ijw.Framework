using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Ijw.Math
{
	using Math = System.Math;

	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix
	{
		public float M11, M12, M13, M14;
		public float M21, M22, M23, M24;
		public float M31, M32, M33, M34;
		public float M41, M42, M43, M44;

		public Vector3 TranslationVector
		{
			get { return new Vector3(M41, M42, M43); }
			set { M41 = value.x; M42 = value.y; M43 = value.z; }
		}

		public static Matrix FromBasisVectors(Vector3 rightVector, Vector3 upVector,
			Vector3 forwardVector, Vector3 translationVector)
		{
			Matrix m = Matrix.Identity;
			m.RightVector = rightVector;
			m.UpVector = upVector;
			m.ForwardVector = forwardVector;
			m.TranslationVector = translationVector;

			return m;
		}

		public static Matrix Translate(Vector3 translate)
		{
			Matrix m = Matrix.Identity;
			m.M41 = translate.x;
			m.M42 = translate.y;
			m.M43 = translate.z;
			return m;
		}

		public static Matrix Scale(float scale)
		{
			Matrix m = Matrix.Identity;
			m.M11 = scale;
			m.M22 = scale;
			m.M33 = scale;
			return m;
		}

		public static Matrix Scale(float x, float y, float z)
		{
			Matrix m = Matrix.Identity;
			m.M11 = x;
			m.M22 = y;
			m.M33 = z;
			return m;
		}

		public Vector3 ForwardVector
		{
			get { return new Vector3(M31, M32, M33); }
			set { M31 = value.x; M32 = value.y; M33 = value.z; }
		}

		public Vector3 UpVector
		{
			get { return new Vector3(M21, M22, M23); }
			set { M21 = value.x; M22 = value.y; M23 = value.z; }
		}

		public Vector3 RightVector
		{
			get { return new Vector3(M11, M12, M13); }
			set { M11 = value.x; M12 = value.y; M13 = value.z; }
		}

		public static readonly Matrix Identity = new Matrix(
			1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1);

		public Matrix(
			float m11, float m12, float m13, float m14,
			float m21, float m22, float m23, float m24,
			float m31, float m32, float m33, float m34,
			float m41, float m42, float m43, float m44)
		{
			M11 = m11; M12 = m12; M13 = m13; M14 = m14;
			M21 = m21; M22 = m22; M23 = m23; M24 = m24;
			M31 = m31; M32 = m32; M33 = m33; M34 = m34;
			M41 = m41; M42 = m42; M43 = m43; M44 = m44;
		}

		public static Matrix LookAt(Vector3 eye, Vector3 target, Vector3 up)
		{
			Vector3 z = (target - eye).Normalize();
			Vector3 x = Vector3.Cross(up, z).Normalize();
			Vector3 y = Vector3.Cross(z, x);

			return new Matrix(
				x.x, y.x, z.x, 0,
				x.y, y.y, z.y, 0,
				x.z, y.z, z.z, 0,
				-Vector3.Dot(x, eye), -Vector3.Dot(y, eye), -Vector3.Dot(z, eye), 1);
		}

		public static Matrix Perspective(float fov, float aspect, float nearClip, float farClip)
		{
			float ys = 1.0f / (float)Math.Tan(fov / 2);
			float xs = ys / aspect;
			float o33 = farClip / (farClip - nearClip);
			float o43 = -nearClip * farClip / (farClip - nearClip);

			return new Matrix(
				xs, 0, 0, 0,
				0, ys, 0, 0,
				0, 0, o33, 1,
				0, 0, o43, 0);
		}

		public static Matrix InversePerspective(float fov, float aspect, float nearClip, float farClip)
		{
			float ys = 1.0f / (float)Math.Tan(fov / 2);
			float xs = ys / aspect;
			float o33 = farClip / (farClip - nearClip);
			float o43 = -nearClip * farClip / (farClip - nearClip);

			return new Matrix(
				1 / xs, 0, 0, 0,
				0, 1 / ys, 0, 0,
				0, 0, 0, 1 / o43,
				0, 0, 1, -o33 / o43);
		}

		public static Matrix operator *(Matrix a, Matrix b)
		{
			Vector4 a1 = new Vector4(a.M11, a.M12, a.M13, a.M14);
			Vector4 a2 = new Vector4(a.M21, a.M22, a.M23, a.M24);
			Vector4 a3 = new Vector4(a.M31, a.M32, a.M33, a.M34);
			Vector4 a4 = new Vector4(a.M41, a.M42, a.M43, a.M44);

			return new Matrix( a1 * b, a2 * b, a3 * b, a4 * b );
		}

		public static Matrix Scaling(float scaleFactor)
		{
			return new Matrix(
				scaleFactor, 0, 0, 0,
				0, scaleFactor, 0, 0,
				0, 0, scaleFactor, 0,
				0, 0, 0, 1);
		}

		public static bool operator !=(Matrix a, Matrix b)
		{
			return
				a.M11 != b.M11 || a.M12 != b.M12 || a.M13 != b.M13 || a.M14 != b.M14 ||
				a.M21 != b.M21 || a.M22 != b.M22 || a.M23 != b.M23 || a.M24 != b.M24 ||
				a.M31 != b.M31 || a.M32 != b.M32 || a.M33 != b.M33 || a.M34 != b.M34 ||
				a.M41 != b.M41 || a.M42 != b.M42 || a.M43 != b.M43 || a.M44 != b.M44;
		}

		public static bool operator ==(Matrix a, Matrix b)
		{
			return
				a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 && a.M14 == b.M14 &&
				a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 && a.M24 == b.M24 &&
				a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33 && a.M34 == b.M34 &&
				a.M41 == b.M41 && a.M42 == b.M42 && a.M43 == b.M43 && a.M44 == b.M44;
		}

		public static Matrix RotationX(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);

			return new Matrix(
				1, 0, 0, 0,
				0, c, s, 0,
				0, -s, c, 0,
				0, 0, 0, 1);
		}

		public static Matrix RotationY(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);

			return new Matrix(
				c, 0, -s, 0,
				0, 1, 0, 0,
				s, 0, c, 0,
				0, 0, 0, 1);
		}

		public static Matrix RotationZ(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);

			return new Matrix(
				c, s, 0, 0,
				-s, c, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1);
		}

		public Matrix Transpose()
		{
			return new Matrix(
				M11, M21, M31, M41,
				M12, M22, M32, M42,
				M13, M23, M33, M43,
				M14, M24, M34, M44);
		}

		public override bool Equals(object obj)
		{
			return this == (Matrix)obj;
		}

		public override int GetHashCode()
		{
			return M11.GetHashCode() ^ M22.GetHashCode() ^ M33.GetHashCode() ^ M44.GetHashCode();
		}

		public static Matrix Read(BinaryReader reader)
		{
			return new Matrix(
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),

				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),

				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),

				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle());
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(M11); writer.Write(M12); writer.Write(M13); writer.Write(M14);
			writer.Write(M21); writer.Write(M22); writer.Write(M23); writer.Write(M24);
			writer.Write(M31); writer.Write(M32); writer.Write(M33); writer.Write(M34);
			writer.Write(M41); writer.Write(M42); writer.Write(M43); writer.Write(M44);
		}

		public Matrix(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
		{
			M11 = a.x; M12 = a.y; M13 = a.z; M14 = a.w;
			M21 = b.x; M22 = b.y; M23 = b.z; M24 = b.w;
			M31 = c.x; M32 = c.y; M33 = c.z; M34 = c.w;
			M41 = d.x; M42 = d.y; M43 = d.z; M44 = d.w;
		}
	}
}
