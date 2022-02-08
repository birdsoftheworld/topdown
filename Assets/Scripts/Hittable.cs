using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player,
    Enemy,
    Object
}


public class Hittable : MonoBehaviour
{
    public Faction faction;

    private Health health;

    public bool safe = true;

    private void Start()
    {
        health = GetComponent<Health>();

        safe = true;
    }

    public bool CanHit(Faction other)
    {
        if (safe == false)
        {
            return true;
        }
        else
        {
            return other != faction /*|| faction == Faction.Enemy*/;
        }
    }

   /* public void Hit(float damage)
    {
        health.Hurt(damage);
    }*/
}
