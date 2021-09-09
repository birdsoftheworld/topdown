using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;

    public float bulletSpeed;
    private Rigidbody2D rb2D;
    private Vector2 destination;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 destinationN = destination.normalized;

        float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;

        rb2D.AddForce(transform.up * bulletSpeed * -1f);

    }

    /*void FixedUpdate()
    {
        //rb2D.AddForce((transform.up / -10f) * bulletSpeed, ForceMode2D.Impulse);
        //for constant acceleration
    }*/
}
