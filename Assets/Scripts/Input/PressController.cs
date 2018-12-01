using UnityEngine;
using System.Collections.Generic;
using Logic.Entity;

namespace Input
{
	public class PressController : MonoBehaviour
	{
		[SerializeField]
		private InputController _inputController;

		private Logic.GravityZone _gravityZone;
		private DustGenerator _dustGenerator;

		private List<Logic.Entity.Entity> _entities = new List<Logic.Entity.Entity>();

		private void Awake()
		{
			_inputController.OnPressStart += OnPressStart;
			_inputController.OnPressUp += OnPressUp;
			_inputController.OnClick += OnClick;
			_inputController.OnTouchPosChange += OnTouchPosChange;
			_gravityZone = Logic.GravityZone.Create();
			_dustGenerator = GameObject.FindObjectOfType<DustGenerator>();
			_gravityZone.gameObject.SetActive(false);
		}

		private void OnClick(Vector3 pos)
		{
			pos = Camera.main.ScreenToWorldPoint(pos);
			
			RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
			for (int i = 0; i < hits.Length; ++i)
			{
				Logic.Entity.Entity entity = hits[i].collider.gameObject.GetComponent<Logic.Entity.Entity>();
				if (entity is Planet)
				{
					Planet planet = entity as Planet;
					Vector3 createPos = planet.transform.position;
					EntityManager.Instance.Destroy(planet);
					_dustGenerator.CreateOnPlanetDestruction(planet.PlanetInfo.Growths[planet.Level - 1].DestroyDustCount, createPos);
					break;
				}
			}
		}

		private void OnTouchPosChange(Vector3 pos)
		{
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos = new Vector3(pos.x, pos.y, 0f);
			_gravityZone.transform.position = pos;
		}

		private void OnPressStart(Vector3 pos)
		{
			_entities.Clear();
			_gravityZone.gameObject.SetActive(true);
			pos = Camera.main.ScreenToWorldPoint(pos);

			RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
			for (int i = 0; i < hits.Length; ++i)
			{
				Logic.Entity.Entity entity = hits[i].collider.gameObject.GetComponent<Logic.Entity.Entity>();
				if (entity is Logic.Entity.Planet || entity is Logic.Entity.Star)
				{
					_entities.Add(entity);
				}
			}

		}

		private void OnPressUp(Vector3 pos)
		{
			_gravityZone.gameObject.SetActive(false);
			for (int i = 0; i < _entities.Count; ++i)
			{
				_entities[i].OnPressUp();
			}

			if (_entities.Count == 0)
			{
				Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
				worldPos.z = 0;
				var createPlanet = new Logic.Situation.CreatePlanetSituation(worldPos, random: true);
			}
			_entities.Clear();
		}
	}
}