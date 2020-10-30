using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    //getting key binds

    //forward key
    [SerializeField]
    KeyCode forward = KeyCode.D;

    //backwards key
    [SerializeField]
    KeyCode backwards = KeyCode.A;

    //jump key
    [SerializeField]
    KeyCode jump = KeyCode.W;

    //reference to rigidbody
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed;


    //distance of raycast
    [SerializeField]
    private float groundDist;

    //seting the height of the jump
    [SerializeField]
    private float jumpHeight = 1600;

    //setting the maximum speed of any character
    [SerializeField]
    private float maxSpeed = 30f;

    [SerializeField]
    private float gravity = 40f;

    // set jump timer
    private float noJump;

    [SerializeField]
    private float maxJumpDelay;

    private float jumpDelay;

    //making a vector 3 for direction of ground
    private Vector3 hitDir;

    private bool jumpPressed;

    void Start()
    {
        //seting direction of raycast directly down
        hitDir = new Vector3(0, -90, 0);
    }

    void Update()
    {
        //run keyPressed
        keyPressed();

        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        jumpDelay -= Time.deltaTime;

        if (jumpPressed == true)
        {
            if (jumpDelay <= 0)
            {
                //jumps
                rb.AddForce(transform.up * jumpHeight);
                jumpPressed = false;
            }
        }
    }

    //keyPressed
    void keyPressed()
    {
        //if pressing forward key
        if (Input.GetKey(forward))
        {
            //run lookRight
            lookRight();

            //move forward
            rb.AddForce(transform.forward * moveSpeed);
        }

        //if pressing backwards key
        if (Input.GetKey(backwards))
        {
            //run lookLeft
            lookLeft();

            //move forward
            rb.AddForce(transform.forward * moveSpeed);
        }


        //stores raycast hit data in 'hit'
        RaycastHit hit;

        //runs raycast from player position looking down. Saves data in hit and will only go as far as before jump.
        if (Physics.Raycast(rb.transform.position, hitDir, out hit, groundDist))
        {
            //if what is hit has the "jumpGround" tag player can jump
            if (hit.transform.tag == "jumpGround")
            {
                //if pressing jump key
                if (Input.GetKey(jump))
                {
                    if (noJump <= 0)
                    {
                        //set timer
                        noJump = (maxJumpDelay + 0.25f);
                        jumpDelay = maxJumpDelay;
                        jumpPressed = true;
                    }

                }
                else
                {
                    //jump timer
                    if (noJump > 0)
                    {
                        noJump -= Time.deltaTime;
                    }
                }
            }
        }
        // makes player fall faster
        else
        {
            rb.AddForce(-transform.up * gravity);
        }
    }

    //lookRight
    void lookRight()
    {
        //Make look left
        transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
    }

    //lookLeft
    void lookLeft()
    {
        //Make look right
        transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
    }

}
