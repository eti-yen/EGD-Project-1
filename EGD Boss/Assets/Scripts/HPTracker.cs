using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTracker : MonoBehaviour
{
	[SerializeField] AudioSource hitSound;
	public int maxHealth;
	public int currentHealth;

	public delegate void Death();
	public Death onDeath;

	// Use this for initialization
	void Start()
	{
		currentHealth = maxHealth;
		if (onDeath == null)
			onDeath = () => Destroy(this.gameObject); 
	}
	
	public void Damage(int damageDone)
	{
		hitSound.Play();
		currentHealth -= damageDone;
		if (currentHealth <= 0)
			onDeath();
	}
}
