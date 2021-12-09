using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{

    private Level levelController;

    public bool isEscape;

    public GameObject winExit;

    public Transform farRoom;
    public Transform closeRoom;

    public bool playerOnExit;

    public GameObject endMenu;

    // Start is called before the first frame update
    void Awake()
    {
        //endMenu = GameObject.FindGameObjectWithTag("EndMenu");

        levelController = this.gameObject.GetComponent<Level>();

        if (GameObject.FindGameObjectWithTag("EndMenu") != null)
        {
            endMenu = GameObject.FindGameObjectWithTag("EndMenu");
        }


        if (isEscape == true)
        {
            StartCoroutine("SetExit");


        }
    }

    IEnumerator SetExit()
    {
        yield return new WaitForSeconds(1);
        GameObject exit = Instantiate(winExit, farRoom.position, this.transform.rotation);
        exit.SetActive(true);
    }

        // Update is called once per frame
    void Update()
    {
        if (isEscape == true)
        {
            if (playerOnExit == true)
            {
                endMenu.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;

                //Time.timeScale = 0;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTest>().curHealth <= 0)
        {
            endMenu.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        }
    }

}
