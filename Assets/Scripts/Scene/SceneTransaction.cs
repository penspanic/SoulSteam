using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Scene
{
	public enum SceneType
	{
		Undefined = 0,
		Intro,
		Credit,
		InGame,
	}
	public class SceneTransaction : MonoBehaviour
	{
		[SerializeField]
		private List<AbstractScene> _scenes = new List<AbstractScene>();

		private AbstractScene _currentScene;

		private void Awake()
		{
			TransactionTo(SceneType.Intro);
		}
	
		public void TransactionTo(SceneType type)
		{
			AbstractScene nextScene = _scenes.Find(s => s.Type == type);

			if (_currentScene != null)
			{
				_currentScene.Exit(nextScene);
			}

			nextScene.Enter(_currentScene);
			_currentScene = nextScene;
		}
	}
}