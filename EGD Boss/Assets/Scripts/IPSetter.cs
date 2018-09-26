using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IPSetter : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetIP(InputField ipField)
	{
		SendData.SetIP(ipField.text);
		SceneManager.LoadScene(1);
	}
}