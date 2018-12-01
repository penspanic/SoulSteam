using Spine;
using Spine.Unity;
using UnityEngine;

namespace Logic.Entity
{
    /// <summary>
    /// 행성
    /// </summary>
    public class Star : Entity
    {
        public SpriteRenderer _renderer;
        public Sprite[] _sprites;

        public CircleCollider2D _col;
        private float _colOriginRadius;
        private CircleCollider2D _absorveCol;
        private float _absorveColOriginRadius;

        public int spriteIdx = 0;

        public override EntityType Type { get; } = EntityType.Star;

        public Common.StaticData.StarInfo StarInfo { get; private set; }
        private int _collectedDust = 0;
        private SkeletonAnimation _skeleton;

        private void Awake()
        {
            _colOriginRadius = _col.radius;
            _skeleton = GetComponent<SkeletonAnimation>();
        }

        public override void Init(string id, int serial)
        {
            base.Init(id, serial);
            StarInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.StarInfo;
            OnChangeLevel();
            // B W Y
            if (id == "Star_1")
            {
                _skeleton.Skeleton.SetSkin("B");
            }
            else if (id == "Star_2")
            {
                _skeleton.Skeleton.SetSkin("B");
            }
            else if (id == "Star_3")
            {
                _skeleton.Skeleton.SetSkin("B");
            }

            _skeleton.state.SetAnimation(0, "create", false);
            _skeleton.AnimationState.Complete += OnCreateComplete;
        }

        private void OnCreateComplete(TrackEntry trackentry)
        {
            _skeleton.AnimationState.Complete -= OnCreateComplete;
            _skeleton.state.SetAnimation(0, "idle", true);
        }

        public override void OnRelease()
        {
            _skeleton.state.SetAnimation(0, "explosion", false);
            _skeleton.AnimationState.Complete += OnExplosionComplete;
        }

        private void OnExplosionComplete(TrackEntry trackentry)
        {
            _skeleton.AnimationState.Complete -= OnExplosionComplete;
            gameObject.SetActive(false);
        }


        public void SetAbsorve(StarAbsorve absorve)
        {
            _absorveCol = absorve.GetComponent<CircleCollider2D>();
            _absorveColOriginRadius = _absorveCol.radius;
        }

        public void CollectDust(Dust dust)
        {
            EntityManager.Instance.Destroy<Dust>(dust);
            ++_collectedDust;
            if (StarInfo.Growths.Count == level)
            {
            }
            else
            {
                if (_collectedDust >= StarInfo.Growths[level - 1].RequireStarDust)
                {
                    _collectedDust = 0;
                    ++level;
                    OnChangeLevel();
                }
            }
        }

        protected override void OnChangeLevel()
        {
            base.OnChangeLevel();
            float radiusScale = StarInfo.Growths[level - 1].Scale;
            this.transform.localScale = Vector3.one * radiusScale;
            _col.radius = _colOriginRadius * radiusScale;
            _absorveCol.radius = _absorveColOriginRadius * radiusScale;
        }

        float impact = 45f, cycle = 90f;
        public void OnTriggerEnter2D(Collider2D other)
        {
            Entity otherEntity = other.GetComponent<Entity>();
            if (otherEntity == null)
            {
                return;
            }

            // 상위개체와 충돌함 (Star, Blackhole)
            if (otherEntity.Type > Type)
                return;

            // 동일개체와 충돌함 (Planet)

            // 하위개체와 추돌함 (Dust)
            switch (otherEntity.Type)
            {
                case EntityType.Undefined:
                    break;

                case EntityType.Planet:
                    Planet otherPlanet = (Planet)otherEntity;
                    Vector2 incomingVec = otherPlanet.moveDirection;
                    Vector2 normalVec = other.transform.position - transform.position;
                    Vector2 reflectVec = Vector2.Reflect(incomingVec, normalVec);
                    Vector2 angleVec = reflectVec - incomingVec;
                    float angle = Mathf.Atan2(reflectVec.y - incomingVec.y, reflectVec.x - incomingVec.x) * Mathf.Rad2Deg;
                    angle = Mathf.Abs(angle);
                    if (angle < impact) // 충돌
                    {
                        otherPlanet.ChangeMoveState(this, MoveType.Impacted);
                        //                        dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    else if (angle < cycle) // 공전
                    {
                        otherPlanet.ChangeMoveState(this, MoveType.Cycle);
                        //dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    else // 왜곡
                    {
                        otherPlanet.ChangeMoveState(this, MoveType.Curve);
                    }
                    break;

                default:
                    break;
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            Entity otherEntity = other.GetComponent<Entity>();
            if (otherEntity == null)
            {
                return;
            }

            // 상위개체와 충돌함 (Star, Blackhole)
            if (otherEntity.Type > Type)
                return;

            // 동일개체와 충돌함 (Planet)

            // 하위개체와 추돌함 (Dust)
            switch (otherEntity.Type)
            {
                case EntityType.Undefined:
                    break;

                case EntityType.Dust:
                    Dust dust = (Dust)otherEntity;
                    dust.RemoveAffectedEntity(this);
                    break;

                default:
                    break;
            }
        }
    }
}