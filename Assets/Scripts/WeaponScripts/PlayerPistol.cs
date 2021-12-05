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
    public int bulletDamage = 2;
    public int ammoCap = 6;
    public int ammo;

    public AmmoTracker ammoCounter;

    public Player player;

    public int whichButton = 0;


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
        bool i = false;
        if (whichButton == 0)
        {
            if (player.waiting2 == 0)
            {
                i = true;
            }
        }
        else
        {
            if (player.waiting3 == 0)
            {
                i = true;
            }
        }


        if (i == true)
        {
            if (Input.GetMouseButton(whichButton))
            {
                if (ammo > 0)
                {

                    ammo--;
                    ammoCounter.define1(ammo.ToString());

                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Camera.main.nearClipPlane;
                    Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                    Vector2 currentPos = this.transform.GetChild(0).transform.position;

                    destination = destination - currentPos;

                    Vector3 destinationN = destination.normalized;

                    float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

                    Quaternion rotation = new Quaternion();
                    rotation.eulerAngles = new Vector3(0, 0, angle + 90);

                    Vector2 position = bulletOrigin.position;
                    GameObject clone = Instantiate(bulletPrefab, position, rotation);
                    clone.gameObject.SetActive(true);

                    Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                    bullet.bulletSpeed = bulletSpeed;
                    bullet.bulletFaction = bulletFaction;
                    bullet.bulletDamage = bulletDamage;

                    if (whichButton == 0)
                    {
                        player.waiting2 = 20;
                    }
                    else
                    {
                        player.waiting3 = 20;
                    }
                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap && player.lightAmmo > 0)
                {
                    int reloadAmount = ammoCap - ammo;
                    if (player.lightAmmo < reloadAmount)
                    {
                        reloadAmount = player.lightAmmo;
                    }
                    if (reloadAmount > 0)
                    {
                        ammo = ammo + reloadAmount;
                        player.lightAmmo -= (reloadAmount -1);
                        //Debug.Log("Rrrrreloading!");
                        if (whichButton == 0)
                        {
                            player.waiting2 = reloadAmount * 10;
                        }
                        else
                        {
                            player.waiting3 = reloadAmount * 10;
                        }

                        ammoCounter.define1(ammo.ToString());
                        player.UpdateCheck();
                        //ammoCounter.define3(player.lightAmmo.ToString());
                    }




                    player.lightAmmo--;

                    ammo = ammoCap;
                    UpdateTracker();
                    //Debug.Log("Reloaded one bullet! You have " + ammo + " bullets loaded.");
                }
                else
                {
                    //Debug.Log("You're already at maximum ammo!");
                }
            }
        }
    }
}