using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneInformation.setL(level);

        Application.LoadLevel("ProjectileTests");
    }
}
