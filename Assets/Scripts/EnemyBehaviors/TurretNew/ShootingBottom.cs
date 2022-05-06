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
            float myRotation = convert(this.transform.localRotation.eulerAngles.z);

            if (rotationTarget == this.transform.rotation.eulerAngles.z)
            {
                behavior = "fire";
            }
            else if (rotationTarget + 3 > myRotation && rotationTarget - 3 < myRotation)
            {
                for (int i = 0; Mathf.Abs(i) < 4; i = -1 * (i))
                {
                    this.transform.Rotate(0.0f, 0.0f, i, Space.World);
                    Debug.Log(i);

                    if (i > -1)
                    {
                        i++;
                    }


                    if (myRotation == rotationTarget)
                    {
                        break;
                    }

                }


                //this.transform.rotation = turretTop.transform.rotation;
            }

            if (rotationTarget == this.transform.rotation.eulerAngles.z)
            {
                behavior = "fire";
            }
            else
            {
                int myQ;
                int targQ;
                

                if (myRotation > 0)
                {
                    if (myRotation > 90)
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
                    if (myRotation > -90)
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

                //Debug.Log("I at " + myQ + " and they at " + targQ);

                if (myQ == targQ)
                {
                    if (myRotation > rotationTarget)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);
                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);
                    }
                }
                else if (myQ > targQ)
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

    public float convert(float fl)
    {
        fl += 180;
        if (fl > 360f)
        {
            fl -= 360;
        }
        fl = -1 * ((fl * -1) + 180);

        return fl;
    }

    public void target(float rotation)
    {
        rotationTarget = rotation;

        behavior = "target";
    }
}
