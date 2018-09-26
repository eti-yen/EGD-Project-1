using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPTracker))]
[RequireComponent(typeof(Animator))]
public class BossAI : MonoBehaviour
{
	[SerializeField] Transform fireSpawn;
	[SerializeField] GameObject fire;
	[SerializeField] private int regenAmount;
	[SerializeField] private int regenThreshold;
	[SerializeField] private float highZ;
	[SerializeField] private float lowZ;
	[SerializeField] private float moveRate;
	[SerializeField] private float attackDelay = 5f;
	private const float switchThreshold = 0.1f;
	private HPTracker hp;
	private Animator animtaro;

	// Use this for initialization
	void Start()
	{
		animtaro = GetComponent<Animator>();
		animtaro.gameObject.SetActive(true);
		hp = GetComponent<HPTracker>();
		StartCoroutine(Move());
		StartCoroutine(Attack());
	}
	
	// Update is called once per frame
	void Update()
	{
		if (hp.currentHealth <= regenThreshold)
		{
			hp.currentHealth += regenAmount;
			SendData.timesRegenerated++;
		}
	}

	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(attackDelay);
			animtaro.SetTrigger("Attack");
			Instantiate(fire, fireSpawn.position, fireSpawn.rotation);
		}
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
