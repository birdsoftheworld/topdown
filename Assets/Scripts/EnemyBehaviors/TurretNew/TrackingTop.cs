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

    public int waiting;

    public Sprite unlocked;
    public Sprite locked;

    public int searchingTick;
    public int searchingTickMax;

    // Start is called before the first frame update
    void Start()
    {
        behavior = "search";

        player = GameObject.FindGameObjectWithTag("Player");

        waiting = 0;

        searchingTick = 0;
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;

            if (waiting > 5)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = locked;
            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = unlocked;

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
                    //Debug.Log(this.transform.eulerAngles.z);


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



                    //////////////////////////////////
                    ////
                    myQ += 0;
                    if (myQ > 4)
                    {
                        myQ -= 4;
                    }
                    //Debug.Log(myQ + "    " + playerQ);


                    //////////////////////////////////////

                    //Debug.Log(passTarg + "    " + targetRotation);
                    //Debug.Log(playerQ + "   " + myQ);
                    if (myQ == playerQ)
                    {
                        //Debug.Log(passTarg + "    " + targetRotation);
                        if (passTarg > targetRotation)
                        {
                            this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                        }
                        else
                        {
                            this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                        }
                    }
                    /*else if (myQ == 3)
                    {
                        if (playerQ == 3)
                        {
                            this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                        }
                    }*/

                    else if ((myQ + 2) == playerQ || (myQ - 2) == playerQ)
                    {
                        //Debug.Log("b");

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
                        //Debug.Log("c");

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
                        //Debug.Log("d");

                        if (playerQ == 4 || playerQ == 3)
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
                        //Debug.Log("e");

                        this.transform.Rotate(0.0f, 0.0f, -3f, Space.World);
                    }
                    else
                    {
                        //Debug.Log("f");

                        this.transform.Rotate(0.0f, 0.0f, 3f, Space.World);
                    }


                    if (checkSightToPlayer() == false)
                    {
                        if (turretBase.GetComponent<ShootingBottom>().behavior != "fire")
                        {
                            turretBase.GetComponent<ShootingBottom>().behavior = "idle";
                        }


                        searchingTick++;
                        if (searchingTick == searchingTickMax)
                        {
                            behavior = "search";

                            searchingTick = 0;
                        }


                    }

                }
            }
        }
    }

    private bool checkSightToPlayer()
    {

        int layerMask = 1 << 0;
        RaycastHit2D hit;

        //Vector3 forwards = transform.TransformPoint(new Vector3(sight.transform.localPosition.x, sight.transform.localPosition.y - .5f, 0f));
        //Debug.Log(forwards);

        Vector3 forwards = sight.transform.GetChild(0).transform.position;

        hit = Physics2D.Raycast(sight.transform.position, forwards - sight.transform.position, Mathf.Infinity, layerMask);

        //hit = Physics2D.Raycast(transform.position, sight.transform.position - this.transform.position, Mathf.Infinity, layerMask);
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
