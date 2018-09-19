using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTracker : MonoBehaviour {
    
    [SerializeField] private Vector2 scrollDistance;
    [SerializeField] private Vector2 scrollSpeed;
    
    private PlayerController tracked;
    private Rigidbody2D trackedRB;
    
    // Use this for initialization
    void Awake()
    {
        tracked = (PlayerController)FindObjectOfType(typeof(PlayerController));
        trackedRB = tracked.GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(tracked.transform.position.x - transform.position.x > scrollDistance.x && trackedRB.velocity.x > 0)
        {
            transform.position += new Vector3(scrollSpeed.x * Time.deltaTime, 0);
        }
        
        if(tracked.transform.position.x - transform.position.x < -scrollDistance.x && trackedRB.velocity.x < 0)
        {
            transform.position += new Vector3(-scrollSpeed.x * Time.deltaTime, 0);
        }
        
        if(tracked.transform.position.y - transform.position.y > scrollDistance.y && trackedRB.velocity.y > 0)
        {
            transform.position += new Vector3(0, scrollSpeed.y * Time.deltaTime);
        }
        
        if(tracked.transform.position.y - transform.position.y < -scrollDistance.y && trackedRB.velocity.y < 0)
        {
            transform.position += new Vector3(0, -scrollSpeed.y * Time.deltaTime);
        }
    }
}
