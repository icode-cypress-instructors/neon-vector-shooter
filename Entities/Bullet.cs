using Microsoft.Xna.Framework;

namespace NeonVectorShooter.Entities;

public class Bullet : Entity
{
	public Bullet(Vector2 position, Vector2 velocity)
	{
		Image = Art.Bullet;
		Position = position;
		Velocity = velocity;
		Orientation = Velocity.ToAngle();
		Radius = 0;
	}
	public override void Update()
	{
		if (Velocity.LengthSquared() > 0)
			Orientation = Velocity.ToAngle();

		Position += Velocity;

		// delete bullets tht go off-screen
		if (!GameRoot.Viewport.Bounds.Contains(Position.ToPoint()))
			IsExpired = true;
	}
}
