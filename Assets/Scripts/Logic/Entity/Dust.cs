using UnityEngine;
using System.Collections.Generic;

namespace Logic.Entity
{
    /// <summary>
    /// 우주 먼지
    /// </summary>
    public class Dust : Entity
    {
        public override EntityType Type { get; } = EntityType.Dust;

        public SpriteRenderer _renderer;
        public Sprite[] _sprites;

        // rotate
        private Vector3 angleRotate;
        private float rotateSpeed;

        // movement
        public Vector3 moveVector;
        public Vector3 moveDirection;
        public Vector3 moveSpeedRate;     // 이동 가속도
        public float moveSpeedTotal;      // 이동속도 - 실제 이동속도
        public float moveSpeedBase;       // 이동속도 - 기본
        public float moveSpeedLevelRate;  // 이동속도 - 단계비율
        //public float moveSpeedGravity;  // 중력

        public List<Entity> affectedEntities = new List<Entity>();

        // scale
        public float scaleBase;         // 기본 크기
        public float scaleRate;         // 단계별 크기 비율

        [Range(1, 3)]
        public int level;               // 레벨

        public void Start()
        {
            angleRotate.x = 0;
            angleRotate.y = 0;
            angleRotate.z = Random.value;

            if (DD_Testment.Testment.isTest)
            {
                moveDirection.x = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
                moveDirection.y = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
                moveDirection.z = 0f;
                moveDirection = moveDirection.normalized;
            }
        }

        public void Update()
        {
            if (DD_Testment.Testment == null)
            {
                return;
            }

            if (DD_Testment.Testment.isTest)
            {
                rotateSpeed = DD_Testment.Testment.dust_rotateSpeed;
                moveSpeedBase = DD_Testment.Testment.dust_moveSpeedBase;
                moveSpeedLevelRate = DD_Testment.Testment.dust_moveSpeedLevelRate[level-1];

                scaleBase = DD_Testment.Testment.dust_scaleBase;
                scaleRate = DD_Testment.Testment.dust_scaleLevelRate[level-1];
            }

            transform.Rotate(angleRotate * rotateSpeed * Time.deltaTime);
            Move();
        }

        public void Move()
        {
            moveSpeedTotal = moveSpeedBase * moveSpeedLevelRate;

            moveVector = Vector3.zero;
            for (int i = 0; i < affectedEntities.Count; i++)
            {
                moveVector += affectedEntities[i].GetAffectVector();
            }
            moveVector = (moveDirection * moveSpeedTotal) + moveVector;
            moveVector *= Time.deltaTime;

            transform.position += moveVector;
        }

        public void GetAffectedVector()
        {
        }

        public void AddAffectedEntity(Entity affectEntity)
        {
            if (affectEntity.Type == EntityType.Dust)
                return;

            affectedEntities.Add(affectEntity);
        }

        public void RemoveAffectedEntity(Entity affectEntity)
        {
            if (affectEntity.Type == EntityType.Dust)
                return;

            affectedEntities.Remove(affectEntity);
        }

        public Common.StaticData.StarDustInfo StarDustInfo { get; private set; }
        public override void Init(string id, int serial)
        {
            base.Init(id, serial);
            StarDustInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.StarDustInfo;

            angleRotate.x = Random.value;
            angleRotate.y = Random.value;
            angleRotate.z = Random.value;

            if (DD_Testment.Testment != null)
            {
                SetData(DD_Testment.Testment.isTest);
            }
        }

        public void SetParameter(Vector2 pos, Vector2 dir)
        {
            transform.position = pos;
            moveDirection = dir.normalized;
        }

        public void SetData(bool isTest)
        {

        }

        private readonly static string LayerMaskWall = "Wall";
        private RaycastHit2D[] hit;
        public void WallOutReset(Transform hitWall)
        {
            hit = Physics2D.RaycastAll(transform.position, -moveDirection, 100f, 1 << LayerMask.NameToLayer("Wall"), -1, 1);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform != hitWall)
                {
                    transform.position = hit[i].point;
                }
            }
        }
    }
}