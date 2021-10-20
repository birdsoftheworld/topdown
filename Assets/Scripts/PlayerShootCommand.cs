using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCommand : MonoBehaviour
{
    public Rigidbody2D bullet;

    public float bulletSpeed;
    public int bulletDamage;
    public Faction bulletFaction;

    public Transform bulletOrigin;
    public GameObject bulletPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = bulletOrigin.position;
            GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
            clone.gameObject.SetActive(true);

            Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

            bullet.bulletSpeed = bulletSpeed;
            bullet.bulletFaction = bulletFaction;
            bullet.bulletDamage = bulletDamage;
        }
    }
}
