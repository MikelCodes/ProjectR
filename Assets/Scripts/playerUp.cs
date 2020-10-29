using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUp : MonoBehaviour
{
    //reference to rigidbody
    [SerializeField]
    private Rigidbody rb;

    //jump key
    [SerializeField]
    KeyCode jump = KeyCode.W;

    //distance of raycast
    [SerializeField]
    private float groundDist;

    //seting the height of the jump
    [SerializeField]
    private float jumpHeight = 300;

    // set jump timer
    private float noJump;

    //making a vector 3 for direction of ground
    private Vector3 hitDir;


    private void Start()
    {

    }

    void Update()
    {
        //seting direction of raycast directly down
        hitDir = new Vector3(0, -90, 0);

        //jump timer
        if (noJump > 0)
        {
            noJump -= Time.deltaTime;
        }


        //if pressing jump key
        if (Input.GetKey(jump))
        {
            if (noJump <= 0)
            {
                //run void canJump
                canJump();

                //set timer
                noJump = 0.25f;
            }
        }
    }

    void canJump()
    {
        //stores raycast hit data in 'hit'
        RaycastHit hit;

        //runs raycast from player position looking down. Saves data in hit and will only go as far as before jump.
        if (Physics.Raycast(rb.transform.position, hitDir, out hit, groundDist))
        {
            //if what is hit has the "jumpGround" tag player can jump
            if (hit.transform.tag == "jumpGround")
            {
                //jumps
                rb.AddForce(transform.up * jumpHeight);
            }
        }
        //else it slows down air movement (a bit janky)
        else
        {

             rb.AddForce(-transform.forward * 100);
            
        }
    }
}
