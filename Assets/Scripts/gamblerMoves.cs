using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamblerMoves : MonoBehaviour
{
    //sets attack A to Q
    [SerializeField]
    KeyCode attackA = KeyCode.Q;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float cooldown;

    //prefab for Gambler's card projectile
    [SerializeField]
    private GameObject card;

    // Update is called once per frame
    void Update()
    {
        keyPressed();
        cooldown -= Time.deltaTime;
    }
    
    void keyPressed ()
    {
        if (cooldown <= 0)
        {
            if (Input.GetKey(attackA))
            {
                //summon card at current position facing the player's direction
                Instantiate(card, rb.transform.position, rb.transform.rotation);
                cooldown = 1;
            }
        }
    }
}
