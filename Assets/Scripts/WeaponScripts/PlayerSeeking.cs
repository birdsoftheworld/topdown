using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeking : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;

    public float bulletSpeed = 15;
    public float turnSpeed = 2;
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

    public void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //ammo = ammoCap;

        UpdateTracker();

        //ammo = ammoCap;
        //bulletOrigin = this.transform;
    }

    private void UpdateTracker()
    {
        ammoCounter.define1(ammo.ToString());
        ammoCounter.define2(ammoCap.ToString());
        //ammoCounter.define3(player.rockets.ToString());
        player.UpdateCheck();
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
                if (ammo > 0)
                {
                    //if (fireTick == fireTickMax)
                    //{
                    ammo--;
                    ammoCounter.define1(ammo.ToString());

                    Vector2 position = bulletOrigin.position;
                    GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                    clone.gameObject.SetActive(true);

                    SeekingMissile bullet = clone.gameObject.GetComponent("SeekingMissile") as SeekingMissile;

                    bullet.bulletSpeed = bulletSpeed;
                    bullet.turnSpeed = turnSpeed;
                    bullet.bulletFaction = bulletFaction;
                    bullet.bulletDamage = bulletDamage;

                   // Vector3 mousePos = Input.mousePosition;
                    //mousePos.z = Camera.main.nearClipPlane;

                    //bullet.target = findNearestNodeOfType("Enemy", mousePos);

                    //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);

                    player.waiting2 = 25;

                    //fireTick = 0;
                    //}
                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap && player.rockets > 0)
                {
                    ammo++;
                    player.rockets--;
                    ammoCounter.define1(ammo.ToString());
                    ammoCounter.define1(player.rockets.ToString());
                    Debug.Log("Reloaded one bullet! You have " + ammo + " bullets loaded.");
                    player.waiting2 = 50;
                }
                else
                {
                    Debug.Log("You're already at maximum ammo!");
                }
            }


            if (player.waiting3 > 0)
            {
                player.waiting3--;
            }
        }
    }

    /*public Transform findNearestNodeOfType(string nodeTag, Vector3 from)
    {
        //find all cover
        GameObject[] cover;

        cover = GameObject.FindGameObjectsWithTag(nodeTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = from;
        foreach (GameObject go in cover)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
    }*/
}
