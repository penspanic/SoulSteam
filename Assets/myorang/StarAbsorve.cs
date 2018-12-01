using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

public class StarAbsorve : MonoBehaviour
{
	private Star _star;
	private void Awake()
	{
		_star = transform.parent.GetComponent<Star>();
		_star.SetAbsorve(this);
	}
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gravity"))
            return;

        Entity otherEntity = other.transform?.parent?.GetComponent<Entity>();
		if (otherEntity == null)
			return;

        // 상위개체와 충돌함 (Star, Blackhole)
        if (otherEntity.Type > _star.Type)
			return;

		// 동일개체와 충돌함 (Planet)
        if(otherEntity.Type == _star.Type)
        {
            if(otherEntity.level == _star.level)
            {
                if (otherEntity.Serial > _star.Serial)
                {
                    EntityManager.Instance.Destroy<Star>(_star);
                    otherEntity.level++;
                    otherEntity.OnChangeLevel();
                    return;
                }
                else
                {
                    EntityManager.Instance.Destroy<Star>((Star)otherEntity);
                    _star.level++;
                    _star.OnChangeLevel();
                    return;
                }
            }
            else if (otherEntity.level < _star.level)
            {
                EntityManager.Instance.Destroy<Star>((Star)otherEntity);
                _star.level++;
                _star.OnChangeLevel();
                return;
            }
            return;
        }

		// 하위개체와 추돌함 (Dust)
		switch (otherEntity.Type)
		{
			case EntityType.Undefined:
				break;

			case EntityType.Planet:
                Planet otherPlanet = (Planet)otherEntity;
				_star.CollectPlanet(otherPlanet);
				break;

			default:
				break;
		}
	}
}
