
using System.Linq;
using Common.StaticInfo;
using DG.Tweening;
using Logic.Entity;

namespace Logic.Situation
{
	public class CreatePlanetSituation : AbstractSituation
	{
		public CreatePlanetSituation()
		{
			Common.StaticData.EntityInfo[] planetInfos = StaticInfoManager.Instance.EntityInfos.GetList()
				.Where(i => i is Common.StaticData.PlanetInfo).ToArray();
			Common.StaticData.PlanetInfo selected = planetInfos[planetInfos.Length - 1] as Common.StaticData.PlanetInfo;
			EntityManager.Instance.Create<Planet>(selected);
		}
	}
}