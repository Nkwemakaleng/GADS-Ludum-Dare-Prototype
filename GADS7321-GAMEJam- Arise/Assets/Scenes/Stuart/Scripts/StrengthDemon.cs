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
    private float jumpForce = 10F;
    private bool isJumping = false;
    private float fallMultiplier = 2.0F;

    //INTERACTION VARIABLES
    private Vector3 currentDemonPos;
    private Vector3 pickupPos;
    private Vector3 oldPlatformPos;
    private bool platStored = false;
    private GameObject currentPlatform;
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
        currentDemonPos = transform.position;
        pickupPos = new Vector3(currentDemonPos.x, currentDemonPos.y + 1F, 0);

        if (Input.GetKeyDown(KeyCode.F)) { 
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

        //PLATFORM PICK UP
        if (!pickupInteractState && platStored && currentPlatform)
        {
            RevertPlatform();
        }

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

    private void RevertPlatform()
    {
        currentPlatform.tag = "Heavy_Lift";
        currentPlatform.GetComponent<BoxCollider>().excludeLayers = 0;
        currentPlatform.GetComponent<BoxCollider>().isTrigger = true;
        currentPlatform.gameObject.transform.parent = null;
        currentPlatform.gameObject.transform.position = oldPlatformPos;
        currentPlatform = null;
        platStored = false;
        currentPlatform = null;
    }

    void OnDestroy()
    {
        if (currentPlatform != null)
        {
            currentPlatform.transform.position = oldPlatformPos;
            currentPlatform.tag = "Heavy_Lift";
            currentPlatform.GetComponent<BoxCollider>().excludeLayers = 0;
            currentPlatform.GetComponent<BoxCollider>().isTrigger = true;
            pickupInteractState = false;
            platStored = false;
            currentPlatform.transform.parent = null;
            currentPlatform = null;
        }
    }

    //COLLISIONS
    void OnCollisionStay(Collision collision) 
    {
        //GROUND ENTER
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        //HEAVY PUSH INTERACTION
        if (collision.gameObject.CompareTag("Heavy_Push"))
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

            isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //HEAVY OBJECT PICKUP INTERACT
        if (collision.gameObject.CompareTag("Heavy_Lift") && pickupInteractState && !platStored)
        {
            //Debug.Log("Trigger Works");
            oldPlatformPos = collision.gameObject.transform.position;
            currentPlatform = collision.gameObject;

            currentPlatform.gameObject.transform.position = pickupPos;
            currentPlatform.gameObject.transform.parent = transform;

            currentPlatform.tag = "Ground";
            currentPlatform.GetComponent<BoxCollider>().isTrigger = false;
            currentPlatform.GetComponent<BoxCollider>().excludeLayers = 8;

            platStored = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //GROUND EXIT
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        //HEAVY PUSH INTERACTION
        if (collision.gameObject.CompareTag("Heavy_Push"))
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isGrounded = false;
        }
    }
}
