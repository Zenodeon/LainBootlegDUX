using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Xna.Framework
{
	public struct Vector2Int
	{
		public static Vector2Int zero = new Vector2Int(0, 0);
		public static Vector2Int one = new Vector2Int(1, 1);

		public Vector2 vector;

		public int x
		{
			get => (int)vector.x;
			set => vector.X = value;
		}

		public int y
		{
			get => (int)vector.y;
			set => vector.y = value;
		}

		public Vector2Int(float x, float y)
		{
			vector = new Vector2(x, y);
		}

		public Vector2Int(Vector2 vector2)
		{
			vector = vector2;
		}

		public static bool operator !=(Vector2Int vectorA, Vector2Int vectorB)
			=> !(vectorA == vectorB);

		public static bool operator ==(Vector2Int vectorA, Vector2Int vectorB)
			=> vectorA.vector == vectorB.vector;

        public override string ToString()
        {
			return $"{{ x : {x}, y : {y} }}";
        }
    }
}
