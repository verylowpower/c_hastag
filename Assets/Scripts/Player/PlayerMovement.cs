using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;  //same as public float moveSpeed = 10f; but safer
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer; //Getting the layer call groundLayer
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float WallJumpCoolDown;
    private float delay; //delay when the player isOnWall() method action too quick making the player haven't touch the wall but velocity allready set to 00
    private Rigidbody2D MyRBody;
    private Animator MyAnimator;
    private BoxCollider2D MyBoxCollider2D;
    private float getHorizontalInput;
    private int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        MyRBody = GetComponent<Rigidbody2D>();
        // Get the Rigidbody2D componemet from object player.

        MyAnimator = GetComponent<Animator>();
        MyBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(isOnWall());
        
    }

    private void FixedUpdate()
    {
        getHorizontalInput = Input.GetAxisRaw("Horizontal");
        // MyRBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // For vector y doesn't need to get input (or be changed) cause in this game we only go from left to right, 
        // Only if we change Rigidbody2D from dynamic to kinematic then it's a topdown game instead of side view game
        // This is a nutshell go look for type of body in Rigidbody2D

        

        //flip transform base on direction
        if (getHorizontalInput >= 0.01f)
            transform.localScale = Vector3.one;
        else if(getHorizontalInput <= -0.01f )
            transform.localScale = new Vector3(-1,1, 1);

        
        MyAnimator.SetBool("run", getHorizontalInput != 0); //Animation set value left and right
        MyAnimator.SetBool("grounded", isGrounded());  // Animtion set value for jump check if player is on the ground
        

        if (WallJumpCoolDown > 0.2f)
        {
            MyRBody.velocity = new Vector2(getHorizontalInput * moveSpeed, MyRBody.velocity.y); 
            // MyRBody.velocity is the same as power push the object in vector form

            if (!isGrounded() && isOnWall()) //checking if player is not on the ground and on the wall
            {
                
                delay += Time.deltaTime;
                //if(delay > 0.1f) { }
                MyRBody.gravityScale = 0.2f;
                MyRBody.velocity = new Vector2(0, 0);
            } else { MyRBody.gravityScale = 0.5f; /*delay = 0;*/ }

            if (Input.GetKey(KeyCode.Space))
            {
                if (isOnWall() && isGrounded()) // Fix problem when play is onground and stand next to wall
                {
                    return;
                }
                print("Space has been pressed");
                Jump();
                if(isOnWall())
                jumpCount++;
                
            }
        } else { WallJumpCoolDown += Time.deltaTime; }
    }

    private void Jump()
    {
        if(isGrounded())
        {
            MyRBody.velocity = new Vector2(MyRBody.velocity.x, jumpPower);
            isGrounded();
            MyAnimator.SetTrigger("jump");
            jumpCount = 0;
        } 
        else if(!isGrounded() && isOnWall()) 
        {
            if(getHorizontalInput == 0)
            {
                MyRBody.velocity = new Vector2(-(transform.localScale.x) * 2, 1.5f);
                // -Mathf.Sign(transform.localScale.x)
                // Sign gonna give 1 or -1 base on transform.localScale.x
                // if transform.localScale.x gave an negative value then be -1 and vice versa
                transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else 
            { 
                if (jumpCount < 2)
                {
                    MyRBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 0.5f, 2);
                }
                
            }
            WallJumpCoolDown = 0;
        }
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    
    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(MyBoxCollider2D.bounds.center, MyBoxCollider2D.bounds.size,0, Vector2.down,0.1f, groundLayer);
        // Need more information about how this method work??
        return rayCastHit.collider != null; // if rayCasetHit is not collieded with ground layer then return false, vice versa
    }

    private bool isOnWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(MyBoxCollider2D.bounds.center, MyBoxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.035f, wallLayer);
        // Need more information Vector2(transform.localScale.x,0)
        return rayCastHit.collider != null; // if rayCasetHit is not collieded with ground layer then return false, vice versa
        
    }

    public bool canAttack()
    {
        return getHorizontalInput == 0 && isGrounded() && !isOnWall();
    }
}
