using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour: MonoBehaviour
{
    //getting key binds

    //forward key
    [SerializeField]
    KeyCode forward;

    //backwards key
    [SerializeField]
    KeyCode backwards;

    //left key
    [SerializeField]
    KeyCode left;

    //right key
    [SerializeField]
    KeyCode right;

    //look left key
    [SerializeField]
    KeyCode lLeft;

    //look right key
    [SerializeField]
    KeyCode lRight;

    //jump key
    [SerializeField]
    KeyCode jump = KeyCode.W;

    //reference to rigidbody
    [SerializeField]
    private Rigidbody rb;

    private bool canJump;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotateSpeed;

    private Vector3 rotation;

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
    private float maxJumpDelay = 0.2f;

    private float jumpDelay;

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
        rotation = rb.rotation.eulerAngles;

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
            //move forward
            rb.AddForce(transform.forward * moveSpeed);
        }

        //if pressing backwards key
        if (Input.GetKey(backwards))
        {
            //move forward
            rb.AddForce(-transform.forward * moveSpeed);
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(-transform.right * (moveSpeed/2));

        }
        if (Input.GetKey(right))
        {
            rb.AddForce(transform.right * (moveSpeed/2));

        }

        //set look direction
        if (Input.GetKey(lLeft))
        {
            //look left
            rotation.y -= rotateSpeed;
            rb.rotation = Quaternion.Euler(rotation);

        }
        if (Input.GetKey(lRight))
        {
            //look right
            rotation.y += rotateSpeed;
            rb.rotation = Quaternion.Euler(rotation);

        }



        if (canJump == true)
        {
            //if pressing jump key
            if (noJump <= 0)
            {
                if (Input.GetKey(jump))
                {
                    //set timer
                    noJump = (maxJumpDelay + 0.15f);
                    jumpDelay = maxJumpDelay;
                    jumpPressed = true;
                }
            }

            else
            {
                //jump timer
                noJump -= Time.deltaTime;  
            }
        
        }
        // makes player fall faster
        else
        {
            rb.AddForce(-transform.up * gravity);
        }
    }

    // allow to jump
    public void makeJump (bool jumpable)
    {
        canJump = jumpable;
    }

    //hardLookRight
    private void hardLookRight()
    {
        //Make look left
        transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
    }

    //hardLookLeft
    private void hardLookLeft()
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
