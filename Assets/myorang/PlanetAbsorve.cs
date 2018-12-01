using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

public class PlanetAbsorve : MonoBehaviour
{
    private Planet _planet;
    private void Awake()
    {
        _planet = transform.parent.GetComponent<Planet>();
        _planet.SetAbsorve(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity otherEntity = other.GetComponent<Entity>();
        if (otherEntity == null)
        {
            return;
        }

        // 상위개체와 충돌함 (Star, Blackhole)
        if (otherEntity.Type > _planet.Type)
            return;

        // 동일개체와 충돌함 (Planet)

        if (otherEntity.Type == _planet.Type)
        {
            Planet otherPlanet = (Planet)otherEntity;

            switch (_planet.elementId)
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
                        if (otherPlanet.level == _planet.level)
                        {

                        }
                        else if (otherPlanet.level > _planet.level)
                        {
                            EntityManager.Instance.Destroy<Planet>(_planet);
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
                _planet.CollectDust(dust);
                break;

            default:
                break;
        }
    }
}
