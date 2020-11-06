using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBehaviour : MonoBehaviour
{
    //call rigidbody
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed = 1;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float despawnTimer;

    [SerializeField]
    private float damageAmount;

    [SerializeField]
    private float dotAmount;

    [SerializeField]
    private float dotTimer;

    [SerializeField]
    private float stunTimer;

    private void Update()
    {
        //move forward
        rb.AddForce(transform.forward * moveSpeed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        Destroy(gameObject, despawnTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if hits player damage it
        if (collision.gameObject.tag == "player")
        {
            var target = collision.gameObject.GetComponent<playerBehaviour>();
            target.damager(damageAmount);
            target.dotStart(dotTimer, dotAmount);
            target.stun(stunTimer);
        }
        Destroy(gameObject);
    }
}
