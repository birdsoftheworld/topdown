using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDown : MonoBehaviour
{
    public ToggleWithInfo t;

    public RectTransform thisRectTransform;

    public RectTransform toggleRectTransform;

    public Sprite inactive;
    public Sprite active;

    // Start is called before the first frame update
    void Start()
    {
        thisRectTransform = this.gameObject.GetComponent<RectTransform>();

        toggleRectTransform = t.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (t.hovering == true || t.isOn == true)
        {
            if (thisRectTransform.localPosition.y > 0)
            {
                thisRectTransform.localPosition -= new Vector3(0, 4, 0);
            }
            if (thisRectTransform.localPosition.y < 0)
            {
                thisRectTransform.localPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (thisRectTransform.localPosition.y < (toggleRectTransform.localPosition.y * 4))
            {
                thisRectTransform.localPosition += new Vector3(0, 3, 0);
            }
        }
    }

    public void Change()
    {
        if (this.gameObject.GetComponent<Image>().sprite == inactive)
        {
            this.gameObject.GetComponent<Image>().sprite = active;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = inactive;
        }
    }
}
