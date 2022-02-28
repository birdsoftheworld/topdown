using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDispersalWave : MonoBehaviour
{

    public Player player;
    public int rechargeBase;

    public int active;

    //public float reduction = 0;
    //public float reductionMax;

    int waiting;

    int setWaiter;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        active = 0;

        //reduction = 0;

        waiting = 0;

        setWaiter = 15;
    }

    void FixedUpdate()
    {
        /*Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 currentPos = this.transform.position;

        destination = destination - currentPos;

        Vector3 destinationN = destination.normalized;

        float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;*/





        if (waiting > 0)
        {
            waiting--;
        }
        else if (active == 0)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player.waiting4 == 0)
                {
                    active = 2;
                    this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                }
            }
        }
        else if (active == 1)
        {
            if (this.transform.localScale.x > 6.1f)
            {
                this.transform.localScale -= new Vector3(.1f, .1f, 0f);
            }
            else if (this.transform.localScale.x > 3.1f)
            {
                this.transform.localScale -= new Vector3(.2f, .2f, 0f);
            }
            else if (this.transform.localScale.x > 1.1f)
            {
                this.transform.localScale -= new Vector3(.3f, .3f, 0f);
            }

            if (this.transform.localScale.x < 1.1f)
            {
                this.transform.localScale = new Vector3(1.1f, 1.1f, 0f);
            }
            if (this.transform.localScale.x == 1.1f)
            {
                active = 0;
            }
        }
        else if (active == 2)
        {
            if (Input.GetKey(KeyCode.E))
            {
                setWaiter++;

                if (this.transform.localScale.x < 3.1f)
                {
                    this.transform.localScale += new Vector3(.2f, .2f, 0f);
                }
                else if (this.transform.localScale.x < 6.1f)
                {
                    this.transform.localScale += new Vector3(.1f, .1f, 0f);
                }
                else if (this.transform.localScale.x < 9.1f)
                {
                    this.transform.localScale += new Vector3(.05f, .05f, 0f);
                }
                else if (setWaiter == rechargeBase * 6)
                {
                    active = 1;
                    waiting = setWaiter;
                    player.waiting4 = setWaiter;
                    setWaiter = rechargeBase;
                }
            }
            else
            {
                active = 1;
                waiting = setWaiter;
                player.waiting4 = setWaiter;
                setWaiter = rechargeBase;
            }
        }
    }




    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Projectile>() != null)
        {
            //coll.GetComponent<Collider2D>().GetComponent<Projectile>().bulletSpeed -= reduction;

            coll.GetComponent<Collider2D>().GetComponent<Hittable>().safe = false;

            /*Quaternion rotation = new Quaternion();

            rotation.eulerAngles = coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation.eulerAngles;

            rotation.eulerAngles += new Vector3(0, 0, 180);

            coll.GetComponent<Collider2D>().GetComponent<Transform>().rotation = rotation;*/


            /*if (Input.GetKey(0)) { 


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 currentPos = this.transform.position;

            destination = destination - currentPos;

            Vector3 destinationN = destination.normalized;

            float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);


            coll.transform.rotation = rotation;
            }*/
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.GetComponent<Collider2D>().GetComponent<Projectile>() != null)
        {
            Projectile bullet = coll.GetComponent<Collider2D>().GetComponent<Projectile>();

            if (bullet.bulletSpeed > 1)
            {
                bullet.bulletSpeed -= bullet.bulletSpeed / 2;
            }
            else if (bullet.bulletSpeed >= 0)
            {
                bullet.bulletSpeed = -.5f;
            }
            else
            {
                bullet.bulletSpeed = bullet.bulletSpeed * 2;
            }





            //coll.GetComponent<Collider2D>().GetComponent<Hittable>().safe = false;
        }
    }
}
