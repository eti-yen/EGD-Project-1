using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

[RequireComponent(typeof(HPTracker))]
public class Done_PlayerController : MonoBehaviour
{
	bool dead = false;
	[SerializeField] Animator anim;
	[SerializeField] SendData sender;
	public int health;
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;
	private HPTracker hp;

	void Start()
	{
		anim.gameObject.SetActive(true);
		hp = GetComponent<HPTracker>();
		hp.onDeath = Die;
	}
	
	void Update()
	{
		if (dead)
		{
			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			return;
		}
		if (!dead)
		{
			if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource>().Play();
			}
		}
	}

	void FixedUpdate ()
	{
		if (!dead)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			anim.SetBool("Moving", moveHorizontal != 0f || moveVertical != 0f);

			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			GetComponent<Rigidbody>().velocity = movement * speed;

			GetComponent<Rigidbody>().position = new Vector3
			(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

			GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 90.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		}
	}

	void Die()
	{
		dead = true;
		anim.SetBool("Dead", true);
		sender.SendScore();
	}
}
