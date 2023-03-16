using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class Ability
{
    protected GameObject user;

    protected bool offCooldown = true;
    [field: SerializeField]
    public float cooldownTime { get; protected set; }
    [NonSerialized]
    public float remainingCooldown = 0.0f;

    [field: SerializeField]
    [field: Min(0.01f)]
    public float effectiveRange { get; protected set; }
    //effective range is not automatically accurate 
    //effective range is meant to be read by AI so that it knows how to use each ability

    public string displayName { get; protected set; } = "";
    public string description { get; protected set; } = "No description";

    public enum ColourTypes
    {
        NA,
        Red,
        Green,
        Blue,
        White,
        Demon
    }
    public ColourTypes colour { get; protected set; } = ColourTypes.NA;
    public int upgradeLevel = 0;

    public int ID = -1;
    public abstract bool Use(Vector3 targetPosition);

    protected abstract void SetVars();

    public abstract void SetDisplayVars();

    public Ability(GameObject _user = null)
    {
        if (_user == null) return;
        user = _user;

        SetVars();
    }

    public void DisableCooldown()
    {
        offCooldown = true;
    }

    public void EnableCooldown()
    {
        offCooldown = false;
        remainingCooldown = cooldownTime;
    }

    public void ManualInit(GameObject _user)
    {
        user = _user;
        SetVars();
    }
}
