using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{

    public Transform monsterPosition;


    void Start()
    {

    }

    void Update()
    {

        this.GetComponent<Transform>().position = new Vector3 (monsterPosition.position.x, monsterPosition.position.y - 1, monsterPosition.position.z);



    }
}
