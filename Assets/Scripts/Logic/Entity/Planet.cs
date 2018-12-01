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

        public override EntityType Type { get; } = EntityType.Planet;

        public Common.StaticData.PlanetInfo PlanetInfo { get; private set; }
        public override void Init(string id, int serial)
        {
            base.Init(id, serial);
            PlanetInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.PlanetInfo;
        }

        float impact = 45f, cycle = 90f;
        public void OnTriggerEnter2D(Collider2D other)
        {
            Entity otherEntity = other.GetComponent<Entity>();

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
                    Vector2 incomingVec = dust.moveDirection;
                    Vector2 normalVec = other.transform.position - transform.position;
                    Vector2 reflectVec = Vector2.Reflect(incomingVec, normalVec);
                    Vector2 angleVec = reflectVec - incomingVec;
                    float angle = Mathf.Atan2(reflectVec.y - incomingVec.y, reflectVec.x - incomingVec.x) * Mathf.Rad2Deg;
                    angle = Mathf.Abs(angle);
                    if(angle < impact) // 충돌
                    {
                        //dust.ChangeMoveState(this, MoveType.Impacted);
                        dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    else if(angle < cycle) // 공전
                    {
                        //dust.ChangeMoveState(this, MoveType.Cycle);
                        dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    else // 왜곡
                    {
                        dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    break;

                default:
                    break;
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            Entity otherEntity = other.GetComponent<Entity>();

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