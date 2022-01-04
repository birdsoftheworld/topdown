using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject levelGen;

    public GameObject startCamera;

    public GameObject mainCamera;

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

        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        /*mainCamera.tag = "Untagged";
        startCamera.tag = "MainCamera";

        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasScaler>().scaleFactor = .25f;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().worldCamera = startCamera.GetComponent<Camera>();*/
    }

    public void Begin()
    {
        player.gameObject.SetActive(true);

        levelGen.SetActive(true);

        //startCamera.SetActive(false);
        //GameObject.FindGameObjectWithTag("MainCamera").SetActive(true);

        Time.timeScale = 1;

        //if (mainCamera == null)
        //{
        //    mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //}

        /*mainCamera.tag = "MainCamera";
        startCamera.tag = "Untagged";
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasScaler>().scaleFactor = 1f;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().worldCamera = mainCamera.GetComponent<Camera>();
        startCamera.SetActive(false);

        this.gameObject.SetActive(false);*/
    }
}
