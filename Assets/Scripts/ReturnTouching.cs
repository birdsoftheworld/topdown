using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTouching : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<ObjectiveController>().playerOnExit = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<ObjectiveController>().playerOnExit = false;
        }
    }
}
