using Bellona_Console.MemoryReading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Other {
    public class Vector3 {

        /// <summary>
        /// A <see cref="Vector3"/> with all of its components set to zero.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3();

        /// <summary>
        /// The X unit <see cref="Vector3"/> (1, 0, 0).
        /// </summary>
        public static readonly Vector3 UnitX = new Vector3(1.0f, 0.0f, 0.0f);

        /// <summary>
        /// The Y unit <see cref="Vector3"/> (0, 1, 0).
        /// </summary>
        public static readonly Vector3 UnitY = new Vector3(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// The Z unit <see cref="Vector3"/> (0, 0, 1).
        /// </summary>
        public static readonly Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1.0f);

        /// <summary>
        /// A <see cref="Vector3"/> with all of its components set to one.
        /// </summary>
        public static readonly Vector3 One = new Vector3(1.0f, 1.0f, 1.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating up (0, 1, 0).
        /// </summary>
        public static readonly Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating down (0, -1, 0).
        /// </summary>
        public static readonly Vector3 Down = new Vector3(0.0f, -1.0f, 0.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating left (-1, 0, 0).
        /// </summary>
        public static readonly Vector3 Left = new Vector3(-1.0f, 0.0f, 0.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating right (1, 0, 0).
        /// </summary>
        public static readonly Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating forward in a right-handed coordinate system (0, 0, -1).
        /// </summary>
        public static readonly Vector3 ForwardRH = new Vector3(0.0f, 0.0f, -1.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating forward in a left-handed coordinate system (0, 0, 1).
        /// </summary>
        public static readonly Vector3 ForwardLH = new Vector3(0.0f, 0.0f, 1.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating backward in a right-handed coordinate system (0, 0, 1).
        /// </summary>
        public static readonly Vector3 BackwardRH = new Vector3(0.0f, 0.0f, 1.0f);

        /// <summary>
        /// A unit <see cref="Vector3"/> designating backward in a left-handed coordinate system (0, 0, -1).
        /// </summary>
        public static readonly Vector3 BackwardLH = new Vector3(0.0f, 0.0f, -1.0f);

        public static readonly float BehindScale = -3;


        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public float X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public float Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        public Vector3() {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Vector3(float value) {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        /// <param name="z">Initial value for the Z component of the vector.</param>
        public Vector3(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the X, Y, and Z components of the vector. This must be an array with three elements.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than three elements.</exception>
        public Vector3(float[] values) {
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length != 3)
                throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Vector3.");

            X = values[0];
            Y = values[1];
            Z = values[2];
        }
        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result) {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            float z = value1.Z - value2.Z;

            result = (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public static float Distance(Vector3 value1, Vector3 value2) {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            //float z = value1.Z - value2.Z;
            float z = 0;
            
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public static Vector3 Behindtarget(GameObject target) {
            return new Vector3(
                target.Unit.Position.X + (float)Math.Cos(target.Unit.Rotation) * BehindScale,
                target.Unit.Position.Y + (float)Math.Sin(target.Unit.Rotation) * BehindScale,
                target.Unit.Position.Z);

        }
    }
}
