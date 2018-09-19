using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] private int maxTime;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            EventSingleton.GetInstance().SetTime(maxTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
