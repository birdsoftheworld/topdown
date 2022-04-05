using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    public GameObject shieldEffect;

    public float overloadMax;
    public float overload;
    public bool sparking;
    public int waiting;

    void Awake()
    {
        overload = 0;

        sparking = false;

        waiting = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            int hits = 0;

            GameObject[] bullet;

            bullet = GameObject.FindGameObjectsWithTag("Projectile");
            GameObject closest = this.gameObject;
            float distance = Mathf.Infinity;
            Vector3 position = this.transform.position;

            if (overload > overloadMax)
            {
                sparking = true;
            }

            if (sparking == false)
            {
                foreach (GameObject go in bullet)
                {
                    if (go.GetComponent<Projectile>() != null)
                    {
                        if (go.GetComponent<Projectile>().bulletSpeed == 13)
                        {

                        }
                        else if (go.transform.parent == this.transform)
                        {
                            Vector2 bulletPos = go.transform.position;
                            Vector2 currentPos = this.transform.position;
                            Vector2 destination = currentPos - bulletPos;
                            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
                            Quaternion rotation = new Quaternion();
                            rotation.eulerAngles = new Vector3(0, 0, angle + 270);
                            transform.rotation = rotation;

                            go.transform.rotation = rotation;

                            go.GetComponent<Projectile>().bulletSpeed = 13;

                            go.transform.SetParent(null, true);
                        }
                        else if (Vector2.Distance(go.transform.position, this.transform.position) < 2)
                        {
                            go.GetComponent<Projectile>().bulletSpeed = 0;
                            go.GetComponent<Hittable>().safe = false;
                            go.transform.SetParent(this.transform, true);

                            GameObject sparks = Instantiate(shieldEffect, go.transform.position, this.transform.rotation);

                            overload += go.GetComponent<Projectile>().bulletDamage * go.GetComponent<Projectile>().bulletDamage;
                            hits += go.GetComponent<Projectile>().bulletDamage;
                        }
                    }
                }
            }
            else
            {
                GameObject sparks = Instantiate(shieldEffect, this.transform.position, this.transform.rotation);

                foreach (GameObject go in bullet)
                {
                    if (go.GetComponent<Projectile>() != null)
                    {
                        if (go.GetComponent<Projectile>().bulletSpeed == 16)
                        {

                        }
                        else if (go.transform.parent == this.transform)
                        {
                            Vector2 bulletPos = go.transform.position;
                            Vector2 currentPos = this.transform.position;
                            Vector2 destination = currentPos - bulletPos;
                            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
                            Quaternion rotation = new Quaternion();
                            rotation.eulerAngles = new Vector3(0, 0, angle + 90);
                            transform.rotation = rotation;

                            go.transform.rotation = rotation;

                            go.GetComponent<Projectile>().bulletSpeed = 16;

                            go.transform.SetParent(null, true);

                            if (overload > 0)
                            {
                                overload -= .2f;
                            }
                            else
                            {
                                waiting += 20;
                                //Debug.Log("shield down for " + waiting);
                                overload = 0;
                            }
                            if (hits < 1)
                            {
                                hits = 1;
                            }
                        }
                        else if (Vector2.Distance(go.transform.position, this.transform.position) < 10)
                        {
                            go.GetComponent<Projectile>().bulletSpeed = 0;
                            go.GetComponent<Hittable>().safe = false;
                            go.transform.SetParent(this.transform, true);

                            sparks = Instantiate(shieldEffect, go.transform.position, this.transform.rotation);
                            sparks = Instantiate(shieldEffect, this.transform.position, this.transform.rotation);
                        }
                    }
                }
            }

            if (hits > 0)
            {
                hits--;
            }
            else if (overload > 0)
            {
                overload -= .2f;

                if (overload == 0)
                {
                    sparking = false;
                }
            }
        }
    }
}
