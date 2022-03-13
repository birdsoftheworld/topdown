using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrasResourceImages : MonoBehaviour
{
    public Player player;

    void Update()
    {
        
        if (player.lightAmmo > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (player.healthRestores > 0)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (player.heavyAmmo > 0)
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
