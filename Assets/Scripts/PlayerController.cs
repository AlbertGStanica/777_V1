using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBod;
    [SerializeField]private float moveSpeed;
    
    
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool canDoublejump;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize values
        moveSpeed = 7;
        jumpForce = 10;

        rigidBod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Animate();
    }

    private void Move()
    {
        rigidBod.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBod.velocity.y);
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rigidBod.velocity = new Vector2(rigidBod.velocity.x, jumpForce);
                canDoublejump = true;
            }
            else
            {
                if (canDoublejump)
                {
                    rigidBod.velocity = new Vector2(rigidBod.velocity.x, jumpForce/2);
                    canDoublejump = false;
                }
            }
        }
        
    }

    private void Animate()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(rigidBod.velocity.x));

        if(rigidBod.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        } else if(rigidBod.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

    }


}
