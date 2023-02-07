using System;
using Microsoft.Xna.Framework;

namespace NeonVectorShooter.Entities;

public class PlayerShip : Entity
{
	private static PlayerShip _instance;

	public static PlayerShip Instance => _instance ??= new PlayerShip();

	private const int CooldownFrames = 6;
	private int _cooldownRemaining = 0;
	private static Random rand = new Random();

	public PlayerShip()
	{
		Image = Art.Player;
		Position = GameRoot.ScreenSize / 2;
		Radius = 10;
	}

	public override void Update()
	{
		const float speed = 8;
		Velocity = speed * Input.GetMovementDirection();
		Position += Velocity;
		Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2);

		if (Velocity.LengthSquared() > 0)
			Orientation = Velocity.ToAngle();

		const float spread = 0.04f;
		const float bulletMagnitude = 11f;
		Vector2 aim = Input.GetAimDirection();
		if (aim.LengthSquared() > 0 && _cooldownRemaining <= 0)
		{
			_cooldownRemaining = CooldownFrames;
			float aimAngle = aim.ToAngle();
			Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

			float randomSpread = rand.NextFloat(-spread, spread);
			Vector2 vel = Math.FromPolar(aimAngle + randomSpread, bulletMagnitude);

			Vector2 offset = Vector2.Transform(new Vector2(25 -8), aimQuat);
			EntityManager.Add(new Bullet(Position + offset, vel));

			offset = Vector2.Transform(new Vector2(25, 8), aimQuat);
			EntityManager.Add(new Bullet(Position + offset, vel));
		}

		if (_cooldownRemaining > 0)
			_cooldownRemaining--;
	}
}
