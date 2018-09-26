using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTracker : MonoBehaviour
{
	[SerializeField] AudioSource hitSound;
	public int maxHealth;
	public int currentHealth;

	public delegate void Death();
	public Death onDeath;

    public Slider healthbar;
    public GameObject gameoverPanel;

	// Use this for initialization
	void Start()
	{
		currentHealth = maxHealth;
        healthbar.value = currentHealth;
		if (onDeath == null)
			onDeath = () => Destroy(this.gameObject); 
	}
	
	public void Damage(int damageDone)
	{
		hitSound.Play();
		currentHealth -= damageDone;
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            gameoverPanel.SetActive(true);
            onDeath();
        }
	}
}
