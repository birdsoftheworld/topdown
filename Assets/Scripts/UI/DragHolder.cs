using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHolder : MonoBehaviour
{
    public GameObject[] draggables;

    public GameObject[] slots;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
