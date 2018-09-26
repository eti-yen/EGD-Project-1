using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
	[SerializeField] int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		HPTracker hp = other.gameObject.GetComponent<HPTracker>();
		if (hp)
		{
			hp.Damage(damage);
			Destroy(this.gameObject);
		}
	}
}
