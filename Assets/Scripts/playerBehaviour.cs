using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour: MonoBehaviour
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

    // lives
    private int lives = 0;

    //health system
    private float maxHealth = 2000;

    [SerializeField]
    private float health = 2000;

    //damage over time timer
    private float dotTimer;

    private float dot;

    //stun time and how it ends
    private float stunned;

    private float stunHeath;

    //UI

    private void Start()
    {
        //seting direction of raycast directly down
        hitDir = new Vector3(0, -90, 0);

        //set max health
        maxHealth = health;

        //set health to max
        resetHealth();

        //set max health for healthbar
        gameObject.GetComponent<healthUI>().setMaxHealth(health, maxHealth);
    }

    private void Update()
    {
        if (stunned <= 0)
        {
            //run keyPressed
            keyPressed();

            //jump delay and jump
            if (jumpPressed == true)
            {
                if (jumpDelay <= 0)
                {
                    //jumps
                    rb.AddForce(transform.up * jumpHeight);
                    jumpPressed = false;
                }
                else
                {
                    jumpDelay -= Time.deltaTime;
                }
            }

            if (dotTimer > 0)
            {
                doter();
                dotTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (stunHeath != health)
            {
                stunned = 0;
            }
            stunned -= Time.deltaTime;
        }

        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        death();
    }

    //keyPressed
    private void keyPressed()
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
            if (hit.transform.tag == "jumpGround" && Input.GetKey(jump))
            {
                //if pressing jump key
                if (noJump <= 0)
                {
                    //set timer
                    noJump = (maxJumpDelay + 0.15f);
                    jumpDelay = maxJumpDelay;
                    jumpPressed = true;
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
    private void lookRight()
    {
        //Make look left
        transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
    }

    //lookLeft
    private void lookLeft()
    {
        //Make look right
        transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
    }

    //for new lives
    private void resetHealth()
    {
        health = maxHealth;
    }

    //takes damage
    public void damager(float damage)
    {
        health -= damage;
        gameObject.GetComponent<healthUI>().setHealth(health);
    }

    //sets damage over time
    public void dotStart(float dotTime, float dotDam)
    {
        dotTimer = dotTime;
        dot = dotDam;
    }

    //applys damage over time
    private void doter()
    {
        health -= (dot * Time.deltaTime);
        gameObject.GetComponent<healthUI>().setHealth(health);
    }

    //stun
    public void stun(float stunTime)
    {
        stunned = stunTime;
        stunHeath = health;
    }

    //death
    private void death()
    {
        if (health <= 0)
        {
            if (lives == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                lives--;
                resetHealth();
                gameObject.GetComponent<healthUI>().setMaxHealth(health, maxHealth);
            }
        }
    }
}
