using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCycleLance : MonoBehaviour
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

    public AmmoTracker ammoCounter;

    private void Start()
    {
        ammo = ammoCap;
        fireTick = fireTickMax / 10;
        //bulletOrigin = this.transform;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //ammo = ammoCap;
        UpdateTracker();
    }

    private void UpdateTracker()
    {
        ammoCounter.define1(ammo.ToString());
        ammoCounter.define2(ammoCap.ToString());
        //ammoCounter.define3(player.lightAmmo.ToString());
        player.UpdateCheck();
    }

    private void FixedUpdate()
    {
        if (player.waiting2 == 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (ammo > 0 && burstTick > 0)
                {
                    if (fireTick == (fireTickMax / 10))
                    {
                        ammo--;
                        ammoCounter.define1(ammo.ToString());

                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        FusionProjectile bullet = clone.gameObject.GetComponent("FusionProjectile") as FusionProjectile;

                        bullet.bulletSpeed = bulletSpeed;
                        bullet.bulletFaction = bulletFaction;
                        bullet.bulletDamage = bulletDamage;

                        fireTick = 0;

                        player.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed * -5f * (10 - burstTick));

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
                    int reloadAmount = 15 - ammo;
                    if (player.lightAmmo < reloadAmount)
                    {
                        reloadAmount = player.lightAmmo;
                    }
                    if (reloadAmount > 0)
                    {
                        ammo = ammo + reloadAmount;
                        player.lightAmmo -= reloadAmount;
                        //Debug.Log("Rrrrreloading!");
                        player.waiting2 = 150;
                        ammoCounter.define1(ammo.ToString());
                        player.UpdateCheck();
                        //ammoCounter.define3(player.lightAmmo.ToString());
                    }
                }
                else
                {
                    //Debug.Log("You're already at maximum ammo!");
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
