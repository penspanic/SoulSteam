using UnityEngine;
using System.Collections.Generic;

namespace Input
{
	public class PressController : MonoBehaviour
	{
		[SerializeField]
		private InputController _inputController;

		private Logic.GravityZone _gravityZone;

		private List<Logic.Entity.Entity> _entities = new List<Logic.Entity.Entity>();

		private void Awake()
		{
			_inputController.OnPressStart += OnPressStart;
			_inputController.OnPressUp += OnPressUp;
			_inputController.OnTouchPosChange += OnTouchPosChange;
			_gravityZone = Logic.GravityZone.Create();
			_gravityZone.gameObject.SetActive(false);
		}

		private void OnTouchPosChange(Vector3 pos)
		{
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos = new Vector3(pos.x, pos.y, 0f);
			_gravityZone.transform.position = pos;
		}

		private void OnPressStart(Vector3 pos)
		{
			_gravityZone.gameObject.SetActive(true);
			pos = Camera.main.ScreenToWorldPoint(pos);

			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

			Logic.Entity.Entity entity = hit.collider?.gameObject.GetComponent<Logic.Entity.Entity>();
			if (entity != null)
			{
				Debug.Log($"Pressed : {entity}");
			}
		}

		private void OnPressUp(Vector3 pos)
		{
			_gravityZone.gameObject.SetActive(false);
			for (int i = 0; i < _entities.Count; ++i)
			{
				_entities[i].OnPressUp();
			}
			_entities.Clear();
		}
	}
}