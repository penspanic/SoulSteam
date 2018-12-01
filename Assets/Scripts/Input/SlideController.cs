using UnityEngine;
using System.Collections.Generic;

namespace Input
{
	public class SlideController : MonoBehaviour
	{
		[SerializeField]
		private InputController _inputController;

		private List<Logic.Entity.Entity> _entities = new List<Logic.Entity.Entity>(); 

		private void Awake()
		{
			_inputController.OnSlide += OnSlide;
			_inputController.OnTouchUp += OnTouchUp;
		}

		private void OnSlide(Vector3 startPoint, Vector3 endPoint)
		{
			startPoint = Camera.main.ScreenToWorldPoint(startPoint);
			endPoint = Camera.main.ScreenToWorldPoint(endPoint);
			startPoint = new Vector3(startPoint.x, startPoint.y, 0f);
			endPoint = new Vector3(endPoint.x, endPoint.y, 0f);
			Vector3 delta = endPoint - startPoint;
			Collider2D[] hits = Physics2D.OverlapCircleAll(endPoint, 1f);
			for (int i = 0; i < hits.Length; ++i)
			{
				Logic.Entity.Entity entity = hits[i].gameObject.GetComponent<Logic.Entity.Entity>();
				if (entity != null)
				{
					entity.OnDrag(endPoint, delta);
				}
			}
			Debug.Log($"OnSlide, {startPoint}->{endPoint}");
		}

		private void OnTouchUp(Vector3 pos)
		{
			_entities.Clear();
		}
	}
}