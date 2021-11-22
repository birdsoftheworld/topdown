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
                Vector2 position = bulletOrigin.position;
                GameObject clone = Instantiate(bombPrefab, position, bulletOrigin.rotation);
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
