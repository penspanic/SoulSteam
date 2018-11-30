using UnityEngine;

namespace Logic.Entity
{
	/// <summary>
	/// 우주 먼지
	/// </summary>
	public class StarDust : Entity
	{
        // rotate
        private Vector3 angleRotate;
        private float rotateSpeed;

        // movement
        public Vector3 moveDirection;
        public float moveSpeed;         // 이동속도
        public Vector3 moveSpeedRate;     // 이동가속도
        //public float moveSpeedGravity;  // 중력

        // scale
        public float scaleBase;         // 기본 크기
        public float scaleRate;         // 단계별 크기 비율
        
        public void Update()
        {
            if (DD_Testment.Testment.isTest)
            {
            }
        }

        public Common.StaticData.StarDustInfo StarDustInfo { get; private set; }
		public override void Init(string id, int serial)
		{
			base.Init(id, serial);
			StarDustInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.StarDustInfo;

            angleRotate.x = Random.value;
            angleRotate.y = Random.value;
            angleRotate.z = Random.value;

            SetData(DD_Testment.Testment.isTest);
		}

        public void SetData(bool isTest)
        {

        }
	}
}