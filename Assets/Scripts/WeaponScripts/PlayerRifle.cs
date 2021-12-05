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
    public int ammoCap = 1;
    public int ammo;

    public int waiting = 0;

    public AmmoTracker ammoCounter;

    public Player player;

    private void Start()
    {
        ammo = ammoCap;
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
        //ammoCounter.define3(player.heavyAmmo.ToString());
        player.UpdateCheck();
    }

    private void FixedUpdate()
    {
        if (player.waiting2 == 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (ammo > 0)
                {

                    ammo--;
                    ammoCounter.define1(ammo.ToString());

                    Vector2 position = bulletOrigin.position;
                    GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                    clone.gameObject.SetActive(true);

                    Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                    bullet.bulletSpeed = bulletSpeed;
                    bullet.bulletFaction = bulletFaction;
                    bullet.bulletDamage = bulletDamage;
                
                    player.waiting2 = 25;
                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap && player.heavyAmmo > 0)
                {
                    ammo++;
                    player.heavyAmmo--;
                    UpdateTracker();
                    //Debug.Log("Reloaded one bullet! You have " + ammo + " bullets loaded.");
                    player.waiting2 = 50;
                }
                else
                {
                    //Debug.Log("You're already at maximum ammo!");
                }
            }



            else if (Input.GetMouseButton(1))
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
            }
        }
    }
}
