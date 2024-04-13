using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonManager : MonoBehaviour
{
    //PLAYER MOVEMENT VARIABLES
    public float moveSpeed = 5F;
    public float jumpForce = 7F;
    public bool isGrounded;
    private bool isJumping = false;

    //COYOTE TIMER
    private float coyoteTime = 0.1F;
    private float coyoteTimeCounter;

    //JUMP BUFFER
    private float jumpBuffTime = 0.2F;
    private float jumpBuffTimeCounter;

    //OBJECT VARIABLES
    [SerializeField] GameObject[] demons;   
    GameObject activeDemon;
    Rigidbody playerRB;

    //MATERIAL VARIABLES
    [SerializeField] Material standardMaterial;
    [SerializeField] Material selectedMaterial;

    //STATE VARIABLES
    public int demonIndex = 0;     //Which demon is being summoned
    public bool demonActive = false;       //Whether demon is summoned or not
    private bool isSpawned = false;        //Checks whether demon has been spawned
    public bool activeController = false;  //Controls whether player or demon is being controlled (false - player, true - demon)

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();      //PLAYER MODEL MUST BE 1st CHILD
    }

    //DEMON + PLAYER CONTROL
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        //Debug.Log(playerRB.velocity);
        //WHETHER DEMON IS ACTIVE - ACTIVATING DEMON IS LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            demonActive = !demonActive;
        }

        //TOGGLES BETWEEN CONTROLLING DEMON AND PLAYER
        if (Input.GetMouseButtonDown(1))
        {
            activeController = !activeController;
        }

        //CHANGES WHICH DEMON IS SELECTED - CHANGE TO WORK WITH ARRAY
        //GOING LEFT
        if (Input.GetKeyDown("1"))
        {
            //Debug.Log("Left Pressed");
            if (demonIndex - 1 < 0)
            {
                demonIndex = demons.Length - 1;
            }
            else
            {
                demonIndex--;
            }
        }

        //GOING RIGHT
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("Right Pressed");

            if (demonIndex + 1 > demons.Length - 1)
            {
                demonIndex = 0;
            }
            else
            {
                demonIndex++;
            }
        }

        //CONTROLS DEMON SPAWN + DESPAWN
        if (demonActive)
        {
            if (!isSpawned)
            {
                spawnDemon();
                isSpawned = true;
            }
        }
        else
        {
            deleteDemon();
        }

        //PLAYER MOVEMENT
        //COYOTE JUMP
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //JUMP BUFFER
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            jumpBuffTimeCounter = jumpBuffTime;
        }
        else {
            jumpBuffTimeCounter -= Time.deltaTime;
        }

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)) && playerRB.velocity.y > 0F)
        {
            coyoteTimeCounter = 0F;
        }
            //MOVMENT
            if (!activeController)
            {
            playerRB.velocity = new Vector3(moveInput * moveSpeed, playerRB.velocity.y, 0);   
            }  
    }
    private void FixedUpdate()
    {
        //JUMPING
        if ((jumpBuffTimeCounter > 0F && coyoteTimeCounter > 0F) && !isJumping)
        {
            if (!activeController && playerRB != null)
            {
                if (!isJumping)
                {
                    playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                    Invoke("ResetJump", 1);
                }       
            }
            jumpBuffTimeCounter = 0F;
        }
    }

    //COLLISIONS
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ResetJump() {
        isJumping = false;
    }

    //INSTANTIATES DEMON IN FRONT OF PLAYER
    public void spawnDemon() {
        activeDemon = Instantiate(demons[demonIndex], new Vector3(transform.position.x + 3F, 0F, 0F), Quaternion.identity);
    }

    //DELETES DEMON + REVERTS CONTROL VARIABLES
    public void deleteDemon() {
        Destroy(activeDemon);
        activeController = false;
        isSpawned = false;
       
    }
}
