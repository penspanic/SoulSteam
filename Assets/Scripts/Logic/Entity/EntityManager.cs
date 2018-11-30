using System.Collections.Generic;

namespace Logic.Entity
{
	public class EntityManager : Utility.SingletonMonoBehaviour<EntityManager>
	{
		private Dictionary<int /*Serial*/, Entity> _entities = new Dictionary<int, Entity>();
		public override void Init()
		{
		}

		public Entity Get(int serial)
		{
			if (_entities.ContainsKey(serial) == false)
			{
				return null;
			}

			return _entities[serial];
		}
	}
}