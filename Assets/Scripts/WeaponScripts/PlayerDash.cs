using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    public Player player;

    public int recharge;

    public int dodgeTimer;

    public Sprite[] sprites;

    public int timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.slowDownMax -= 1;
        player.slowDown -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.waiting4 == 0)
            {
                //Debug.Log("zooom");
                player.body.AddRelativeForce(Vector2.down * player.moveSpeed * 500f);
                player.slowDown -= dodgeTimer;
                player.waiting4 = recharge;
                player.transform.gameObject.GetComponent<HealthTest>().iFrames += dodgeTimer;

                player.waiter4Changed = true;
            }
        }

        if (player.slowDown < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        else if (player.slowDown == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        else if (player.slowDown < player.slowDownMax)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }
}
