using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidgetDemon : MonoBehaviour
{
    //OBJECT VARIABLES
    private DemonManager dm;
    private Rigidbody demonRB;

    //MOVEMENT VARIABLES
    private bool isGrounded;
    private float moveSpeed = 10F;
    private float jumpForce = 10F;
    private bool isJumping = false;
    private float fallMultiplier = 2.0F;

    //INTERACTION VARIABLES
    public bool pickupInteractState = false;

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
        //INTERACTIONS
        //Debug.Log(pickupInteractState);

        if (Input.GetKeyDown(KeyCode.F))
        {
            pickupInteractState = !pickupInteractState;
        }

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
        demonRB.AddForce(Physics.gravity);

        //JUMPING
        if ((jumpBuffTimeCounter > 0F && coyoteTimeCounter > 0F) && !isJumping)
        {
            if (!isJumping)
            {
                demonRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                Invoke("ResetJump", 0.4F);
            }
            jumpBuffTimeCounter = 0F;
        }

        //FALLING
        if (demonRB.velocity.y < 0F)
        {
            demonRB.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    private void ResetJump()
    {
        isJumping = false;
    }

    void OnDestroy()
    {

    }

    //COLLISIONS
    void OnCollisionStay(Collision collision)
    {
        //GROUND ENTER
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Heavy_Push"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //DESTROYS DEMON IF NOT IN LIT LIGHT
        if (collision.gameObject.CompareTag("Light") && !collision.gameObject.GetComponent<Light>().enabled)
        {
            if (transform.GetChild(1) != null)
            {
                transform.GetChild(1).parent = dm.gameObject.transform;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //DESTROYS DEMON IF NOT IN LIT LIGHT
        if (collision.gameObject.CompareTag("Light") && !collision.gameObject.GetComponent<Light>().enabled)
        {
            //DESTROYS DEMON IF NOT IN LIT LIGHT
            if (collision.gameObject.CompareTag("Light") && !collision.gameObject.GetComponent<Light>().enabled)
            {
                try
                {
                    transform.GetChild(1).parent = dm.gameObject.transform;
                }
                catch { }
                Destroy(this.gameObject);
            }
            }
    }

    void OnCollisionExit(Collision collision)
    {
        //GROUND EXIT
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Heavy_Push"))
        {
            isGrounded = false;
        }
    }
}
