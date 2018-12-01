using System.Collections.Generic;
using UnityEngine;

namespace Input
{
	using UInput = UnityEngine.Input;
	public class InputController : MonoBehaviour
	{
		[SerializeField]
		private float _slideTouchArea;
	
		private enum InputState
		{
			None = 0,
			Sliding,
			Pinch,
		}
		
		private void Update()
		{
			List<Touch> touches = InputHelper.GetTouches();
			for (int i = 0; i < touches.Count; ++i)
			{
				Debug.Log($"Touch({i}) phase : {touches[i].phase}");
			}
		}
	}
}
