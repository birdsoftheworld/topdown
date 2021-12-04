using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnife : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Player player;

    public int strikeCount;

    public AmmoTracker ammoCounter;

    private float angle;

    public int wait = 20;

    public int whichButton = 0;


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        ammoCounter.define1("-");
        ammoCounter.define2("-");

        //sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 currentPos = this.transform.position;

        destination = destination - currentPos;

        Vector3 destinationN = destination.normalized;

        angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        bool i = false;
        if (whichButton == 0)
        {
            if (player.waiting2 == 0)
            {
                i = true;
            }
        }
        else
        {
            if (player.waiting3 == 0)
            {
                i = true;
            }
        }

        if (i == true)
        {
            if (strikeCount > 0)
            {
                strikeCount++;

                if (strikeCount == 4)
                {
                    strikeCount = 0;
                    if (whichButton == 0)
                    {
                        player.waiting2 = wait;
                    }
                    else                    
                    {
                        player.waiting3 = wait;
                    }
                }
            }
            else if (Input.GetMouseButton(whichButton))
            {
                strikeCount++;
            }
            this.transform.localPosition = new Vector2(0f, (strikeCount * .07f));
            this.transform.localScale = new Vector3(1f, 1f + (strikeCount * .25f), 1f);
        }
    }
}
