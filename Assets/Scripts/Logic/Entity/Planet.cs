using Common.StaticData;
using Common.StaticInfo;
using UnityEngine;
using System.Collections.Generic;

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
        private float _colOriginRadius;
        private CircleCollider2D _absorveCol;
        private float _absorveColOriginRadius;

        public enum Element
        {
            Normal = 0,
            Fire,
            Ice,
            Iron,
            Gas,
            Tree
        }
        public Element elementId = 0;

        public List<Dust> satellites = new List<Dust>();

        public override EntityType Type { get; } = EntityType.Planet;

        public Common.StaticData.PlanetInfo PlanetInfo { get; private set; }
        private int _collectedDust = 0;

        private void Awake()
        {
            _colOriginRadius = _col.radius;
        }

        public override void Init(string id, int serial)
        {
            base.Init(id, serial);
            PlanetInfo = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[id] as Common.StaticData.PlanetInfo;
            OnChangeLevel();
        }

        public void SetAbsorve(PlanetAbsorve absorve)
        {
            _absorveCol = absorve.GetComponent<CircleCollider2D>();
            _absorveColOriginRadius = _absorveCol.radius;
        }

        public void CollectDust(Dust dust)
        {
            EntityManager.Instance.Destroy<Dust>(dust);
            ++_collectedDust;
            if (_collectedDust >= PlanetInfo.Growths[level - 1].RequireStarDust)
            {
                _collectedDust = 0;
                ++level;
                OnChangeLevel();
            }
        }

        protected override void OnChangeLevel()
        {
            base.OnChangeLevel();
            if (level == PlanetInfo.Growths.Count)
            {
                EntityManager.Instance.Destroy(this);
                EntityManager.Instance.Create<Star>(StaticInfoManager.Instance.EntityInfos["Star_2"] as StarInfo);
                return;
            }
            _renderer.sprite = _sprites[level - 1];
            float radiusScale = PlanetInfo.Growths[level - 1].Scale;
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
            if (otherEntity.Type == Type)
            {
                Planet otherPlanet = (Planet)otherEntity;

                switch (elementId)
                {
                    case Element.Normal:
                        break;

                    case Element.Fire:
                        // 승리
                        if (otherPlanet.elementId == Element.Ice
                            || otherPlanet.elementId == Element.Gas)
                        {
                            EntityManager.Instance.Destroy<Planet>(otherPlanet);
                        }

                        // 합체
                        else if (otherPlanet.elementId == Element.Fire
                           || otherPlanet.elementId == Element.Tree)
                        {
                            // 단계가 같음
                            if (otherPlanet.level == level)
                            {

                            }
                            else if (otherPlanet.level > level)
                            {
                                EntityManager.Instance.Destroy<Planet>(this);
                            }
                            else
                            {
                                EntityManager.Instance.Destroy<Planet>(otherPlanet);
                            }
                        }

                        // 패배
                        //else if (otherPlanet.elementId == Element.Iron)
                        //{
                        //}
                        break;

                    case Element.Ice:
                        // 승리
                        // 합체
                        if (otherPlanet.elementId == Element.Ice
                            || otherPlanet.elementId == Element.Gas
                            || otherPlanet.elementId == Element.Tree)
                        {

                        }

                        // 패배
                        //else if (otherPlanet.elementId == Element.Fire
                        //    || otherPlanet.elementId == Element.Iron)
                        //{

                        //}
                        break;

                    case Element.Iron:
                        // 승리
                        if (otherPlanet.elementId == Element.Fire
                            || otherPlanet.elementId == Element.Ice
                            || otherPlanet.elementId == Element.Gas
                            || otherPlanet.elementId == Element.Tree)
                        {
                            EntityManager.Instance.Destroy<Planet>(otherPlanet);
                        }

                        // 합체
                        else if (otherPlanet.elementId == Element.Iron)
                        {

                        }
                        // 패배
                        break;

                    case Element.Gas:
                        // 승리
                        // 자신 - 합체
                        if (otherPlanet.elementId == Element.Gas)
                        {

                        }
                        // 상대 - 합체
                        else if (otherPlanet.elementId == Element.Ice
                            || otherPlanet.elementId == Element.Tree)
                        {

                        }
                        // 패배
                        //else if (otherPlanet.elementId == Element.Fire
                        //    || otherPlanet.elementId == Element.Iron)
                        //{

                        //}
                        break;

                    case Element.Tree:
                        // 승리
                        // 자신 - 합체
                        if (otherPlanet.elementId == Element.Tree
                            || otherPlanet.elementId == Element.Gas)
                        {

                        }

                        // 상대 - 합체
                        else if (otherPlanet.elementId == Element.Fire
                            || otherPlanet.elementId == Element.Ice)
                        {

                        }

                        // 패배
                        //else if (otherPlanet.elementId == Element.Iron)
                        //{

                        //}
                        break;

                    default:
                        break;
                }

                return;
            }

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
                    if (angle < impact) // 충돌
                    {
                        dust.ChangeMoveState(this, MoveType.Impacted);
                        //                        dust.ChangeMoveState(this, MoveType.Curve);
                    }
                    else if (angle < cycle) // 공전
                    {
                        dust.ChangeMoveState(this, MoveType.Cycle);
                        //dust.ChangeMoveState(this, MoveType.Curve);
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