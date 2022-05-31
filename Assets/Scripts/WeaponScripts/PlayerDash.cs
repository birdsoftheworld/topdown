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

        player.driftingTickMax += 5;
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



                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                Vector2 currentPos = this.transform.position;

                destination = destination - currentPos;

                Vector3 destinationN = destination.normalized;

                float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;


                angle += 180;
                if (angle > 360f)
                {
                    angle -= 360;
                }
                angle = -1 * ((angle * -1) + 180);


                if (angle < 0)
                {
                    angle = 360 + angle;
                }

                angle = angle * Mathf.PI / 180;

                int changeX = (int)(2 * Mathf.Cos(angle) * player.driftingTickMax);
                int changeY = (int)(2 * Mathf.Sin(angle) * player.driftingTickMax);

                player.driftingTickX += changeX;
                player.driftingTickY += changeY;
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
