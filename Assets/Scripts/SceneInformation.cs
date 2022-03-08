using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneInformation
{
    public static int levelNum = 0;

    public static void setL(int level)
    {
        levelNum = level;
    }

    public static int getL()
    {
        return levelNum;
    }
}
