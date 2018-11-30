using UnityEngine;

namespace Logic.Entity
{
	public class Entity : MonoBehaviour, IPoolable
	{
		public int Serial => _serial;
		[SerializeField]
		private int _serial;

		public void Init(int serial)
		{
			_serial = serial;
		}

		public void OnInit()
		{
			
		}

		public void OnRelease()
		{
			
		}
	}
}