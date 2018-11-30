using System.Collections.Generic;

namespace Logic.Entity
{
	public class EntityManager : Utility.SingletonMonoBehaviour<EntityManager>
	{
		private Dictionary<int /*Serial*/, Entity> _entities = new Dictionary<int, Entity>();
		private int _currentSerial;
		public override void Init()
		{
		}

		public Entity Create<T>(Common.StaticData.EntityInfo entityInfo) where T : Entity
		{
			T entity = PoolManager<T>.Instance.Get();
			int serial = ++_currentSerial;
			entity.Init(entityInfo.Id, serial);
			return entity;
		}

		public Entity Get<T>(int serial) where T : Entity
		{
			if (_entities.ContainsKey(serial) == false)
			{
				return null;
			}

			return _entities[serial];
		}
	}
}