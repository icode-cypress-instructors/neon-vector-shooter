using Microsoft.Xna.Framework;

namespace NeonVectorShooter;

public static class Math
{
	public static Vector2 FromPolar(float angle, float magnitude) =>
		magnitude * new Vector2((float) System.Math.Cos(angle), (float) System.Math.Sin(angle));
}
