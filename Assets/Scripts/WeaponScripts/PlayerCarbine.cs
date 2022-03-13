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

    public AmmoTracker ammoCounter;

    public int jitter;

    public CamFollow cam;

    public int whichButton = 0;

    private void Start()
    {
        ammo = ammoCap;
        fireTick = fireTickMax / 10;
        //bulletOrigin = this.transform;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
        //ammo = ammoCap;
        //UpdateTracker();
    }

    /*private void UpdateTracker()
    {
        ammoCounter.define1(ammo.ToString());
        ammoCounter.define2(ammoCap.ToString());
        //ammoCounter.define3(player.lightAmmo.ToString());
        player.UpdateCheck();
    }*/

    private void FixedUpdate()
    {
        if (ammo == 0 && player.lightAmmo > 0)
        {
            player.lightAmmo--;
            ammo += 5;

            if (whichButton == 0)
            {
                if (this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>() != null)
                {
                    this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>().ammo += 3;
                    if (this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>().ammo > this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>().ammoCap)
                    {
                        this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>().ammo = this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerPistol>().ammoCap;
                    }
                }
                else if (this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>() != null)
                {
                    this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>().ammo += 5;
                    if (this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>().ammo > this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>().ammoCap)
                    {
                        this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>().ammo = this.transform.parent.parent.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerCarbine>().ammoCap;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 3; i++) {
                    if (this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>() != null)
                    {
                        this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>().ammo += 3;
                        if (this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>().ammo > this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>().ammoCap)
                        {
                            this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>().ammo = this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerPistol>().ammoCap;
                        }
                    }
                    if (this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>() != null)
                    {
                        this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>().ammo += 5;
                        if (this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>().ammo > this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>().ammoCap)
                        {
                            this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>().ammo = this.transform.parent.parent.GetChild(0).GetChild(i).gameObject.GetComponent<PlayerCarbine>().ammoCap;
                        }
                    }
                }

            }
        }

        bool a = false;
        if (whichButton == 0)
        {
            if (player.waiting2 == 0)
            {
                a = true;
            }
        }
        else if (whichButton == 1)
        {
            if (player.waiting3 == 0)
            {
                a = true;
            }
        }

        if (a == true)
        {
            if (Input.GetMouseButton(whichButton))
            {
                if (ammo > 0 && burstTick > 0)
                {
                    if (fireTick == (fireTickMax / 10))
                    {
                        ammo--;
                        //ammoCounter.define1(ammo.ToString());

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

                        fireTick = 0;

                        //Quaternion rotation2 = new Quaternion();
                        //rotation2.eulerAngles = new Vector3(angle + 90, 0, 0);

                        this.transform.GetChild(0).rotation = rotation;
                        
                        
                        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

                        cam.jitter += jitter;

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
                    int reloadAmount = ammoCap - ammo;
                    /*if (player.lightAmmo < reloadAmount)
                    {
                        reloadAmount = player.lightAmmo;
                    }
                    if (reloadAmount > 0)
                    {
                        ammo = ammo + reloadAmount;
                        player.lightAmmo -= reloadAmount;
                        //Debug.Log("Rrrrreloading!");*/

                    ammo += reloadAmount;
                        if (whichButton == 0)
                        {
                            player.waiting2 = 150;
                        }
                        else
                        {
                            player.waiting3 = 150;
                        }
                        //ammoCounter.define1(ammo.ToString());
                        //player.UpdateCheck();
                        //ammoCounter.define3(player.lightAmmo.ToString());
                    //}
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

            if (whichButton == 0)
            {
                player.waiting2 = 50;
            }
            else
            {
                player.waiting3 = 50;
            }

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
