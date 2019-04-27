using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    static bool tutorialComplete = false;

    static bool fireLevelComplete = false;
    static bool waterLevelComplete = false;
    static bool earthLevelComplete = false;

    public static bool GetLevelComplete(string name)
    {
        switch (name)
        {
            case "fire":
                return fireLevelComplete;
            case "water":
                return waterLevelComplete;
            case "earth":
                return earthLevelComplete;
        }
        return false;
    }

    public static void SetLevelComplete(string name, bool value)
    {
        switch (name)
        {
            case "fire":
                fireLevelComplete = true;
                break;
            case "water":
                waterLevelComplete = true;
                break;
            case "earth":
                earthLevelComplete = true;
                break;
        }
    }

    public static bool GetTutorialComplete()
    {
        return tutorialComplete;
    }

    public static void SetTutorialComplete()
    {
        tutorialComplete = true;
    }
}
