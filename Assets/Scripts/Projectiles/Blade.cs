using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Faction swordFaction;
    public int swordDamage;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
        {
            Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();
            Faction hitFact = hitted.faction;

            if (hitted != null)
            {
                if (hitted.CanHit(swordFaction))
                {
                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(swordDamage, this.transform.position);
                    }
                }
            }

            if (coll.GetComponent<Collider2D>().GetComponent<Projectile>() != null)
            {
                //Debug.Log("ptwang!");

                coll.GetComponent<Collider2D>().GetComponent<Hittable>().safe = false;

                Quaternion rotation = new Quaternion();

                rotation.eulerAngles = coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation.eulerAngles;

                rotation.eulerAngles += new Vector3(0, 0, 180);

                coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation = rotation;
            }

        }
    }
}
