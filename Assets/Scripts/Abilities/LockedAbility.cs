using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedAbility : Ability
{
    public LockedAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        cooldownTime = 5.0f;
        effectiveRange = 0.1f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.NA;
    }

    public override void SetDisplayVars()
    {
        description = "This ability is locked.";
        displayName = "Locked";
    }

    public override bool Use(Vector3 targetPosition)
    {
        Debug.Log("Ability locked");
        return false;
    }
}
