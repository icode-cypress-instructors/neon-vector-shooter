using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity
{
	protected Texture2D Image;
	// The tint of the image. This will also allow us to change the transparency.
	protected Color Color = Color.White;

	public Vector2 Position, Velocity;
	public float Orientation;
	public float Radius = 20;	// used for circular collision detection
	// TODO: instead of using IsExpired, add entities to a destruction queue
	public bool IsExpired;		// true if the entity was destroyed and should be deleted.

	public Vector2 Size => Image == null ? Vector2.Zero : new Vector2(Image.Width, Image.Height);

	public abstract void Update();

	public virtual void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Image, Position, null, Color, Orientation, Size / 2f, 1f, 0, 0);
	}
}
