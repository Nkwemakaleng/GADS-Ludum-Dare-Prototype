using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthDemon : MonoBehaviour
{
    //VARIABLES
    DemonManager dm;
    Rigidbody demonRB;

    public bool isGrounded;
    public float moveSpeed = 5F;
    private float jumpForce = 10F;
    
    private void Awake()
    {
        dm = GameObject.FindGameObjectWithTag("Player").GetComponent<DemonManager>();
        demonRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(demonRB.velocity);

        //DEMON JUMPING
        if (dm.activeController) {
            float moveInput = Input.GetAxisRaw("Horizontal");
            demonRB.velocity = new Vector3(moveInput * moveSpeed, demonRB.velocity.y, 0);

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
            {
                demonRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }        
    }

    //COLLISIONS
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
