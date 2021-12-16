using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSniper : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;

    public float bulletSpeed = 15;
    public int bulletDamage = 3;
    public int ammoCap = 1;
    public int ammo;

    public int waiting = 0;

    public AmmoTracker ammoCounter;

    public Player player;

    public GameObject camera;

    private void Start()
    {
        ammo = ammoCap;
        //bulletOrigin = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateTracker();

        camera = GameObject.FindGameObjectWithTag("Camera");
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //ammo = ammoCap;
        UpdateTracker();

        camera = GameObject.FindGameObjectWithTag("Camera");

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

                    player.waiting2 = 30;
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap && player.heavyAmmo > 0)
                {
                    ammo++;
                    player.heavyAmmo--;
                    UpdateTracker();
                    player.waiting2 = 100;
                }
            }



        }

        if (Input.GetMouseButton(1))
        {
            if (camera == null)
            {
                camera = GameObject.FindGameObjectWithTag("Camera");
            }

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 currentPos = this.transform.GetChild(0).transform.position;

            //destination = destination - currentPos;

            Vector3 destinationN = destination.normalized;

            float i = Vector2.Distance(currentPos, destination);

            this.transform.GetChild(0).transform.position = Vector2.MoveTowards(currentPos, destination, i / 10);

            this.transform.GetChild(0).gameObject.SetActive(true);

            camera.GetComponent<CamFollow>().player = this.transform.GetChild(0).transform;
        }
        else
        {
            this.transform.GetChild(0).transform.position = this.transform.position;
            camera.GetComponent<CamFollow>().player = player.transform;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
