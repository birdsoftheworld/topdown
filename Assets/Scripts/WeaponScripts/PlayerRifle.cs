using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifle : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;
    public GameObject tracerPrefab;

    public float bulletSpeed = 15;
    public int bulletDamage = 3;
    public int ammoCap = 5;
    public int ammo;

    public int waiting = 0;

    private int fireTickMax = 30;
    private int fireTick = 30;

    private int specialTickMax = 45;
    private int specialTick = 45;

    private void Start()
    {
        ammo = ammoCap;
        //bulletOrigin = this.transform;
    }

    private void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (ammo > 0)
                {
                    if (fireTick == fireTickMax)
                    {
                        ammo--;

                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                        bullet.bulletSpeed = bulletSpeed;
                        bullet.bulletFaction = bulletFaction;
                        bullet.bulletDamage = bulletDamage;
                        //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);

                        fireTick = 0;
                    }
                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetMouseButton(1))
            {
                if (ammo > 0)
                {
                    if (specialTick == specialTickMax)
                    {
                        ammo--;

                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(tracerPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        Tracer flare = clone.gameObject.GetComponent("Tracer") as Tracer;

                        flare.bulletSpeed = bulletSpeed;
                        flare.bulletFaction = bulletFaction;
                        //bullet.bulletDamage = bulletDamage;
                        //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);

                        specialTick = 0;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap)
                {
                    ammo++;
                    Debug.Log("Reloaded one bullet! You have " + ammo + " bullets loaded.");
                    waiting = 50;
                }
                else
                {
                    Debug.Log("You're already at maximum ammo!");
                }
            }

        }
        if (fireTick != fireTickMax)
        {
            fireTick++;
        }
        if (specialTick != specialTickMax)
        {
            specialTick++;
        }
    }
}
