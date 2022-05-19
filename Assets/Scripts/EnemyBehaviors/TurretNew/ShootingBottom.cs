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

    int waiting = 0;

    public int bulletSpeed;
    public int bulletDamage;
    public GameObject bulletPrefab;

    public int burstTick;
    public int burstMax;

    public Sprite healthy;
    public Sprite damaged;
    public HealthTest myHealth;

    public GameObject wreckPrefab;
    private GameObject levelGen;

    public GameObject armor;

    // Start is called before the first frame update
    void Start()
    {
        behavior = "idle";

        player = GameObject.FindGameObjectWithTag("Player");

        this.transform.parent.rotation = new Quaternion(0f, 0f, 0f, 0f);

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
    }

    void Update()
    {
        if (myHealth.curHealth <= 0)
        {
            levelGen.GetComponent<LootController>().Drop(this.transform.position, 1, 6, 2);

            GameObject wreck = Instantiate(wreckPrefab, this.transform.position, this.transform.rotation);
            wreck.GetComponent<WreckBehavior>().setSprite(5);
            wreck.gameObject.SetActive(true);

            armor.transform.SetParent(wreck.transform, true);

            armor.GetComponent<ArmorNew>().enabled = false;

            Destroy(this.transform.parent.gameObject);
        }
        else if (myHealth.justHalved == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = damaged;
        }
        else if (myHealth.justThirded == true)
        {
            this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            myHealth.justThirded = false;
        }
    }

    void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            if (behavior == "fire")
            {
                Shoot();
                waiting = 5;
                turretTop.GetComponent<TrackingTop>().waiting = 10;
                burstTick++;
                if (burstTick == burstMax)
                {
                    behavior = "target";
                    waiting = 30;
                    turretTop.GetComponent<TrackingTop>().waiting = 30;
                    burstTick = 0;
                }
            }
            else if (behavior == "idle")
            {

            }
            else if (behavior == "target")
            {
                float myRotation = convert(this.transform.localRotation.eulerAngles.z);

                //Debug.Log(rotationTarget - 3 < myRotation);

                if (rotationTarget == myRotation)
                {
                    behavior = "fire";
                    waiting = 5;
                    turretTop.GetComponent<TrackingTop>().waiting = 10;
                }
                else if (rotationTarget + 3 > myRotation && rotationTarget - 3 < myRotation)
                {
                    //Debug.Log("   ");
                    /*for (int i = 0; Mathf.Abs(i) < 4; i = -1 * (i))
                    {
                        this.transform.Rotate(0.0f, 0.0f, i, Space.World);
                        //Debug.Log(i);

                        if (i > -1)
                        {
                            i++;
                        }


                        if (myRotation == rotationTarget)
                        {
                            break;
                        }

                    }*/

                    this.transform.Rotate(0.0f, 0.0f, rotationTarget - myRotation, Space.World);

                    behavior = "fire";
                    waiting = 5;
                    turretTop.GetComponent<TrackingTop>().waiting = 10;

                    //this.transform.rotation = turretTop.transform.rotation;
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

                    //////////////////
                  /*  if (myQ == 1)
                    {
                        myQ = 4;
                    }
                    else
                    {
                        myQ--;
                    }*/
                    ///////////////////



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
                    else if ((myQ + 2) == targQ || (myQ - 2) == targQ)
                    {
                        if (Mathf.Abs(myQ) + Mathf.Abs(targQ) > 180)
                        {
                            this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, -90f, Space.World);
                        }
                        else
                        {
                            this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, 90f, Space.World);

                        }
                    }
                    else if (myQ == 1)
                    {
                        if (targQ == 4)
                        {
                            this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, -90f, Space.World);

                        }
                        else
                        {
                            this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, 90f, Space.World);

                        }
                    }
                    else if (myQ == 4)
                    {
                        if (targQ == 4 || targQ == 3)
                        {
                            this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, -90f, Space.World);

                        }
                        else
                        {
                            this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);

                            this.transform.Rotate(0.0f, 0.0f, 90f, Space.World);

                        }
                    }
                    else if (myQ > targQ)
                    {
                        this.transform.Rotate(0.0f, 0.0f, -2f, Space.World);

                        this.transform.Rotate(0.0f, 0.0f, -90f, Space.World);

                    }
                    else
                    {
                        this.transform.Rotate(0.0f, 0.0f, 2f, Space.World);

                        this.transform.Rotate(0.0f, 0.0f, 90f, Space.World);

                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject clone = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = (Faction)1;
        bullet.bulletDamage = bulletDamage;
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

        if (behavior != "fire")
        {
            behavior = "target";
        }
    }
}
