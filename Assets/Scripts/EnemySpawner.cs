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


    // Start is called before the first frame update
    void Start()
    {

        Vector3 position = origin.position;

        Spawn(position, origin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(Vector3 position, Transform rotation)
    {
        GameObject clone = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], position, origin.rotation);
   //     clone.GetComponent<EnemyFacingBasic>().player = player;

        GameObject cloneHealth = Instantiate(healthPrefab, position, origin.rotation);
        //  cloneHealth.transform.SetParent(canvas.transform, false);

        if (clone.GetComponent<HealthTest>() == null && clone.transform.GetChild(0).GetComponent<HealthTest>() != null)
        {
            //Debug.Log(clone.transform.GetChild(0).transform.gameObject);

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
        /*else
        {
            Debug.Log(clone);
        }*/


        /*if (cloneHealth.GetComponent<UIFollow>().monsterPosition == null)
        {
            Debug.Log("fail");
            Debug.Log(clone);
            Destroy(clone);
            Destroy(cloneHealth);
        }*/
    }
}
