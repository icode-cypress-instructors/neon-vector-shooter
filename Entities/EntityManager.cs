using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

static class EntityManager
{
	private static List<Entity> _entities = new List<Entity>();

	private static bool _isUpdating;
	private static readonly List<Entity> AddedEntities = new List<Entity>();

	public static int Count => _entities.Count;

	public static void Add(Entity entity)
	{
		if (!_isUpdating)
			_entities.Add(entity);
		else
			AddedEntities.Add(entity);
	}

	public static void Update()
	{
		_isUpdating = true;
		foreach (var entity in _entities)
			entity.Update();
		_isUpdating = false;
		foreach (var entity in AddedEntities)
			_entities.Add(entity);
		AddedEntities.Clear();
		// remove any expired entities.
		_entities = _entities.Where(x => !x.IsExpired).ToList();
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		foreach (var entity in _entities)
			entity.Draw(spriteBatch);
	}
}
