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
    public GameObject gameoverPanel;
    public GameObject ingameUIPanel;
    public GameObject mainmenuButton;
    [SerializeField] private Text displayScoreText;
    [SerializeField] private Text displayScoreNum;

    Leaderboard board;
	int newScore = 0;

	// Use this for initialization
	void Start() 
	{
        ingameUIPanel.SetActive(true);
		nameList.enabled = false;
		scoreList.enabled = false;
        displayScoreText.enabled = false;
        displayScoreNum.enabled = false;
        gameoverPanel.SetActive(false);
		enterName.gameObject.SetActive(false);
		board = GetComponent<Leaderboard>();
	}

	public void AskName(int score)
	{
        gameoverPanel.SetActive(true);
		enterName.gameObject.SetActive(true);
        mainmenuButton.SetActive(true);
		newScore = score;
        displayScoreNum.text = "" + newScore;
	}

	public void AddScore(InputField nameField)
	{
		string name = nameField.text;
		int changeScore = board.AddScore(name, newScore);
		enterName.interactable = false;
		enterName.GetComponent<Image>().enabled = false;
		enterName.textComponent.enabled = false;
        displayScoreText.enabled = true;
        displayScoreNum.enabled = true;
	}

	public void DisplayLeaderboard()
	{
        ingameUIPanel.SetActive(false);
        gameoverPanel.SetActive(false);
		nameList.enabled = true;
		scoreList.enabled = true;
		nameList.text = board.GetNameList();
		scoreList.text = board.GetScoreList();
	}

    public void HideLeaderboard()
    {
        nameList.enabled = false;
        scoreList.enabled = false;
        gameoverPanel.SetActive(true);
    }
}
