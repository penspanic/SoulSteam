using UnityEngine;

namespace Input
{
	public class PressController : MonoBehaviour
	{
		[SerializeField]
		private InputController _inputController;

		private void Awake()
		{
			_inputController.OnPressStart += OnPressStart;
			_inputController.OnPressUp += OnPressUp;
		}

		private void OnPressStart(Vector3 pos)
		{
		}

		private void OnPressUp(Vector3 pos)
		{
		}
	}
}