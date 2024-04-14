using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemonManager : MonoBehaviour
{
    //CAMERA
    [SerializeField] Camera cam;
    Vector3 camPos;

    //PLAYER MOVEMENT VARIABLES
    private float moveSpeed = 5F;
    private float jumpForce = 10F;
    private bool isGrounded;
    private bool isJumping = false;
    private float fallMultiplier = 2.0F;

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
    public bool activeController = true;  //Controls whether player or demon is being controlled (false - player, true - demon)

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();      //PLAYER MODEL MUST BE 1st CHILD
        camPos = cam.transform.position;
    }

    //DEMON + PLAYER CONTROL
    void Update()
    {
        //CAMERA CONTROL
        if (isSpawned && activeController)
        {
            cam.transform.parent = activeDemon.transform;
            cam.transform.localPosition = camPos;
            
        }
        else {
            cam.transform.parent = this.transform;
            cam.transform.localPosition = camPos;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        //Debug.Log(playerRB.velocity);
        //WHETHER DEMON IS ACTIVE - ACTIVATING DEMON IS LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            demonActive = !demonActive;
            moveInput = 0;
        }

        //TOGGLES BETWEEN CONTROLLING DEMON AND PLAYER
        if (Input.GetMouseButtonDown(1))
        {
            activeController = !activeController;
            moveInput = 0;
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
            //Debug.Log("Right Pressed");

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
        else {
            playerRB.velocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        playerRB.AddForce(Physics.gravity);

        //JUMPING
        if ((jumpBuffTimeCounter > 0F && coyoteTimeCounter > 0F) && !isJumping)
        {
            if (!activeController && playerRB != null)
            {
                if (!isJumping)
                {
                    playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                    Invoke("ResetJump", 0.3F);
                }       
            }
            jumpBuffTimeCounter = 0F;
        }

        //FALLING
        if (playerRB.velocity.y < 0F)
        {
            playerRB.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    //COLLISIONS
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Heavy_Lift") || collision.gameObject.CompareTag("Heavy_Push"))
        {
            isGrounded = true;
        }

        //RESETS SCENE IF HIT HAZARD
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("You died bruh");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Heavy_Lift") || collision.gameObject.CompareTag("Heavy_Push"))
        {
            isGrounded = false;
        }
    }

    private void ResetJump() {
        isJumping = false;
    }

    //INSTANTIATES DEMON IN FRONT OF PLAYER
    public void spawnDemon() {
        activeDemon = Instantiate(demons[demonIndex], new Vector3(transform.position.x + 1F, transform.position.y, 0F), Quaternion.identity);
    }

    //DELETES DEMON + REVERTS CONTROL VARIABLES
    public void deleteDemon() {
        cam.transform.parent = this.transform;
        activeController = false;
        isSpawned = false;
        Destroy(activeDemon);
    }
}