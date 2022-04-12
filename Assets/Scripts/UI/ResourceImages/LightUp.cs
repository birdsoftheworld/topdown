using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUp : MonoBehaviour
{
    void Update()
    {
        if (this.transform.childCount == 1)
        {
            this.gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
