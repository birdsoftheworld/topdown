using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTop : MonoBehaviour
{
    private GameObject player;

    public string behavior;

    public GameObject turretBase;
    public GameObject sight;
    public GameObject pointer;


    // Start is called before the first frame update
    void Start()
    {
        behavior = "search";

        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {
        if (behavior == "search")
        {
            this.transform.Rotate(0.0f, 0.0f, -1f, Space.World);

            if (checkSightToPlayer())
            {
                behavior = "target";
            }
        }

        if (behavior == "target")
        {
            float passTarg = turretBase.GetComponent<ShootingBottom>().convert(this.transform.localRotation.eulerAngles.z);

            turretBase.GetComponent<ShootingBottom>().target(passTarg);

            if (checkSightToPlayer() == false)
            {
                //Debug.Log(Vector2.Angle(this.transform.position, player.transform.position));
               // Debug.Log(this.transform.eulerAngles.z);


                Vector2 difference = this.transform.position - player.transform.position;
                //float flip = (player.transform.position.x > this.transform.position.x) ? 1.0f : -1.0f;

                //float signX = (player.transform.position.x > this.transform.position.x) ? -90f : 0f;
                //float signY = (player.transform.position.y > this.transform.position.y) ? 90f : -0f;

                //Debug.Log(( Vector2.Angle(Vector2.right, difference) /*signX + signY + */));

                float playerQ = 0;
                float myQ = 0;

                float targetRotation = turretBase.GetComponent<ShootingBottom>().convert(pointer.transform.rotation.eulerAngles.z);

                if (player.transform.position.x > this.transform.position.x)
                {
                    if (player.transform.position.y > this.transform.position.y)
                    {
                        playerQ = 1;
                    }
                    else
                    {
                        playerQ = 4;
                    }
                }
                else
                {
                    if (player.transform.position.y > this.transform.position.y)
                    {
                        playerQ = 2;
                    }
                    else
                    {
                        playerQ = 3;
                    }
                }

                if (passTarg > 0)
                {
                    if (passTarg > 90)
                    {
                        myQ = 1;
                    }
                    else
                    {
                        myQ = 4;
                    }
                }
                else
                {
                    if (passTarg > -90)
                    {
                        myQ = 3;
                    }
                    else
                    {
                        myQ = 2;
                    }
                }
                //Debug.Log(passTarg + "    " + targetRotation);
                if (myQ == playerQ)
                {
                    if (passTarg > targetRotation)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                    }
                }
                else if ((myQ + 2) == playerQ || (myQ - 2) == playerQ)
                {
                    if (Mathf.Abs(myQ) + Mathf.Abs(playerQ) > 180)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                    }
                }
                else if (myQ == 1)
                {
                    if (playerQ == 4)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                    }
                }
                else if (myQ == 4)
                {
                    if (playerQ == 4)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                    }
                }
                else if (myQ > playerQ)
                {
                    this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                }
                else
                {
                    this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                }
            }
        }
    }

    private bool checkSightToPlayer()
    {
        int layerMask = 1 << 0;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, sight.transform.position - this.transform.position, Mathf.Infinity, layerMask);
        //if (hit != null)
        //{
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        /*}
        else
        {
            return false;
        }*/
    }
}
