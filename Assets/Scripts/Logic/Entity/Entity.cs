using UnityEngine;

namespace Logic.Entity
{
	public enum EntityType
	{
		Undefined = 0,
		Entity,
		StarDust,
		Planet,
		Star,
		BlackHole,
	}
	public class Entity : MonoBehaviour, IPoolable
	{
		public virtual EntityType Type { get; } = EntityType.Entity;
		public override string ToString() => $"{Id}({Serial})";
		public Common.StaticData.EntityInfo Info { get; private set; }
		public string Id => _id;
		private string _id;
		public int Serial => _serial;
		private int _serial;

		public virtual void Init(string id, int serial)
		{
			_id = id;
			_serial = serial;
			Info = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[_id];
		}

		public void OnInit()
		{
			
		}

		public void OnRelease()
		{
			
		}
	}
}