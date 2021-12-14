using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemiesToSpawn;

    private bool a = false;

    private Player p;

    public int wait = 15;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!a)
        {
            //if (wait == 0)
            //{
            if (GameObject.FindGameObjectsWithTag("EnemySpawner") != null)
            {
                GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

                if (spawners.Length < enemiesToSpawn)
                {
                    enemiesToSpawn = spawners.Length;
                }

                while (enemiesToSpawn > 0)
                {
                    for (int b = 0; b < spawners.Length; b++)
                    {
                        if (b == p.location)
                        {
                            b++;
                            if (Random.Range(0, 2) == 1)
                            {
                                Debug.Log(spawners[b].transform.position);
                                spawners[b].GetComponent<EnemySpawner>().spawningEnemy = true;
                                enemiesToSpawn--;
                            }
                        }
                        else
                        {
                            if (Random.Range(0, 2) == 1)
                            {
                                Debug.Log(spawners[b].transform.position);
                                spawners[b].GetComponent<EnemySpawner>().spawningEnemy = true;
                                enemiesToSpawn--;
                            }
                        }

                        if (enemiesToSpawn == 0)
                        {
                            b = spawners.Length;
                            a = true;
                        }
                    }
                }
            }
            //}
            //else
            //{
            //    wait--;
            //}
        }
        else
        {
            this.enabled = false;
        }
    }
}
