using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnButton : MonoBehaviour
{
    [SerializeField] private string scene;
    
    public void OnButton()
    {
        SceneManager.LoadScene(scene);
    }
}
