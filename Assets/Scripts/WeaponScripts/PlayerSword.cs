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

        if (player.waiting2 > 0)
        {
            player.waiting2--;
        }
        else
        {
            if (swingCount > 0)
            {
                swingCount++;

                if (swingCount == 8)
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
                
                wait = 30;
                player.body.AddRelativeForce(Vector2.down * player.moveSpeed * 500f);
                swingCount++;
            }

            this.transform.rotation = Quaternion.Euler(0, 0, angle - 30 + swingCount * 30f);
        }
    }
}
