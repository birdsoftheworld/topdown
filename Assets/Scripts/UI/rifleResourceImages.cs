using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rifleResourceImages : MonoBehaviour
{
    public PlayerRifle source;
    public Player player;

    /*void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }*/

    void Update()
    {
        if (player.ammoChanged == true)
        {
            RectTransform rt = this.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(300 - (60 * source.ammo), 60);
        }
    }
}
