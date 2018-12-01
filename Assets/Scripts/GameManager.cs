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

	public void EndGame()
	{
		IsGameProcessing = false;
	}
}
