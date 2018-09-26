using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTracker : MonoBehaviour
{
	public int maxHealth;
	private int currentHealth;

	public delegate void Death();
	public Death onDeath;

	// Use this for initialization
	void Start()
	{
		currentHealth = maxHealth;
		if (onDeath == null)
			onDeath = () => Destroy(this.gameObject); 
	}
	
	void Damage(int damageDone)
	{
		currentHealth -= damageDone;
		if (currentHealth <= 0)
			onDeath();
	}
}
