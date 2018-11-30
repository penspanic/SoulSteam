using UnityEngine;

namespace Scene
{
	public abstract class AbstractScene : MonoBehaviour
	{
		public SceneType Type => _type;
		[SerializeField]
		private SceneType _type;

		public virtual void Enter(AbstractScene beforeScene)
		{
			
		}
	}
}