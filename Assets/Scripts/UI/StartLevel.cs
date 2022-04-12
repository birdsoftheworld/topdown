using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneInformation.setL(level);

        SceneManager.LoadScene("ProjectileTests");
    }
}
