using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //public GameObject enemyPrefab;
    public GameObject healthPrefab;

    public Transform origin;

    //  public GameObject player;

    //  public GameObject canvas;

    public List<GameObject> enemyPrefabs;

    public List<GameObject> objectPrefabs;

    private bool a = false;
    private bool b = false;

    public bool spawningEnemy;

    private int waiting = 0;

    void Awake()
    {
        waiting = 0;
    }

    void Update()
    {
        Vector3 position = origin.position;

        if (waiting == 5)
        {
            if (spawningEnemy == true)
            {
                if (!a)
                {
                    if (enemyPrefabs.Count != 0)
                    {
                        SpawnEnemy(position, origin);
                        a = true;
                    }
                }
            }
            else
            {
                a = true;
            }

            if (!b)
            {

                if (Random.Range(0, 3) == 2)
                {
                    float a = Random.Range(-10f, 10f);
                    a = a / (Mathf.Abs(a) - 0.5f);

                    float b = Random.Range(-10f, 10f);
                    b = b / (Mathf.Abs(b) - 0.5f);

                    SpawnObject(new Vector3(position.x + (a * 2), position.y + (b * 2), position.z), origin);

                    if (Random.Range(0, 4) == 3)
                    {
                        int c = Random.Range(0, 2);
                        if (c == 1)
                        {
                            a = a * -1;
                        }
                        else
                        {
                            b = b * -1;
                        }

                        SpawnObject(new Vector3(position.x + (a * 2), position.y + (b * 2), position.z), origin);

                        if (Random.Range(0, 5) == 4)
                        {
                            int d = Random.Range(0, 2);
                            if (d == c)
                            {
                                if (d == 1)
                                {
                                    d = 0;
                                }
                                else
                                {
                                    d = 1;
                                }
                            }

                            else if (d == 1)
                            {
                                a = a * -1;
                            }
                            else
                            {
                                b = b * -1;
                            }

                            SpawnObject(new Vector3(position.x + (a * 2), position.y + (b * 2), position.z), origin);

                            if (Random.Range(0, 6) == 5)
                            {
                                if (c == 1)
                                {
                                    a = a * -1;
                                }
                                else
                                {
                                    b = b * -1;
                                }

                                SpawnObject(new Vector3(position.x + (a * 2), position.y + (b * 2), position.z), origin);
                            }
                        }
                    }
                }

                b = true;
            }

            if (a == true && b == true)
            {
                this.enabled = false;
            }
        }
        else
        {
            waiting++;
        }
    }

    void SpawnEnemy(Vector3 position, Transform rotation)
    {
        GameObject clone = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], position, origin.rotation);

        GameObject cloneHealth = Instantiate(healthPrefab, position, origin.rotation);

        if (clone.GetComponent<HealthTest>() == null && clone.transform.GetChild(0).GetComponent<HealthTest>() != null)
        {
            clone.transform.GetChild(0).GetComponent<HealthTest>().healthBar = cloneHealth.GetComponent<HealthBar>();
            cloneHealth.GetComponent<UIFollow>().monsterPosition = clone.transform.GetChild(0).GetComponent<Transform>();
            cloneHealth.GetComponent<UIFollow>().health = clone.transform.GetChild(0).GetComponent<HealthTest>();

            cloneHealth.GetComponent<HealthBar>().playerHealth = clone.transform.GetChild(0).GetComponent<HealthTest>();
        }
        else if (clone.GetComponent<HealthTest>() != null)
        {
            clone.GetComponent<HealthTest>().healthBar = cloneHealth.GetComponent<HealthBar>();
            cloneHealth.GetComponent<UIFollow>().monsterPosition = clone.GetComponent<Transform>();
            cloneHealth.GetComponent<UIFollow>().health = clone.GetComponent<HealthTest>();

            cloneHealth.GetComponent<HealthBar>().playerHealth = clone.GetComponent<HealthTest>();
        }

        cloneHealth.SetActive(true);
        cloneHealth.GetComponent<HealthBar>().setFirst();
    }

    void SpawnObject(Vector3 position, Transform rotation)
    {
        GameObject clone = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Count)], position, origin.rotation);
    }
}
