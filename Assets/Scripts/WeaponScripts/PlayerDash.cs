using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    public Player player;

    public int recharge;

    public int dodgeTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            }
        }
    }
}
