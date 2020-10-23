using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    // Private Variables
    private Rigidbody2D rBody;
    private bool isGrounded = false;
    private bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    //Physics
    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        //Jump Code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
            GetComponent<Animator>().SetBool("Run_01", false);
            isRunning = false;
            GetComponent<Animator>().SetBool("Jump_01", true);
        }
        else if (!isGrounded)
        {
            GetComponent<Animator>().SetBool("Jump_01", false);
        }
        else if (isGrounded && !isRunning)
        {
            GetComponent<Animator>().SetBool("Run_01", true);
            isRunning = true;
        }
        else if (isRunning)
        {
            GetComponent<Animator>().SetBool("Run_01", false);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }
}
