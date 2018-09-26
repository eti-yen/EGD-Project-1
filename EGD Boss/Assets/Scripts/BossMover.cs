using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMover : MonoBehaviour
{
	public float speed;

	void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.right * speed;
	}
}
