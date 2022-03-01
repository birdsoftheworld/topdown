using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolResourceImages : MonoBehaviour
{
    public PlayerPistol source;
    public Player player;

    /*void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }*/

    void Update()
    {
        if (source.gameObject.activeSelf == true)
        {
            if (player.ammoChanged == true)
            {
                for (int i = 5; i > -1; i--)
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