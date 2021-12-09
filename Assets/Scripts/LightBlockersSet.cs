using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlockersSet : MonoBehaviour
{
    private int a = 0;

    void LateUpdate()
    {
        if (a == 1)
        {
            storeRoomVars roomVars = this.transform.parent.gameObject.GetComponent<storeRoomVars>();



            if (roomVars.downExit == true)
            {
                Destroy(this.transform.GetChild(7).gameObject);
            }
            if (roomVars.upExit == true)
            {
                Destroy(this.transform.GetChild(6).gameObject);
            }
            if (roomVars.rightExit == true)
            {
                Destroy(this.transform.GetChild(5).gameObject);
            }
            if (roomVars.leftExit == true)
            {
                Destroy(this.transform.GetChild(4).gameObject);
            }
            if (roomVars.downExit == false && roomVars.rightExit == false)
            {
                Destroy(this.transform.GetChild(3).gameObject);
            }
            if (roomVars.upExit == false && roomVars.rightExit == false)
            {
                Destroy(this.transform.GetChild(2).gameObject);
            }
            if (roomVars.downExit == false && roomVars.leftExit == false)
            {
                Destroy(this.transform.GetChild(1).gameObject);
            }
            if (roomVars.upExit == false && roomVars.leftExit == false)
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }

            StartCoroutine("Deactivate");

            StartCoroutine("Activate");

            a++;
        }
        else if (a == 0)
        {
            a++;
        }

        //this.gameObject.SetActive(false);
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1 / 3);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1 / 3);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
