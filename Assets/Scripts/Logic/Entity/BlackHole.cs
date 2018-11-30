namespace Logic.Entity
{
	/// <summary>
	/// 행성
	/// </summary>
	public class BlackHole : Entity
	{
		public Common.StaticData.BlackHoleInfo BlackHoleInfo { get; private set; }
		public override void Init(string id, int serial)
		{
			base.Init(id, serial);
			BlackHoleInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.BlackHoleInfo;
		}
	}
}