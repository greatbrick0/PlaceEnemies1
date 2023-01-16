using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedAbility : Abilty
{
    public override bool Use()
    {
        Debug.Log("Ability locked");
        return false;
    }
}
