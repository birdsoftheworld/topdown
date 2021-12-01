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


    void Start()
    {
        Vector3 position = origin.position;

        if (enemyPrefabs.Count != 0)
        {
            SpawnEnemy(position, origin);
        }

        for (int i = Random.Range(0, 10); i < 5; i += 1)
        {
            float a = Random.Range(-10f, 10f);
            a = a / (Mathf.Abs(a) - 0.5f);

            float b = Random.Range(-10f, 10f);
            b = b / (Mathf.Abs(b) - 0.5f);

            SpawnObject(new Vector3(position.x + (a * 2), position.y + (b * 2), position.z), origin);
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
    }

    void SpawnObject(Vector3 position, Transform rotation)
    {
        GameObject clone = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Count)], position, origin.rotation);
    }
}
