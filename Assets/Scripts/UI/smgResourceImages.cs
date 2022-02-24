using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smgResourceImages : MonoBehaviour
{
    public PlayerCarbine source;

    // Update is called once per frame
    void Update()
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300 - (20 * source.ammo), 40);

        if (source.ammo > 14)
        {
            this.transform.parent.GetChild(2).gameObject.SetActive(false);
            this.transform.parent.GetChild(3).gameObject.SetActive(false);
            this.transform.parent.GetChild(4).gameObject.SetActive(false);
        }
        else if (source.ammo > 9)
        {
            this.transform.parent.GetChild(2).gameObject.SetActive(true);
            this.transform.parent.GetChild(3).gameObject.SetActive(false);
            this.transform.parent.GetChild(4).gameObject.SetActive(false);
        }
        else if (source.ammo > 4)
        {
            this.transform.parent.GetChild(2).gameObject.SetActive(true);
            this.transform.parent.GetChild(3).gameObject.SetActive(true);
            this.transform.parent.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            this.transform.parent.GetChild(2).gameObject.SetActive(true);
            this.transform.parent.GetChild(3).gameObject.SetActive(true);
            this.transform.parent.GetChild(4).gameObject.SetActive(true);
        }
    }
}