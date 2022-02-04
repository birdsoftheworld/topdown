using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHolder : MonoBehaviour
{
    public GameObject[] draggables;

    public GameObject[] slots;

    public int selectableItems;

    public GameObject descriptionBox;

    // Start is called before the first frame update
    void Start()
    {
        int a = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<CanBeDragged>() != null)
            {
                a++;
            }
        }

        draggables = new GameObject[a];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<CanBeDragged>() != null)
            {
                draggables[i] = this.transform.GetChild(i).gameObject;
            }
        }
    }

    public void Realize()
    {
        for (int i = selectableItems; i > -1; i--)
        {
            for (int a = this.transform.childCount - 1; a > -1; a--)
            //for (int a = 0; a < this.transform.childCount; a++)
            {
                if (this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>() != null)
                {
                    if (this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>().location == i)
                    {
                        this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>().target.SetAsFirstSibling();
                    }
                    else if (this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>().location > selectableItems - 1)
                    {
                        this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>().target.SetAsLastSibling();
                    }
                }
            }
        }
    }
}
