using Common.StaticInfo;
using Logic.Entity;
using UnityEngine;

namespace DefaultNamespace
{
	public class Test : MonoBehaviour
	{
		private void Awake()
		{
			StaticInfoManager.Instance.Init("StaticData/Common/");

			EntityManager.Instance.Create<StarDust>(StaticInfoManager.Instance.EntityInfos["StarDust_1"]);

		}
	}
}