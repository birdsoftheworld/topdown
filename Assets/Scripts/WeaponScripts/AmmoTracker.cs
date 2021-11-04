using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTracker : MonoBehaviour
{
    public string num1;
    public string num2;
    public string num3;

    public GameObject txt;

    // Start is called before the first frame update
    void Start()
    {
        num1 = "-";
        num2 = "-";
        num3 = "-";
    }

    // Update is called once per frame
    void Update()
    {
        string entry = " |  | ";
        //num1.ToString

        entry = entry.Insert(6, num3);
        entry = entry.Insert(3, num2);
        entry = entry.Insert(0, num1);

        txt.GetComponent<UnityEngine.UI.Text>().text = entry;
    }

    public void define1(string num)
    {
        num1 = num;
    }
    public void define2(string num)
    {
        num2 = num;
    }
    public void define3(string num)
    {
        num3 = num;
    }
}
