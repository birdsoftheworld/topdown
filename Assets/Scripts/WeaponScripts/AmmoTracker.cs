using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    public string num1;
    public string num2;
    public string num3;
    public string num4;
    public string num5;

    public GameObject txt;

    // Start is called before the first frame update
    void Start()
    {
        num1 = "-";
        num2 = "-";
        num3 = "-";
        num4 = "-";
        num5 = "-";
    }

    // Update is called once per frame
    void UpdateChecker()
    {
        string entry = " / \n |  | ";
        //num1.ToString

        entry = entry.Insert(10, num5);
        entry = entry.Insert(7, num4);
        entry = entry.Insert(4, num3);
        entry = entry.Insert(3, num2);
        entry = entry.Insert(0, num1);

        txt.GetComponent<TextMeshPro>().text = entry;
    }

    public void define1(string num)
    {
        num1 = num;
        UpdateChecker();
    }
    public void define2(string num)
    {
        num2 = num;
        UpdateChecker();
    }
    public void define3(string num)
    {
        num3 = num;
        UpdateChecker();
    }
    public void define4(string num)
    {
        num4 = num;
        UpdateChecker();
    }
    public void define5(string num)
    {
        num5 = num;
        UpdateChecker();
    }
}
