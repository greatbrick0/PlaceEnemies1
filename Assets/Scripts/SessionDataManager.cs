using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDataManager
{
    public static List<Ability> playerLoadOut = new List<Ability>();
    public static int nightNum = 0;
    public static int savedPlayerHealth = 0;
    public static int currency;

    public static void ResetSession()
    {
        playerLoadOut = new List<Ability>();
        nightNum = 0;
        savedPlayerHealth = 0;
        currency = 0;
    }
}
