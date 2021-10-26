using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarbine : MonoBehaviour
{
    //public Rigidbody2D bullet;



    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;

    public float bulletSpeed = 12;
    public int bulletDamage = 3;
    public int ammoCap = 5;
    public int ammo;

    public int waiting = 0;

    private int fireTickMax = 30;
    private int burstTickMax = 5;
    public int burstTick = 5;
    public int fireTick = 5;

    public Player player;

    private void Start()
    {
        ammo = ammoCap;
        fireTick = fireTickMax / 10;
        //bulletOrigin = this.transform;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (player.waiting2 > 0)
        {
            player.waiting2--;
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

            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap)
                {
                    ammo = ammoCap;
                    Debug.Log("Rrrrreloading!");
                    player.waiting2 = 150;
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
            player.waiting2 = 50;
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

    }
}
