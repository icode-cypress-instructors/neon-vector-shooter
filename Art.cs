using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NeonVectorShooter;

public static class Art
{
	public static Texture2D Player { get; private set; }
	public static Texture2D Bullet { get; private set; }
	public static Texture2D Pointer { get; private set; }

	public static void Load(ContentManager content)
	{
		Player = content.Load<Texture2D>("Art/Player");
		Bullet = content.Load<Texture2D>("Art/Bullet");
		Pointer = content.Load<Texture2D>("Art/Pointer");
	}
}
