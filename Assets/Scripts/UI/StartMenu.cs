using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject levelGen;

    // Start is called before the first frame update
    void Awake()
    {
        /*GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjects.Length - 1; i++)
        {
            if (allObjects[i].tag != "UI" && allObjects[i].GetComponent<Camera>() == null && allObjects[i].GetComponent<Canvas>() == null)
            {
                allObjects[i].SetActive(false);
            }

        }*/
    }

    public void Begin()
    {
        player.gameObject.SetActive(true);

        levelGen.SetActive(true);

        this.gameObject.SetActive(false);

        Time.timeScale = 1;
    }
}
