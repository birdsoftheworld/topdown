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

    private bool exploding = false;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;

        if (timeFlone <= 5)
        {
            exploding = false;
        }

        if (exploding == false)
        {
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
        }
        else
        {
            if (this.transform.localScale != new Vector3(6.25f, 6.25f, 1f))
            {
                this.transform.localScale += new Vector3(2f, 2f, 0f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        timeFlone++;
    }

    public void FaceTarget()
    {
        Vector3 currentPos = this.transform.position;
        Vector3 destination = target.transform.position - currentPos;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        //Debug.Log("1 " + rotation.eulerAngles.z);
        //Debug.Log("2 " + transform.localEulerAngles.z);

        //if (target.transform.position.y < this.transform.position.y)
        //{
            if (rotation.eulerAngles.z > transform.localEulerAngles.z)
            {
                this.transform.Rotate(0.0f, 0.0f, turnSpeed * -1f, Space.World);
            }
            if (rotation.eulerAngles.z < transform.localEulerAngles.z)
            {
                this.transform.Rotate(0.0f, 0.0f, turnSpeed * 1f, Space.World);
            }
        /*}
        else
        {
            if (rotation.eulerAngles.z > transform.localEulerAngles.z)
            {
                this.transform.Rotate(0.0f, 0.0f, turnSpeed * 1f, Space.World);
            }
            if (rotation.eulerAngles.z < transform.localEulerAngles.z)
            {
                this.transform.Rotate(0.0f, 0.0f, turnSpeed * -1f, Space.World);
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
        {
            Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();
            Faction hitFact = hitted.faction;

            if (exploding == false)
            {
                if (timeFlone > 5)
                {
                    sprite.color = Color.white;
                    this.transform.localScale = new Vector3(0.25f, .25f, 1f);
                    exploding = true;
                }
            }
            if (exploding == true)
            {
                if (hitted != null)
                {
                    if (hitted.CanHit(bulletFaction))
                    {
                        HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                        if (health != null)
                        {
                            health.DealDamage(bulletDamage, this.transform.position);
                        }
                    }
                }
            }
        }
    }
}
