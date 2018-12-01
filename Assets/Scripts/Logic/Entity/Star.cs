namespace Logic.Entity
{
	/// <summary>
	/// 항성
	/// </summary>
	public class Star : Entity
	{
		public override EntityType Type { get; } = EntityType.Star;

		public Common.StaticData.StarInfo StarInfo { get; private set; }
		public override void Init(string id, int serial)
		{
			base.Init(id, serial);
			StarInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.StarInfo;
		}
	}
}