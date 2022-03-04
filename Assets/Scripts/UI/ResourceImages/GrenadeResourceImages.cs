using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeResourceImages : MonoBehaviour
{
    public Player player;

    public Sprite charged;
    public Sprite uncharged;

    // Update is called once per frame
    void Update()
    {
        if (player.waiterChanged == true)
        {
            if (player.waiting3 > 0)
            {
                this.gameObject.GetComponent<Image>().sprite = uncharged;
            }
            else
            {
                this.gameObject.GetComponent<Image>().sprite = charged;
            }

            player.waiterChanged = false;
        }

    }
}
