using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHolder : MonoBehaviour
{
    public GameObject[] draggables;

    public GameObject[] slots;

    public int selectableItems;

    public GameObject descriptionBox;

    void Awake()
    {
        MakeList();
    }

    // Start is called before the first frame update
    public void MakeList()
    {
        int a = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<CanBeDragged>() != null && this.transform.GetChild(i).gameObject.activeSelf == true)
            {
                a++;
            }
        }

        draggables = new GameObject[a];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<CanBeDragged>() != null && this.transform.GetChild(i).gameObject.activeSelf == true)
            {
                draggables[i] = this.transform.GetChild(i).gameObject; //ERROR HERE. Only grabs in order, even if they're disabled. Delete first before creating new??
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
                if (this.transform.GetChild(a).gameObject.GetComponent<CanBeDragged>() != null && this.transform.GetChild(a).gameObject.activeSelf == true)
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
