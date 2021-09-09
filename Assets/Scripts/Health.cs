using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float health;

    public UnityEvent OnHit;
    public UnityEvent OnHeal;

    private void Start()
    {
        OnHit = new UnityEvent();
        OnHeal = new UnityEvent();
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = value;
    }

    public void Hurt(float amount)
    {
        health = health - amount;
        OnHit.Invoke();
    }

    public void Heal(float amount)
    {
        health = health + amount;
        OnHeal.Invoke();
    }
}
