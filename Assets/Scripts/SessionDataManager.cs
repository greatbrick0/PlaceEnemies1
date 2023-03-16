using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionDataManager
{
    public static List<Ability> playerLoadOut = new List<Ability>();
    public static int nightNum = 0;
    public static int savedPlayerHealth = 0;
    public static int currency = 0;
    public static bool usingTutorial = false;
    public static Dictionary<string, int> upgrades = new Dictionary<string, int>() { ["red"] = 0, ["blue"] = 0, ["green"] = 0 };

    public static void ResetSession()
    {
        Debug.Log("Data Reset");
        playerLoadOut = new List<Ability>();
        nightNum = 0;
        savedPlayerHealth = 0;
        currency = 0;
        usingTutorial = false;
        upgrades = new Dictionary<string, int>() { ["red"] = 0, ["blue"] = 0, ["green"] = 0 };
    }
}
