using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject endgamePanel;

	// Use this for initialization
	void Start () {
        endgamePanel.SetActive(false);
	}

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
