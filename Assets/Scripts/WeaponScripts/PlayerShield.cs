using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public Player player;

    public int recharge;

    public int dodgeTimer;

    public Sprite inactive;
    public Sprite active;        

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.GetComponent<HealthTest>().maxHealth += 3;
        player.GetComponent<HealthTest>().curHealth += 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.waiting4 == 0)
            {
                player.waiting4 = recharge;
                player.transform.gameObject.GetComponent<HealthTest>().iFrames += dodgeTimer;
            }
        }

        if (player.transform.gameObject.GetComponent<HealthTest>().iFrames > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = active;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = inactive;
        }
    }
}
