using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnonoroffcamerafollow : MonoBehaviour
{

    GameObject mainCam;

   public Transform player;
     public   Transform intermid;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    public void Switch()
    {
        if (mainCam.gameObject.GetComponent<CamFollow>().player == intermid)
        {
            mainCam.gameObject.GetComponent<CamFollow>().player = player;
        }
        else
        {
            mainCam.gameObject.GetComponent<CamFollow>().player = intermid;
        }
    }
}
