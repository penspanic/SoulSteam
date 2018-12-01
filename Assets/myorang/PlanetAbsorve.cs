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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity otherEntity = other.GetComponent<Entity>();

        // 상위개체와 충돌함 (Star, Blackhole)
        if (otherEntity.Type > _planet.Type)
            return;

        // 동일개체와 충돌함 (Planet)

        // 하위개체와 추돌함 (Dust)
        switch (otherEntity.Type)
        {
            case EntityType.Undefined:
                break;

            case EntityType.Dust:
                Dust dust = (Dust)otherEntity;
                EntityManager.Instance.Destroy<Dust>(dust);
                break;

            default:
                break;
        }
    }
}
