using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Leaderboard))]
public class LeaderboardDisplay : MonoBehaviour 
{
	[SerializeField] InputField enterName;
	[SerializeField] private Text nameList;
	[SerializeField] private Text scoreList;

	Leaderboard board;
	int newScore = 0;

	// Use this for initialization
	void Start() 
	{
		nameList.enabled = false;
		scoreList.enabled = false;
		enterName.gameObject.SetActive(false);
		board = GetComponent<Leaderboard>();
	}

	public void AskName(int score)
	{
		enterName.gameObject.SetActive(true);
		newScore = score;
	}

	public void AddScore(InputField nameField)
	{
		string name = nameField.text;
		int changeScore = board.AddScore(name, newScore);
		enterName.interactable = false;
		enterName.GetComponent<Image>().enabled = false;
		enterName.textComponent.enabled = false;
		DisplayLeaderboard();
	}

	void DisplayLeaderboard()
	{
		nameList.enabled = true;
		scoreList.enabled = true;
		nameList.text = board.GetNameList();
		scoreList.text = board.GetScoreList();
	}
}
