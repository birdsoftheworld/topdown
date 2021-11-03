using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingMissile : MonoBehaviour
{

    public int bulletDamage;
    public Faction bulletFaction;

    public GameObject target;

    public float bulletSpeed;
    public float turnSpeed;
    private Rigidbody2D rb2D;

    private int timeFlone = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100);

        if (timeFlone == 10/* || timeFlone == 30 || timeFlone == 60 || timeFlone == 100*/)
        {
            GameObject[] cover;

            cover = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = this.transform.position;
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

            target = closest;

            bulletSpeed = bulletSpeed / 2; //bullet slows down as aiming engages
        }

        //if (timeFlone > 10 && (timeFlone / 10) == Mathf.RoundToInt(timeFlone / 10))
        //{
        //    Debug.Log("plonk");
        if (target != null)
        {
            if ((timeFlone / 10) >= Mathf.RoundToInt(timeFlone / 10) - .2 && (timeFlone / 10) <= Mathf.RoundToInt(timeFlone / 10) + .2)
            {
                FaceTarget();
            }
            else if ((timeFlone / 10) >= Mathf.RoundToInt(timeFlone / 10) - .3 && (timeFlone / 10) <= Mathf.RoundToInt(timeFlone / 10) + .3)
            {
                rb2D.AddForce(transform.up * bulletSpeed * -200);
            }
        }
        //}
        
        timeFlone++;

    }

    public void FaceTarget()
    {
        Vector3 currentPos = this.transform.position;
        Vector3 destination = target.transform.position - currentPos;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        //Debug.Log(transform.localEulerAngles.z);
        if (rotation.eulerAngles.z > transform.localEulerAngles.z)
        {
            this.transform.Rotate(0.0f, 0.0f, turnSpeed * 1f, Space.World);
        }
        if (rotation.eulerAngles.z < transform.localEulerAngles.z)
        {
            this.transform.Rotate(0.0f, 0.0f, turnSpeed * -1f, Space.World);
        }

        //transform.rotation = rotation;


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
        {
            Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();
            Faction hitFact = hitted.faction;

            if (coll.GetComponent<Collider2D>().isTrigger == true)
            {

            }
            else if (hitted != null)
            {
                if (hitted.CanHit(bulletFaction))
                {
                    Destroy(gameObject);
                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(bulletDamage);
                    }
                }
            }
        }
    }
}
