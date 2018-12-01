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
        public TrailRenderer _trail;
        public Sprite[] _sprites;

        // rotate
        private Vector3 angleRotate;
        private float rotateSpeed;

        // movement
        public Vector3 affectedVector;
        public Vector3 moveDirection;
        public Vector3 moveSpeedRate;     // 이동 가속도
        public float moveSpeedTotal;      // 이동속도 - 실제 이동속도
        public float moveSpeedBase;       // 이동속도 - 기본
        public float moveSpeedLevelRate;  // 이동속도 - 단계비율
        //public float moveSpeedGravity;  // 중력

        public Dictionary<Entity, float> affectedEntities = new Dictionary<Entity, float>();

        // scale
        public float scaleBase;         // 기본 크기
        public float scaleRate;         // 단계별 크기 비율
        
        public void Start()
        {
            angleRotate.x = 0;
            angleRotate.y = 0;
            angleRotate.z = Random.value;

            if (Testment.testment.isTest)
            {
                moveDirection.x = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
                moveDirection.y = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
                moveDirection.z = 0f;
                moveDirection = moveDirection.normalized;
            }
        }

        public void Update()
        {
            if (Testment.testment == null)
            {
                return;
            }

            if (Testment.testment.isTest)
            {
                rotateSpeed = Testment.testment.dust_rotateSpeed;
                moveSpeedBase = Testment.testment.dust_moveSpeedBase;
                moveSpeedLevelRate = Testment.testment.dust_moveSpeedLevelRate[level - 1];

                scaleBase = Testment.testment.dust_scaleBase;
                scaleRate = Testment.testment.dust_scaleLevelRate[level - 1];
            }

            transform.Rotate(angleRotate * rotateSpeed * Time.deltaTime);
            Move();
        }

        private delegate void Delegate_Move();
        private Delegate_Move Move;
        public override void ChangeMoveState(Entity hole, MoveType movetype)
        {
            _trail.enabled = false;
            switch (movetype)
            {
                case MoveType.Undefined:
                    break;

                case MoveType.Holded:
                    AddAffectedEntity(hole, hole.impactedGravity);
                    break;

                case MoveType.Linear:
                    affectedEntities.Clear();
                    Move = MoveLinear;
                    break;

                case MoveType.Curve:
                    AddAffectedEntity(hole, hole.curveGravity);
                    Move = MoveLinear;
                    break;

                case MoveType.Cycle:
                    if (cycleCount >= cycleCountMax)
                    {
                        ChangeMoveState(null, MoveType.Linear);
                        return;
                    }
                    else
                    {
                        affectedEntities.Clear();
                        cycleCore = hole;
                        Move = MoveCycleStart;
                    }
                    break;

                case MoveType.Impacted:
                    AddAffectedEntity(hole, hole.impactedGravity);
                    Move = MoveLinear;
                    break;

                default:
                    break;
            }

            MoveState = movetype;
        }

        public Entity cycleCore = null;
        public float cycleRotateSpeed = 72f;
        public float cycleRange = 1f;

        public void MoveCycleStart()
        {
            MoveLinear();

            if (cycleCore.cycleCount >= cycleCore.cycleCountMax)
            {
                ChangeMoveState(null, MoveType.Linear);
                return;
            }

            float nowDist = Vector2.Distance(cycleCore.transform.position, transform.position);
            if (nowDist > cycleRange)
                return;

            cycleCore.cycleCount++;
            _trail.enabled = true;
            Move = MoveCycleLoop;
        }

        public void MoveCycleLoop()
        {
            if (cycleCore == null)
                ChangeMoveState(null, MoveType.Linear);

            transform.RotateAround(cycleCore.transform.position, Vector3.forward, cycleRotateSpeed * Time.deltaTime);
        }

        public void MoveLinear()
        {
            moveSpeedTotal = moveSpeedBase * moveSpeedLevelRate;

            affectedVector = Vector3.zero;

            if (affectedEntities != null)
                foreach (var hole in affectedEntities)
                {
                    affectedVector += (hole.Key.transform.position - transform.position).normalized
                        * moveSpeedTotal * hole.Value * Time.deltaTime;
                }

            moveDirection = (moveDirection * moveSpeedTotal * Time.deltaTime + affectedVector).normalized;

            transform.position += moveDirection * moveSpeedTotal * Time.deltaTime;
        }

        public void GetAffectedVector()
        {
        }

        public void AddAffectedEntity(Entity affectEntity, float gravityRate)
        {
            if (affectEntity.Type == EntityType.Dust)
                return;

            if (affectedEntities.ContainsKey(affectEntity))
                return;

            affectedEntities.Add(affectEntity, gravityRate);
        }

        public void RemoveAffectedEntity(Entity affectEntity)
        {
            if (affectEntity.Type == EntityType.Dust)
                return;

            affectedEntities.Remove(affectEntity);
        }

        public Common.StaticData.DustInfo DustInfo { get; private set; }
        public override void Init(string id, int serial)
        {
            base.Init(id, serial);
            DustInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.DustInfo;

            angleRotate.x = Random.value;
            angleRotate.y = Random.value;
            angleRotate.z = Random.value;

            if (Testment.testment != null)
            {
                SetData(Testment.testment.isTest);
            }

            affectedEntities.Clear();
            ChangeMoveState(null, MoveType.Linear);
            _trail.enabled = false;
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