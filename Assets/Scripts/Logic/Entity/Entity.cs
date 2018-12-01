using UnityEngine;

namespace Logic.Entity
{
	public enum EntityType
	{
		Undefined = 0,
		Entity,
		Dust,
		Planet,
		Star,
		BlackHole,
	}

    public enum MoveType
    {
        Undefined = 0,
        Holded,     // 
        Linear,
        Curve,
        Cycle,
        Impacted
    }

	public class Entity : MonoBehaviour, IPoolable, Input.ITouchable
	{
		public virtual EntityType Type { get; } = EntityType.Entity;
		public override string ToString() => $"{Id}({Serial})";
		public Common.StaticData.EntityInfo Info { get; private set; }
		public bool IsPressed { get; private set; }
		public string Id => _id;
		private string _id;
		public int Serial => _serial;
		private int _serial;
		public int Level => level;
		protected int level;

        public MoveType MoveState = MoveType.Undefined;
        public float impactedGravity;
        public float curveGravity;

		
        public virtual void Init(string id, int serial)
		{
			_id = id;
			_serial = serial;
			level = 1;
			name = $"{Type}_{ToString()}";
			IsPressed = false;
			Info = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[_id];
		}

		protected virtual void OnChangeLevel()
		{
			
		}

		#region IPoolable
		public void OnInit()
		{
			gameObject.SetActive(true);
		}

		public void OnRelease()
		{
			gameObject.SetActive(false);
		}
		#endregion
		#region Input.ITouchable

		public virtual void OnPressDown()
		{
			IsPressed = true;
		}

		public virtual void OnPressUp()
		{
			IsPressed = false;
		}

		public virtual void OnDrag(Vector3 pos, Vector3 deltaPos)
		{
			transform.position += deltaPos;
		}
		#endregion

        public virtual void ChangeMoveState(Entity hole, MoveType movetype)
        {
        }

        public Vector3 GetAffectVector()
        {
	        return Vector3.zero;
        }

        public float cycleRange = 1f;
        //public float cycleNextRange = 
        public int cycleCount = 0;
        public int cycleCountMax = 5;
    }
}