using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rifleResourceImages : MonoBehaviour
{
    public PlayerRifle source;

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<RectTransform>().rect = new Rect(20, 20, 20, 20);

        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300 - (60 * source.ammo), 60);

    }
}
