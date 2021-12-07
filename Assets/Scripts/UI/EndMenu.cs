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
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//end program
		}

		/*if (Time.timeScale != 0)
		{
			transform.SetAsLastSibling();
			this.transform.GetChild(0).gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			this.transform.GetChild(0).gameObject.SetActive(false);
			Time.timeScale = 1;
		}*/

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

		GameObject[] levelGen = GameObject.FindGameObjectsWithTag("LevelGenerator");

		Destroy(levelGen[0]);

		GameObject levelNew = Instantiate(levelGenerator, new Vector3(0, 0, 0), this.transform.rotation);

		levelNew.GetComponent<ObjectiveController>().endMenu = this.gameObject;

		Time.timeScale = 1;

		GameObject[] healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
		for (int i = 0; i < healthBars.Length; i++)
		{
			if (healthBars[i].GetComponent<HealthBar>().playerHealth != player.GetComponent<HealthTest>())
			{
				Destroy(healthBars[i]);
			}
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++)
        {
			Destroy(enemies[i]);
        }
			
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
		for (int i = 0; i < projectiles.Length; i++)
		{
			Destroy(projectiles[i]);
		}

		GameObject[] explosive = GameObject.FindGameObjectsWithTag("Explosive");
		for (int i = 0; i < explosive.Length; i++)
		{
			Destroy(explosive[i]);
		}

		this.gameObject.SetActive(false);
	}


}
