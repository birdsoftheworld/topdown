using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCommand : MonoBehaviour
{
    public Collider2D bullet;
    public float bulletSpeed;




    void Fire()
    {
        // convert mouse position into world coordinates
        //    Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
        //Vector2 direction = (mouseScreenPosition - (Vector2) transform.position).normalized;

        // set vector of transform directly
        // transform.up = direction;

        //Vector3 mouse_pos = Input.mousePosition;

        Collider2D bulletClone = (Collider2D)Instantiate(bullet, transform.position, transform.rotation);
        //bulletClone.transform.LookAt(Input.mousePosition);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        bulletClone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //bulletClone.velocity = transform.forward * bulletSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Fire();
    }

}
