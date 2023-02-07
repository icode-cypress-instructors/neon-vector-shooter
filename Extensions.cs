using System;
using Microsoft.Xna.Framework;

namespace NeonVectorShooter;

public static class Extensions
{
	public static Vector2 To(this Vector2 source, Vector2 destination) =>
		destination - source;

	public static float ToAngle(this Vector2 vector) =>
		(float) System.Math.Atan2(vector.Y, vector.X);

	public static float NextFloat(this Random rand, float minValue, float maxValue) =>
		(float) rand.NextDouble() * (maxValue - minValue) + minValue;
}
