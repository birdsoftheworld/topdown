using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingDot : MonoBehaviour
{


    public Transform target;

    public Vector2 targetPos;

    public int seekingRange;

    private Rigidbody2D rb2D;
    public int bulletSpeed;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject[] enemies;

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = this.transform.position;
            foreach (GameObject go in enemies)
            {
                float curDistance = Vector2.Distance(this.transform.position, go.transform.position);

                if (curDistance < distance)
                {
                    //if (curDistance <= seekingRange)
                    //{
                        int layerMask = 1 << 0;
                        RaycastHit2D hit;
                        hit = Physics2D.Raycast(transform.position, go.transform.position - this.transform.position, Mathf.Infinity, layerMask);
                        if (hit.collider.gameObject.tag == "Enemy")
                        {
                            closest = go;
                            distance = curDistance;
                        }

                    //}
                }
            }

            if (closest != null)
            {
                target = closest.transform;
                targetPos = closest.transform.position;
            }
        }

        if (target != null)
        {
            Vector2 currentPos = this.transform.position;
            Vector2 destination = targetPos - currentPos;
            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle + 90);
            transform.rotation = rotation;
        }


        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100f);

        target = null;
    }

    public void kill()
    {
        Destroy(gameObject);
    }
}
