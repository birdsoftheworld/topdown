using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public int dropType;
    public int amountGive;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        this.transform.Rotate(0, 0, Random.Range(0, 361));
        rb2D.AddForce(transform.up * 5f);
    }

    void FixedUpdate()
    {
        rb2D.velocity = rb2D.velocity / 2;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();

            if (dropType == 0)
            {
                coll.GetComponent<HealthTest>().curHealth += amountGive;
                if (coll.GetComponent<HealthTest>().curHealth > coll.GetComponent<HealthTest>().maxHealth)
                {
                    amountGive = coll.GetComponent<HealthTest>().curHealth - coll.GetComponent<HealthTest>().maxHealth;

                    coll.GetComponent<HealthTest>().curHealth = coll.GetComponent<HealthTest>().maxHealth;

                    coll.GetComponent<HealthTest>().healthBar.SetHealth(coll.GetComponent<HealthTest>().curHealth);

                }
                else
                {
                    coll.GetComponent<HealthTest>().healthBar.SetHealth(coll.GetComponent<HealthTest>().curHealth);
                    coll.GetComponent<HealthTest>().iFrames += 1;
                    Destroy(gameObject);
                }

            }
            if (dropType == 1)
            {
                player.lightAmmo += amountGive;
                if (player.lightAmmo > player.lightAmmoMax)
                {
                    amountGive = player.lightAmmo - player.lightAmmoMax;

                    player.lightAmmo = player.lightAmmoMax;

                }
                else
                {
                    Destroy(gameObject);
                }
            }
            if (dropType == 2)
            {
                player.heavyAmmo += amountGive;
                if (player.heavyAmmo > player.heavyAmmoMax)
                {
                    amountGive = player.heavyAmmo - player.heavyAmmoMax;

                    player.heavyAmmo = player.heavyAmmoMax;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            if (dropType == 3)
            {
                player.rockets += amountGive;
                if (player.rockets > player.rocketsMax)
                {
                    amountGive = player.rockets - player.rocketsMax;

                    player.rockets = player.rocketsMax;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            player.UpdateCheck();

        }
    }
}
