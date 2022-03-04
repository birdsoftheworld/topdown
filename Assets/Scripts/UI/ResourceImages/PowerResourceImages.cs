using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerResourceImages : MonoBehaviour
{
    public Player player;

    public Sprite charged;
    public Sprite uncharged;

    // Update is called once per frame
    void Update()
    {
        if (player.waiter4Changed == true)
        {
            if (player.waiting4 > 0)
            {
                this.gameObject.GetComponent<Image>().sprite = uncharged;
            }
            else
            {
                this.gameObject.GetComponent<Image>().sprite = charged;
            }

            player.waiter4Changed = false;
        }

    }
}
