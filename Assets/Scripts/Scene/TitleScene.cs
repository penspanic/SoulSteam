using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
	public class TitleScene : AbstractScene
	{
		[SerializeField]
		private Canvas _canvas;
		[SerializeField]
		private Button _startButton;
		[SerializeField]
		private Button _creditButton;

		private void Awake()
		{
			_startButton.onClick.AddListener(() => SceneTransaction.Instance.TransactionTo(SceneType.InGame));
			_creditButton.onClick.AddListener(() => SceneTransaction.Instance.TransactionTo(SceneType.Credit));
		}

		public void Update()
		{
			if (IsActiveScene == true && UnityEngine.Input.GetMouseButtonDown(0) == true)
			{
				SceneTransaction.Instance.TransactionTo(SceneType.InGame);
			}
		}

		public override void Enter(AbstractScene beforeScene)
		{
			base.Enter(beforeScene);

//			_canvas.gameObject.SetActive(true);
		}

		public override void Exit(AbstractScene nextScene)
		{
			base.Exit(nextScene);

//			_canvas.gameObject.SetActive(false);
		}
	}
}