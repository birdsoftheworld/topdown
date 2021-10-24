using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPistol : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;
    public GameObject bulletPrefab;

    public float bulletSpeed = 12;
    public int bulletDamage = 3;
    public int ammoCap = 15;
    public int ammo;

    public int waiting = 0;

    private void Start()
    {
        ammo = ammoCap;
        //bulletOrigin = this.transform;
    }

    private void Update()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i > 5; i++)
                {
                    if (ammo > 0)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            ammo--;

                            Vector2 position = bulletOrigin.position;
                            GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                            clone.gameObject.SetActive(true);

                            Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                            bullet.bulletSpeed = bulletSpeed;
                            bullet.bulletFaction = bulletFaction;
                            bullet.bulletDamage = bulletDamage;
                        }
                    }
                    else
                    {
                        Debug.Log("You need to reload!");
                        i = 5;
                    }
                }
                waiting = 200;
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap)
                {
                    ammo++;
                    Debug.Log("Reloaded one bullet! You have " + ammo + " bullets loaded.");
                    waiting = 100;
                }
                else
                {
                    Debug.Log("You're already at maximum ammo!");
                }
            }
        }
    }
}
