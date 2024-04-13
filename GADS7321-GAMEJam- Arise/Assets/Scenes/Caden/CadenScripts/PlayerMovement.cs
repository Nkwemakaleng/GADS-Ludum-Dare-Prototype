using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
    
        public Rigidbody rb;
        private bool isGrounded;
    
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            //Horizontal movement
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
            
            //Jumping
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    
        void OnCollisionEnter(Collision collision)
        {
            //Is player grounded?
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
}
