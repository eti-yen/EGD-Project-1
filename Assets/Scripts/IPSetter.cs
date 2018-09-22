using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IPSetter : MonoBehaviour
{
    public void OnStringSet(string address)
    {
        EventSingleton.GetInstance().SetIp(address);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
