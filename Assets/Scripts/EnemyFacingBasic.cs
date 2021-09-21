using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacingBasic : MonoBehaviour
{

    public GameObject player;

    private Vector2 destination;

    public float fireSpeed;
    private float fireTick = 0;


    public Rigidbody2D bullet;

    public float bulletSpeed;
    public int bulletDamage;
    public Faction bulletFaction;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        fireTick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.transform.position;


        Vector2 currentPos = this.transform.position;

        destination = playerPos - currentPos;


        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;


    }

    void FixedUpdate()
    {
        fireTick++;
        if (fireTick == fireSpeed)
        {
            Fire();

            fireTick = 0;
        }
    }


    private void Fire()
    {
        Vector2 position = this.transform.position;
        GameObject clone = Instantiate(bulletPrefab, position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = bulletFaction;
        bullet.bulletDamage = bulletDamage;
    }
}
