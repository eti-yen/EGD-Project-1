using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    /*Variables*/
    public int sceneName;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject controlPanel;
    bool active = false;

    /*Functions*/
    public void Controls()
    {
        if (active == false)
        {
            controlPanel.SetActive(true);
            active = true;
        }
        else
        {
            controlPanel.SetActive(false);
            active = false;
        }
    }

    public void Play ()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit ()
    {
        Application.Quit();
    }
}
