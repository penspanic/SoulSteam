using UnityEngine;

namespace Input
{
	public interface ITouchable
	{
		void OnPressDown();
		void OnPressUp();
		void OnDrag(Vector3 pos);
	}
}