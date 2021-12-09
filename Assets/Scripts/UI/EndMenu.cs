using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{

	private GameObject player;

	public GameObject levelGenerator;

	// Start is called before the first frame update
	void Awake()
    {
		Time.timeScale = 0;

		player = GameObject.FindGameObjectWithTag("Player");

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
	}

	void FixedUpdate()
	{
		Time.timeScale = 0;
	}

	public void QuickRestart()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		Player p = player.GetComponent<Player>();

		p.slowDown = p.slowDownMax;

		p.waiting = 0;
		p.waiting2 = 0;
		p.waiting3 = 0;
		p.waiting4 = 0;

		p.lightAmmo = p.lightAmmoMax;
		p.heavyAmmo = p.heavyAmmoMax;

		player.transform.position = new Vector2(5.5f, 5.5f);

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		player.GetComponent<Rigidbody2D>().angularVelocity = 0f;

		p.enabled = true;

		player.GetComponent<HealthTest>().curHealth = player.GetComponent<HealthTest>().maxHealth;

		player.GetComponent<HealthTest>().healthBar.SetHealth(player.GetComponent<HealthTest>().curHealth);

		if (GameObject.FindGameObjectsWithTag("HealthBar") != null)
		{

			GameObject[] healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
			for (int i = 0; i < healthBars.Length; i++)
			{
				if (healthBars[i].GetComponent<HealthBar>().playerHealth != player.GetComponent<HealthTest>())
				{
					Destroy(healthBars[i]);
				}
			}
		}

		if (GameObject.FindGameObjectsWithTag("Enemy") != null)
		{

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < enemies.Length; i++)
			{
				Destroy(enemies[i]);
			}
		}

		if (GameObject.FindGameObjectsWithTag("Projectile") != null)
		{

			GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
			for (int i = 0; i < projectiles.Length; i++)
			{
				Destroy(projectiles[i]);
			}
		}

		if (GameObject.FindGameObjectsWithTag("Explosive") != null)
		{

			GameObject[] explosive = GameObject.FindGameObjectsWithTag("Explosive");
			for (int i = 0; i < explosive.Length; i++)
			{
				Destroy(explosive[i]);
			}
		}

		if (GameObject.FindGameObjectsWithTag("LevelGenerator") != null)
		{
			GameObject[] levelGen = GameObject.FindGameObjectsWithTag("LevelGenerator");
			for (int i = 0; i < levelGen.Length; i++)
			{

				Destroy(levelGen[i]);
			}
		}

		GameObject levelNew = Instantiate(levelGenerator, new Vector3(0, 0, 0), this.transform.rotation);

		levelNew.GetComponent<ObjectiveController>().endMenu = this.gameObject;

		Time.timeScale = 1;


		this.gameObject.SetActive(false);
	}


}
