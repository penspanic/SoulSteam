using Spine.Unity;
using UnityEngine;

namespace Scene
{
	public class IntroScene : AbstractScene
	{
		[SerializeField]
		private SkeletonAnimation _introAnimation;

		public override void Enter(AbstractScene beforeScene)
		{
			base.Enter(beforeScene);

			
		}
	}
}