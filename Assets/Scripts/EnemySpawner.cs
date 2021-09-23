using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject healthPrefab;

    public Transform origin;

  //  public GameObject player;

  //  public GameObject canvas;


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
        GameObject clone = Instantiate(enemyPrefab, position, origin.rotation);
   //     clone.GetComponent<EnemyFacingBasic>().player = player;

        GameObject cloneHealth = Instantiate(healthPrefab, position, origin.rotation);
      //  cloneHealth.transform.SetParent(canvas.transform, false);


        clone.GetComponent<HealthTest>().healthBar = cloneHealth.GetComponent<HealthBar>();

        cloneHealth.GetComponent<UIFollow>().monsterPosition = clone.GetComponent<Transform>();
        cloneHealth.GetComponent<UIFollow>().health = clone.GetComponent<HealthTest>();

        cloneHealth.GetComponent<HealthBar>().playerHealth = clone.GetComponent<HealthTest>();
    }
}
