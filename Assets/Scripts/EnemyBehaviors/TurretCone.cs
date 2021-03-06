using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCone : MonoBehaviour
{

    public bool sensingPlayer = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            sensingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            sensingPlayer = false;
        }
    }
}
