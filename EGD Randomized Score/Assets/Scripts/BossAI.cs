using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPTracker))]
public class BossAI : MonoBehaviour
{
	[SerializeField] private int regenThreshold;
	[SerializeField] private float highZ;
	[SerializeField] private float lowZ;
	[SerializeField] private float moveRate;
	private const float switchThreshold = 0.1f;
	private HPTracker hp;

	// Use this for initialization
	void Start()
	{
		hp = GetComponent<HPTracker>();
		StartCoroutine(Move());
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	IEnumerator Move()
	{
		bool movingUp = Mathf.Abs(highZ - transform.position.z) < Mathf.Abs(lowZ - transform.position.z);
		while (true)
		{
			if (movingUp)
			{
				transform.position = Vector3.Lerp(transform.position, transform.position + moveRate * Vector3.forward, 1.0f);
				movingUp = (highZ - transform.position.z) > switchThreshold;
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, transform.position - moveRate * Vector3.forward, 1.0f);
				movingUp = (transform.position.z - lowZ) <= switchThreshold;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
