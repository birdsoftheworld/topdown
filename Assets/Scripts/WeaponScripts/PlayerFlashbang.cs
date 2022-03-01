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

                Vector2 position = bulletOrigin.position;
                GameObject clone = Instantiate(flashbangPrefab, this.transform.GetChild(0).transform.position, rotation);
                clone.gameObject.SetActive(true);

                Flashbang grenade = clone.gameObject.GetComponent("Flashbang") as Flashbang;

                grenade.bulletSpeed = throwSpeed;
                grenade.countdownMax = grenadeTimer;
                grenade.countdown = grenadeTimer;

                player.waiting3 = 50;

                player.waiterChanged = true;

                //bullet.bulletDamage = bulletDamage;
                //clone.GetComponent<CircleCollider2D>().bounds = new Vector2(1f, 0.5f);
            }
        }

    }
}
