using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToTitle : MonoBehaviour
{
    // Update is called once per frame
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Your score: " + EventSingleton.GetInstance().GetScore();
    }
    
    void Update()
    {
        if(Input.anyKeyDown)
        {
            EventSingleton.GetInstance().SwapPlayer();
            SceneManager.LoadScene("TitleScene");
        }
    }
}
