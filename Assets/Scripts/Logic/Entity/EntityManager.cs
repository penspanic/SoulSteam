using System.Collections.Generic;

namespace Logic.Entity
{
	public class EntityManager : Utility.SingletonMonoBehaviour<EntityManager>
	{
		private Dictionary<int /*Serial*/, Entity> _entities = new Dictionary<int, Entity>();
		private Dictionary<EntityType, List<Entity>> _entityTypeLists = new Dictionary<EntityType, List<Entity>>();
		private int _currentSerial;
		public override void Init()
		{
		}

		public T Create<T>(Common.StaticData.EntityInfo entityInfo) where T : Entity
		{
			T entity = PoolManager<T>.Instance.Get();
			int serial = ++_currentSerial;
			entity.Init(entityInfo.Id, serial);
			_entities.Add(serial, entity);
			if (_entityTypeLists.ContainsKey(entity.Type) == false)
			{
				_entityTypeLists.Add(entity.Type, new List<Entity>());
			}
			_entityTypeLists[entity.Type].Add(entity);

			return entity;
		}

		public void Destroy<T>(T entity) where T : Entity
		{
			PoolManager<T>.Instance.Release(entity);
			_entities.Remove(entity.Serial);
			_entityTypeLists[entity.Type].Remove(entity);
		}

		public Entity Get<T>(int serial) where T : Entity
		{
			if (_entities.ContainsKey(serial) == false)
			{
				return null;
			}

			return _entities[serial];
		}

		public List<Entity> GetAll(EntityType type)
		{
			if (_entityTypeLists.ContainsKey(type) == false)
			{
				return new List<Entity>();
			}

			return new List<Entity>(_entityTypeLists[type]);
		}
	}
}