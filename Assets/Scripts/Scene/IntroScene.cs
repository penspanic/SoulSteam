using Spine;
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

			_introAnimation.gameObject.SetActive(true);
			_introAnimation.state?.SetAnimation(0, "Intro", false);
		}

		public override void Exit(AbstractScene nextScene)
		{
			base.Exit(nextScene);
			_introAnimation.gameObject.SetActive(false);
		}
	}
}