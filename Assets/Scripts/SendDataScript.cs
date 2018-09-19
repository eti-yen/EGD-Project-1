using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendDataScript : MonoBehaviour
{
    [SerializeField] private GameObject toEnable;
    
    public void OnDataWrite(string name)
    {
        EventSingleton.GetInstance().SetName(name);
        EventSingleton.GetInstance().SendEvents();
        
        toEnable.SetActive(true);
        Text text = toEnable.GetComponent<Text>();
        if(text != null) text.text = "Your score: " + EventSingleton.GetInstance().GetScore();
        gameObject.SetActive(false);
    }
}
