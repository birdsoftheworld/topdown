using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBottom : MonoBehaviour
{
    private GameObject player;

    public string behavior;

    public GameObject turretTop;

    public float rotationTarget;

    public int rotation;

    // Start is called before the first frame update
    void Start()
    {
        behavior = "idle";

        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        if (behavior == "idle")
        {

        }

        if (behavior == "target")
        {
            //Debug.Log(rotationTarget);

            //Debug.Log(this.transform.rotation.eulerAngles.z);


            if (rotationTarget + 2 > this.transform.rotation.eulerAngles.z && rotationTarget - 2 < this.transform.rotation.eulerAngles.z)
            {
                this.transform.rotation = turretTop.transform.rotation;
            }

            if (rotationTarget == this.transform.rotation.eulerAngles.z)
            {
                behavior = "fire";
            }
            else
            {
                int myQ;
                int targQ;

                Debug.Log(this.transform.rotation.eulerAngles.z);

                if (this.transform.rotation.eulerAngles.z > 0)
                {
                    if (this.transform.rotation.eulerAngles.z > 90)
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
                    if (this.transform.rotation.eulerAngles.z > -90)
                    {
                        myQ = 3;
                    }
                    else
                    {
                        myQ = 2;
                    }
                }

                if (rotationTarget > 0)
                {
                    if (rotationTarget > 90)
                    {
                        targQ = 1;
                    }
                    else
                    {
                        targQ = 4;
                    }
                }
                else
                {
                    if (rotationTarget > -90)
                    {
                        targQ = 3;
                    }
                    else
                    {
                        targQ = 2;
                    }
                }

                Debug.Log("I at " + myQ + " and they at " + targQ);

                if (myQ == targQ)
                {
                    if (this.transform.rotation.eulerAngles.z > rotationTarget)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);
                        //Debug.Log("-");
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);
                        //Debug.Log("-");
                    }
                }

                if (myQ > targQ)
                {
                    this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);
                }
                else
                {
                    this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);
                }


            }



        }

        if (behavior == "fire")
        {

        }
    }

    public void target(float rotation)
    {
        rotationTarget = rotation;

        behavior = "target";
    }
}
