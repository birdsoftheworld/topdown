using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{

    public Transform monsterPosition;

    public HealthTest health;

    public float dist;

    void Awake()
    {

    }

    void Update()
    {

        /*if (monsterPosition == null)
        {
            Destroy(gameObject);
        }*/

        this.GetComponent<Transform>().position = new Vector3 (monsterPosition.position.x - dist, monsterPosition.position.y, monsterPosition.position.z);

        this.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);

        if (health.curHealth <= 0)
        {
            Destroy(gameObject);

        }

    }
}
