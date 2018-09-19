using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            EventSingleton.GetInstance().SwapPlayer();
            SceneManager.LoadScene(0);
        }
    }
}
