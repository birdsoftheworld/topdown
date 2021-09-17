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

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public bool CanHit(Faction other)
    {
        return other != faction /*|| faction == Faction.Enemy*/;
    }

   /* public void Hit(float damage)
    {
        health.Hurt(damage);
    }*/
}
