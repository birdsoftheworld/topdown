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
                        int i = swordDamage / 2;
                        health.DealDamage((int)Mathf.Floor(i + 0.1f));
                        health.DealDamage((int)Mathf.Ceil(i + 0.1f));
                    }
                }
            }
        }
    }
}
