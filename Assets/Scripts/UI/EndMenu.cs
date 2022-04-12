using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

	private GameObject player;

	public GameObject levelGeneratorDisplayed;
	private GameObject levelGeneratorReal;

	public GameObject startMenu;

	// Start is called before the first frame update
	void Awake()
	{
		levelGeneratorReal = levelGeneratorDisplayed;

		Time.timeScale = 0;

		player = GameObject.FindGameObjectWithTag("Player");

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
	}

	void FixedUpdate()
	{
		//Time.timeScale = 0;
	}

	public void FullRestart()
	{
		SceneManager.LoadScene("ProjectileTests");

		/*Reset();

		startMenu.SetActive(true);

		//ALSO RESET THE PLAYER'S INVETORY ORDER

		this.gameObject.SetActive(false);*/

	}

	public void QuickRestart()
	{
		Reset();

		if (GameObject.FindGameObjectsWithTag("LevelGenerator") != null)
		{
			GameObject[] levelGen = GameObject.FindGameObjectsWithTag("LevelGenerator");
			for (int i = 0; i < levelGen.Length; i++)
			{
				levelGen[i].SetActive(true);
			}
		}

		Time.timeScale = 1;

		this.gameObject.SetActive(false);
	}

	public void Reset()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		Player p = player.GetComponent<Player>();

		p.Reset();

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

		if (GameObject.FindGameObjectsWithTag("LootDrop") != null)
		{

			GameObject[] lootDrops = GameObject.FindGameObjectsWithTag("LootDrop");
			for (int i = 0; i < lootDrops.Length; i++)
			{
				Destroy(lootDrops[i]);
			}
		}

		if (GameObject.FindGameObjectsWithTag("Objective") != null)
		{

			GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objective");
			for (int i = 0; i < objectives.Length; i++)
			{
				Destroy(objectives[i]);
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
		//StartCoroutine("NewLevel");

		GameObject levelNew = Instantiate(levelGeneratorReal, new Vector3(0, 0, 0), this.transform.rotation);

		levelNew.GetComponent<ObjectiveController>().endMenu = this.gameObject;

		/*
		Level listStore = levelNew.GetComponent<Level>();

		int level = SceneInformation.getL();

		Debug.Log(SceneInformation.getL());

		if (level > 0)
        {
			this.transform.GetChild(2).gameObject.SetActive(true);
        }
		else
        {
			this.transform.GetChild(2).gameObject.SetActive(false);
		}

		/*if (level == 0)
		{

		}
		else if (level == 1)
		{
			listStore.mapSize = 2;

			for (int i = listStore.enemyPrefabs.Count - 1; i > 0; i--)
			{
				listStore.enemyPrefabs.RemoveAt(i);
			}

			for (int i = listStore.objectPrefabs.Count - 1; i > -1; i--)
			{
				listStore.objectPrefabs.RemoveAt(i);
			}
		}
		else if (level == 2)
		{
			listStore.mapSize = 5;

			for (int i = listStore.enemyPrefabs.Count - 1; i > 3; i--)
			{
				listStore.enemyPrefabs.RemoveAt(i);
			}

			for (int i = listStore.objectPrefabs.Count - 1; i > -1; i--)
			{
				listStore.objectPrefabs.RemoveAt(i);
			}
		}
		else
        {
			SceneInformation.setL(0);
		}*/

		this.transform.parent.GetChild(2).gameObject.GetComponent<StartMenu>().LevelSetter();

		levelNew.SetActive(true);

		//Time.timeScale = 1;

	}

	public void ChangeLevel(int change)
    {
		if (SceneInformation.getL() != 0)
		{
			SceneInformation.setL(SceneInformation.getL() + change);

			if (SceneInformation.getL() > 2) //CHANGE WHEN MORE TUTORIAL LEVELS ARE ADDED
			{
				SceneInformation.setL(0);
				FullRestart();
			}
		}
	}

	/*IEnumerator NewLevel()
	{
		yield return new WaitForSeconds(1 / 5);
		GameObject levelNew = Instantiate(levelGeneratorReal, new Vector3(0, 0, 0), this.transform.rotation);

		levelNew.GetComponent<ObjectiveController>().endMenu = this.gameObject;

		levelNew.SetActive(false);

	}*/
}
