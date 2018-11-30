using UnityEngine;

namespace DefaultNamespace
{
	public class Test : MonoBehaviour
	{
		private void Awake()
		{
			PoolManager<Logic.Entity.Entity>.Instance.Get();
		}
	}
}