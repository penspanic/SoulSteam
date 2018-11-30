using UnityEngine;

namespace DefaultNamespace
{
	public class Test : MonoBehaviour
	{
		private void Awake()
		{
			Common.StaticInfo.StaticInfoManager.Instance.Init("StaticData/Common/");
			PoolManager<Logic.Entity.Entity>.Instance.Get();
		}
	}
}