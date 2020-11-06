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

    //prefab for Gambler's card projectile (all 4)
    [SerializeField]
    private GameObject card1, card2, card3, card4;

    private float cardNo;

    // Update is called once per frame
    private void Update()
    {
        keyPressed();
        cooldown -= Time.deltaTime;
    }

    private void keyPressed ()
    {
        if (cooldown <= 0)
        {
            if (Input.GetKey(attackA))
            {

            }
        }
    }

    private void attack1 ()
    {
        //picks one of the four suits
        cardNo = Random.Range(1, 5);

        //total speed < 3
        if (rb.velocity.magnitude < 3)
        {
            //which way are they facing
            if (rb.transform.rotation.y > 0)
            {
                //summon the random card at current position facing the player's direction
                if (cardNo == 1)
                {
                    Instantiate(card1, rb.transform.position + new Vector3(1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 2)
                {
                    Instantiate(card2, rb.transform.position + new Vector3(1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 3)
                {
                    Instantiate(card3, rb.transform.position + new Vector3(1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 4)
                {
                    Instantiate(card4, rb.transform.position + new Vector3(1, 0, 0), rb.transform.rotation);
                }
                cooldown = 1;
            }

            else
            {
                //summon the random card at current position facing the player's direction
                if (cardNo == 1)
                {
                    Instantiate(card1, rb.transform.position + new Vector3(-1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 2)
                {
                    Instantiate(card2, rb.transform.position + new Vector3(-1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 3)
                {
                    Instantiate(card3, rb.transform.position + new Vector3(-1, 0, 0), rb.transform.rotation);
                }
                if (cardNo == 4)
                {
                    Instantiate(card4, rb.transform.position + new Vector3(-1, 0, 0), rb.transform.rotation);
                }

                else
                {
                    if (rb.transform.rotation.y > 0)
                    {
                        //summon the random card at current position facing the player's direction
                        if (cardNo == 1)
                        {
                            Instantiate(card1, rb.transform.position + new Vector3(2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 2)
                        {
                            Instantiate(card2, rb.transform.position + new Vector3(2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 3)
                        {
                            Instantiate(card3, rb.transform.position + new Vector3(2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 4)
                        {
                            Instantiate(card4, rb.transform.position + new Vector3(2, 0, 0), rb.transform.rotation);
                        }
                    }
                    else
                    {
                        //summon the random card at current position facing the player's direction
                        if (cardNo == 1)
                        {
                            Instantiate(card1, rb.transform.position + new Vector3(-2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 2)
                        {
                            Instantiate(card2, rb.transform.position + new Vector3(-2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 3)
                        {
                            Instantiate(card3, rb.transform.position + new Vector3(-2, 0, 0), rb.transform.rotation);
                        }
                        if (cardNo == 4)
                        {
                            Instantiate(card4, rb.transform.position + new Vector3(-2, 0, 0), rb.transform.rotation);
                        }
                    }
                }
            }
        }
        cooldown = 1;
    }
}
