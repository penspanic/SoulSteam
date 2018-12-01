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

	private void OnTriggerEnter2D(Collider2D other)
	{
		Entity otherEntity = other.GetComponent<Entity>();
		if (otherEntity == null)
		{
			return;
		}

		// 상위개체와 충돌함 (Star, Blackhole)
		if (otherEntity.Type > _star.Type)
			return;

		// 동일개체와 충돌함 (Planet)

		// 하위개체와 추돌함 (Dust)
		switch (otherEntity.Type)
		{
			case EntityType.Undefined:
				break;

			case EntityType.Dust:
				Dust dust = (Dust)otherEntity;
				_star.CollectDust(dust);
				break;

			default:
				break;
		}
	}
}
