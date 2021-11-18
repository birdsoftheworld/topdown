using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();


        for (int i = 0; i < allObjects.Length - 1; i++)
        {
            if (allObjects[i].tag != "UI" && allObjects[i].GetComponent<Camera>() == null && allObjects[i].GetComponent<Canvas>() == null)
            {
                allObjects[i].SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
