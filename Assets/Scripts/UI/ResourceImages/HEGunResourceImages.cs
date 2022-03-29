using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEGunResourceImages : MonoBehaviour
{
    public PlayerHEGun source;
    public Player player;

    void Update()
    {
        if (source.gameObject.activeSelf == true)
        {
            if (player.ammoChanged == true)
            {
                for (int i = 2; i > -1; i--)
                {
                    this.transform.GetChild(i).gameObject.SetActive(true);
                }

                if (source.ammo > 0)
                {
                    for (int i = 0; i < source.ammo; i++)
                    {
                        this.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                //player.ammoChanged = false;
            }
        }
    }
}