using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipOnLevel : MonoBehaviour
{
    public int level;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneInformation.getL() == level)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
