using Spine;
using Spine.Unity;
using UnityEngine;

namespace Scene
{
	public class IntroScene : AbstractScene
	{
		[SerializeField]
		private SkeletonAnimation _introAnimation;

		private bool _canChangeScene;

		public override void Enter(AbstractScene beforeScene)
		{
			base.Enter(beforeScene);

			_introAnimation.gameObject.SetActive(true);
			_introAnimation.AnimationState.Complete += OnAnimationStateComplete;
		}

        private void OnAnimationStateStart(TrackEntry trackEntry)
        {
            switch(trackEntry.Animation.name)
            {
                case "intro":
                    SoundManager.Instance.Play("BigBang");
                    break;
                case "outro":
                    SoundManager.Instance.Play("Un_Bigbang");
                    break;
            }
        }

		private void OnAnimationStateComplete(TrackEntry trackentry)
		{
			_introAnimation.AnimationState.End -= OnAnimationStateComplete;
			_introAnimation.state.SetAnimation(0, "idle", true);
			_canChangeScene = true;
		}

		private void Update()
		{
			if (IsActiveScene == true && _canChangeScene == true)
			{
				_canChangeScene = false;
				SceneTransaction.Instance.TransactionTo(SceneType.Title);
			}
		}

		public override void Exit(AbstractScene nextScene)
		{
			base.Exit(nextScene);
//			_introAnimation.gameObject.SetActive(false);
		}
	}
}