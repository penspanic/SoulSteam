using System.Collections;
using Logic.Entity;
using Logic.Situation;
using UnityEngine;
using Utility;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsGameProcessing { get; private set; }
	public override void Init()
	{
	}

	public void StartGame()
	{
		IsGameProcessing = true;
		var firstPlanetCreation = new CreatePlanetSituation();
		GameObject.FindObjectOfType<DustGenerator>().Do();
	}

	private void Update()
	{
		if (IsGameProcessing == true && UnityEngine.Input.GetKeyDown(KeyCode.Escape) == true)
		{
			ResetGame();
		}
	}

	public void ResetGame()
	{
		StartCoroutine(ResetGameProcess());
	}

	private IEnumerator ResetGameProcess()
	{
		IsGameProcessing = false;
		Camera.main.GetComponent<Animator>().Play("GameReset");
		FindObjectOfType<UIManager>().PlayResetAnimation();
		yield return new WaitForSeconds(2f);
		EntityManager.Instance.DestroyAll();
	}
	

	public void EndGame()
	{
		IsGameProcessing = false;
	}
}
