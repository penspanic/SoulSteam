using UnityEngine;

namespace Logic.Entity
{
	/// <summary>
	/// 행성
	/// </summary>
	public class Planet : Entity
    {
        public SpriteRenderer _renderer;
        public Sprite[] _sprites;

        public CircleCollider2D _col;

        public float[] gravity = { 0.8f, 1.2f };

        public override EntityType Type { get; } = EntityType.Planet;

		public Common.StaticData.PlanetInfo PlanetInfo { get; private set; }
		public override void Init(string id, int serial)
		{
			base.Init(id, serial);
			PlanetInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.PlanetInfo;
		}
	}
}