using UnityEngine;

namespace Input
{
	public class SlideController : MonoBehaviour
	{
		[SerializeField]
		private InputController _inputController;

		private void Awake()
		{
			_inputController.OnSlide += OnSlide;
		}

		private void OnSlide(Vector3 startPoint, Vector3 endPoint)
		{
			Debug.Log($"OnSlide, {startPoint}->{endPoint}");
		}
	}
}