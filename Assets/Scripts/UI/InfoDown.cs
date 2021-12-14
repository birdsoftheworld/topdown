using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDown : MonoBehaviour
{
    public ToggleWithInfo t;

    public RectTransform thisRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisRectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t.hovering == true || t.isOn == true)
        {
        } //check if thisRectTransform is less than twice that of the toggle. If it is, expand out by 2 and move down 1.

        //while attached ToggleWithInfo hovering == true or isOn == true, expand down until in final position. Else, slide up if out of hidden position
    }
}
