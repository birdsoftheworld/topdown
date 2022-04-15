using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUp : MonoBehaviour
{
    void Update()
    {
        if (this.transform.childCount == 2)
        {
            if (this.transform.GetChild(1).gameObject.GetComponent<StoreSelectButtonVars>() != null)
            {
                if (this.transform.GetChild(1).gameObject.GetComponent<StoreSelectButtonVars>().gameWeapon.activeSelf == true)
                {
                    this.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    this.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
                }
            }
            else
            {
                this.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
