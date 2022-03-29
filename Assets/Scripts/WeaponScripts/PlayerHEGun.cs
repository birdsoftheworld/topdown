using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHEGun : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;
    //public GameObject explosionPrefab;

    public float bulletSpeed = 20;
    public int bulletDamage = 3;
    public int ammoCap = 3;
    public int ammo;

    public Player player;

    public int jitter;

    public CamFollow cam;

    private void Start()
    {
        ammo = ammoCap;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
    }

    private void FixedUpdate()
    {
        if (ammo == 0 && player.heavyAmmo > 0)
        {
            player.heavyAmmo--;
            ammo++;
        }

        if (player.waiting2 == 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (ammo > 0)
                {

                    ammo--;

                    Vector2 position = bulletOrigin.position;
                    GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                    clone.gameObject.SetActive(true);

                    Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                    bullet.bulletSpeed = bulletSpeed;
                    bullet.bulletFaction = bulletFaction;
                    bullet.bulletDamage = bulletDamage;

                    cam.jitter += jitter;

                    this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

                    player.waiting2 = 25;

                    if (player.slowDown < player.slowDownMax + 10)
                    {
                        player.slowDown += 6;
                    }

                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap) // && player.heavyAmmo > 0)
                {
                    ammo = ammoCap;
                    player.waiting2 = 100;
                }
            }



            /*else if (Input.GetMouseButton(1))
            {
                if (ammo > 0)
                {
                    if (player.waiting3 == 0)
                    {
                        ammo--;
                        ammoCounter.define1(ammo.ToString());

                        Vector2 position = bulletOrigin.position;
                        GameObject clone = Instantiate(tracerPrefab, position, bulletOrigin.rotation);
                        clone.gameObject.SetActive(true);

                        Tracer flare = clone.gameObject.GetComponent("Tracer") as Tracer;

                        flare.bulletSpeed = bulletSpeed;
                        flare.bulletFaction = bulletFaction;
                        //bullet.bulletDamage = bulletDamage;
                        //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);
                        player.waiting3 = 25;
                    }

                }
            }*/
        }
    }
}