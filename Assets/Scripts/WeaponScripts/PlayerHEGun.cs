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



                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Camera.main.nearClipPlane;
                    Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                    Vector2 currentPos = this.transform.position;

                    destination = destination - currentPos;

                    Vector3 destinationN = destination.normalized;

                    float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

                    angle += 180;
                    if (angle > 360f)
                    {
                        angle -= 360;
                    }
                    angle = -1 * ((angle * -1) + 180);
                    if (angle < 0)
                    {
                        angle = 360 + angle;
                    }
                    angle = angle * Mathf.PI / 180;

                    int changeX = (int)(-2 * Mathf.Cos(angle) * player.driftingTickMax);
                    int changeY = (int)(-2 * Mathf.Sin(angle) * player.driftingTickMax);

                    player.driftingTickX += changeX;
                    player.driftingTickY += changeY;

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



           else if (Input.GetMouseButton(1))
            {
                this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();

                /*if (ammo > 0)
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

                }*/
            }
        }
    }
}