namespace Logic.Entity
{
	/// <summary>
	/// 우주 먼지
	/// </summary>
	public class StarDust : Entity
	{
		public Common.StaticData.StarDustInfo StarDustInfo { get; private set; }
		public override void Init(string id, int serial)
		{
			base.Init(id, serial);
			StarDustInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.StarDustInfo;
		}
	}
}