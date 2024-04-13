using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthDemon : MonoBehaviour
{
    //OBJECT VARIABLES
    private DemonManager dm;
    private Rigidbody demonRB;

    //MOVEMENT VARIABLES
    private bool isGrounded;
    private float moveSpeed = 5F;
    private float jumpForce = 7F;
    private bool isJumping = false;

    //COYOTE TIMER
    private float coyoteTime = 0.1F;
    private float coyoteTimeCounter;

    //JUMP BUFFER
    private float jumpBuffTime = 0.2F;
    private float jumpBuffTimeCounter;

    private void Awake()
    {
        dm = GameObject.FindGameObjectWithTag("Player").GetComponent<DemonManager>();
        demonRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //DEMON MOVEMENT
        if (dm.activeController) 
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            demonRB.velocity = new Vector3(moveInput * moveSpeed, demonRB.velocity.y, 0);

            //PLAYER MOVEMENT
            //COYOTE JUMP
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            //JUMP BUFFER
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
            {
                jumpBuffTimeCounter = jumpBuffTime;
            }
            else
            {
                jumpBuffTimeCounter -= Time.deltaTime;
            }

            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)) && demonRB.velocity.y > 0F)
            {
                coyoteTimeCounter = 0F;
            }
        }        
    }

    private void FixedUpdate()
    {
        //JUMPING
        if ((jumpBuffTimeCounter > 0F && coyoteTimeCounter > 0F) && !isJumping)
        {
            
                if (!isJumping)
                {
                    demonRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                    Invoke("ResetJump", 1);
                }
            
            jumpBuffTimeCounter = 0F;
        }
    }

    private void ResetJump()
    {
        isJumping = false;
    }

    //COLLISIONS
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
