using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashbang : MonoBehaviour
{
    public Transform bulletOrigin;

    public GameObject flashbangPrefab;
    public int grenadeTimer = 100;
    public int throwSpeed = 12;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.waiting3 > 0)
        {
            player.waiting3--;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 position = bulletOrigin.position;
            GameObject clone = Instantiate(flashbangPrefab, position, bulletOrigin.rotation);
            clone.gameObject.SetActive(true);

            Flashbang grenade = clone.gameObject.GetComponent("Flashbang") as Flashbang;

            grenade.bulletSpeed = throwSpeed;
            grenade.countdownMax = grenadeTimer;
            grenade.countdown = grenadeTimer;

            player.waiting3 = 50;
            //bullet.bulletDamage = bulletDamage;
            //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);
        }

    }
}
