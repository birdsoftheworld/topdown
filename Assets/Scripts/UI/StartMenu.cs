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

    public Level listStore;

    void Start()
    {
        LevelSetter();
    }

    public void LevelSetter()
    {
        int level = SceneInformation.getL();

        //Debug.Log(SceneInformation.getL());



        if (level == 0)
        {

        }
        else if (level == 1)
        {
            //no items or powers. go straight to game
            this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

            //only pistol selectable
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(false);

            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).gameObject.GetComponent<CanBeDragged>().priorLocation = 5;
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).gameObject.GetComponent<CanBeDragged>().location = 5;
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).position = new Vector2(5.4f, 4f);

            //Level lists = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>();

            for (int i = listStore.enemyPrefabs.Count - 1; i > 0; i--)
            {
                listStore.enemyPrefabs.RemoveAt(i);
            }

            for (int i = listStore.objectPrefabs.Count - 1; i > -1; i--)
            {
                listStore.objectPrefabs.RemoveAt(i);
            }

            listStore.mapSize = 2;
        }
        else if (level == 2)
        {
            //no powers, yes items
            this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

            this.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);

            //only pistol and rifle selectable
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(true);

            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).gameObject.GetComponent<CanBeDragged>().priorLocation = 5;
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).gameObject.GetComponent<CanBeDragged>().location = 5;
            this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(3).position = new Vector2(5.4f, 4f);

            this.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(true);
            this.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(false);

            //Level lists = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>();

            for (int i = listStore.enemyPrefabs.Count - 1; i > 3; i--)
            {
                listStore.enemyPrefabs.RemoveAt(i);
            }

            for (int i = listStore.objectPrefabs.Count - 1; i > -1; i--)
            {
                listStore.objectPrefabs.RemoveAt(i);
            }

            listStore.mapSize = 5;
        }
        else
        {
            SceneInformation.setL(0);
            level = 0;
        }



        if (level > 0)
        {
            this.transform.parent.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            this.transform.parent.GetChild(1).GetChild(2).gameObject.SetActive(false);
        }

        Time.timeScale = 1;
    }

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
