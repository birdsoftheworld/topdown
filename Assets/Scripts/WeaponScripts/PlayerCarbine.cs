using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarbine : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;
    public GameObject flashbangPrefab;

    public float bulletSpeed = 12;
    public int bulletDamage = 3;
    public int ammoCap = 5;
    public int ammo;
    public int grenadeTimer = 100;

    public int waiting = 0;

    private int fireTickMax = 30;
    private int burstTickMax = 5;
    public int burstTick = 5;
    public int fireTick = 5;

    private int specialTickMax = 45;
    private int specialTick = 45;

    private void Start()
    {
        ammo = ammoCap;
        fireTick = fireTickMax / 10;
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
                if (ammo > 0 && burstTick > 0)
                {
                    if (fireTick == (fireTickMax / 10))
                    {
                        ammo--;

                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                        bullet.bulletSpeed = bulletSpeed;
                        bullet.bulletFaction = bulletFaction;
                        bullet.bulletDamage = bulletDamage;

                        fireTick = 0;

                        burstTick--;
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
                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(flashbangPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        Flashbang grenade = clone.gameObject.GetComponent("Flashbang") as Flashbang;

                        grenade.bulletSpeed = bulletSpeed;
                        grenade.countdownMax = grenadeTimer;
                        grenade.countdown = grenadeTimer;
                        grenade.bulletFaction = bulletFaction;
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
                    ammo = ammoCap;
                    Debug.Log("Rrrrreloading!");
                    waiting = 150;
                }
                else
                {
                    Debug.Log("You're already at maximum ammo!");
                }
                if (burstTick < burstTickMax) { burstTick++; }

            }
            else
            {
                if (burstTick < burstTickMax) { burstTick++; }
            }
        }

        if (burstTick > 0)
        {
            if (fireTick < (fireTickMax / 10))
            {
                fireTick++;
            }
        }
        else if (burstTick == 0)
        {
            burstTick = burstTickMax;
            waiting = 50;
            if (fireTick < (fireTickMax))
            {
                fireTick++;
            }
        }
        else
        {
            if (fireTick < fireTickMax)
            {
                fireTick++;
            }
        }
        if (specialTick != specialTickMax)
        {
            specialTick++;
        }
    }
}
