using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCommand : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float bulletSpeed;

    public Transform bulletOrigin;
    public GameObject bulletPrefab;

    private void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = bulletOrigin.position;
            GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
            clone.gameObject.SetActive(true);

            //clone.gameObject.GetComponent<Projectile>;

            clone.GetComponent<Collider2D>().isTrigger = true;

        }
    }
}
