using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDispersalWave : MonoBehaviour
{

    public Player player;
    public int recharge;

    public bool active;

    private int reduction = 0;
    public int reductionMax;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        active = false;

        reduction = 0;
    }

    void Update()
    {
        if (active == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player.waiting4 == 0)
                {
                    player.waiting4 = recharge;
                    active = true;
                }
            }

            if (this.transform.localScale.x > 1.1f)
            {
                this.transform.localScale -= new Vector3(.1f, .1f, 0f);
            }
            if (this.transform.localScale.x < 1.1f)
            {
                this.transform.localScale = new Vector3(1.1f, 1.1f, 0f);
            }
            
            if (reduction > 0)
            {
                reduction--;
            }            
             
        }
        else
        {
            if (this.transform.localScale.x < 9.1f)
            {
                this.transform.localScale += new Vector3(.1f, .1f, 0f);

                if (reduction < reductionMax)
                {
                    reduction++;
                }
            }
            else
            {
                active = false;
            }
        }
    }




    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Projectile>() != null)
        {
            coll.GetComponent<Collider2D>().GetComponent<Projectile>().bulletSpeed -= reduction;

            coll.GetComponent<Collider2D>().GetComponent<Hittable>().safe = false;

            /*Quaternion rotation = new Quaternion();

            rotation.eulerAngles = coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation.eulerAngles;

            rotation.eulerAngles += new Vector3(0, 0, 180);

            coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation = rotation;


            */
        }
    }
}
