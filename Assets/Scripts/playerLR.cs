using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLR : MonoBehaviour
{
    //getting key binds

    //forward key
    [SerializeField]
    KeyCode forward = KeyCode.D;

    //backwards key
    [SerializeField]
    KeyCode backwards = KeyCode.A;
    
    //reference to rigidbody
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed;



    void Update()
    {
        //slow down (a bit janky)

        if (rb.velocity.x != 0)
        {
            rb.AddForce(-transform.forward*2);
        }

        //run keyPressed
        keyPressed();
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
