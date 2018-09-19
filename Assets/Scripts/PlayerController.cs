using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float controlForce;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravityScale;
    [SerializeField] private float airMovement;
    [SerializeField] private string movementAxis;
    [SerializeField] private string jumpButton;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private float deathTimer;
    [SerializeField] private float deathPlane;
    
    private float axis;
    private bool jump;
    private Rigidbody2D rb;
    private bool airJump;
    private bool wallsAreFloors;
    private SpriteRenderer sr;
    private float hVel;
    
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = groundDrag;
        airJump = false;
        wallsAreFloors = false;
        sr = GetComponent<SpriteRenderer>();
        hVel = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        axis = Input.GetAxis(movementAxis);
        jump |= Input.GetButtonDown(jumpButton);
        if(axis > 0) sr.sprite = rightSprite;
        else if(axis < 0) sr.sprite = leftSprite;
    }
    
    void FixedUpdate()
    {
        rb.gravityScale = gravityScale;
        bool grounded = isGrounded();
        Vector2 force = (grounded ? controlForce : airMovement) * Vector2.right * axis;
        
        rb.drag = grounded ? groundDrag : airDrag;
        
        if(jump && (airJump || grounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
        
        hVel = rb.velocity.x;
        
        rb.AddForce(force * rb.mass);
        
        jump = false;
        
        if(transform.position.y < deathPlane)
        {
            StartCoroutine(Reset());
        }
    }
    
    public void SetAirJump(bool value)
    {
        airJump = value;
    }
    
    public void SetWallsAreFloors(bool value)
    {
        wallsAreFloors = value;
    }
    
    bool isGrounded() 
    {
        ContactPoint2D[] points = new ContactPoint2D[4];
        int len = rb.GetContacts(points);
        
        if(wallsAreFloors)
        {
            return len > 0;
        }
        
        rb.GetContacts(points);
        
        foreach(ContactPoint2D point in points)
        {
            if(point.normal.y > Mathf.Abs(point.normal.x))
            {
                return true;
            }
        }

        return false;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        bool transition = false;
        if (col.gameObject.tag == "goal")
        {
            StartCoroutine(Win());
            transition = true;
        }
        
        BlockSwitch block = col.gameObject.GetComponent<BlockSwitch>();
        
        if(block != null && block.IsKillBlock())
        {
            StartCoroutine(Reset());
            transition = true;
        }
        
        if(!transition)
        {
            rb.velocity = new Vector2(hVel, rb.velocity.y);
        }
    }
    
    IEnumerator Reset()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sr.color = Color.red;
        yield return new WaitForSeconds(deathTimer);
        EventSingleton.GetInstance().AddDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    IEnumerator Win()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(deathTimer);
        EventSingleton.GetInstance().Goal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
