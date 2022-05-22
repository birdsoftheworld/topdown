using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{

    private Rigidbody2D rb2D;
    public Player player;

    public int swingCount;

    public AmmoTracker ammoCounter;

    private float angle;

    private int waitDef = 15;
    private int wait = 15;


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        //ammo = ammoCap;
        ammoCounter.define1("-");
        ammoCounter.define2("-");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        wait = waitDef;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }
    // Update is called once per frame
    /*void Update()
    {

    }*/

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 currentPos = this.transform.position;

        destination = destination - currentPos;

        Vector3 destinationN = destination.normalized;

        angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        if (player.waiting2 == 0)
        {
            if (swingCount > 0)
            {
                swingCount++;

                if (swingCount == 11)
                {
                    swingCount = 0;
                    player.waiting2 = wait;
                    wait = waitDef;
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                swingCount++;
            }
            else if (Input.GetMouseButton(1))
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                player.waiting = 5;

                player.slowDown -= 4;

                player.transform.gameObject.GetComponent<HealthTest>().iFrames += 6;

                wait = 30;
                player.body.AddRelativeForce(Vector2.down * player.moveSpeed * 500f);
                swingCount++;

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

                int changeX = (int)(Mathf.Cos(angle) * player.driftingTickMax);
                int changeY = (int)(Mathf.Sin(angle) * player.driftingTickMax);

                player.driftingTickX += changeX;
                player.driftingTickY += changeY;
            }

            this.transform.rotation = Quaternion.Euler(0, 0, angle + 90 + (swingCount -1) * 20f);
        }
    }
}
