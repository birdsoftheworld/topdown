using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpResourceImages : MonoBehaviour
{
    public Sprite[] images;

    public HealthTest playerHealth;

    void Update()
    {
        if (playerHealth.iFrames > 0)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = images[playerHealth.curHealth];
        }
    }
}