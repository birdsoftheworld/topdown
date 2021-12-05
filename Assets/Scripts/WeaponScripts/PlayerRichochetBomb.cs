using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRichochetBomb : MonoBehaviour
{
    public Transform bulletOrigin;

    public GameObject bombPrefab;
    public int grenadeTimer = 150;
    public int throwSpeed = 12;
    public int damage = 1;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.waiting3 == 0)
        {
            if (Input.GetMouseButton(1))
            {

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                Vector2 currentPos = this.transform.GetChild(0).transform.position;

                destination = destination - currentPos;

                Vector3 destinationN = destination.normalized;

                float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, angle + 90);



                GameObject clone = Instantiate(bombPrefab, this.transform.GetChild(0).transform.position, rotation);

                clone.gameObject.SetActive(true);

                RichochetBomb grenade = clone.gameObject.GetComponent("RichochetBomb") as RichochetBomb;

                grenade.bulletSpeed = throwSpeed;
                grenade.countdownMax = grenadeTimer;
                grenade.countdown = grenadeTimer;
                grenade.damage = damage;


                player.waiting3 = 75;
                //bullet.bulletDamage = bulletDamage;
                //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);
            }
        }

    }
}
