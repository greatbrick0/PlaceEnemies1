using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Abilty
{
    protected GameObject user;
    protected bool offCooldown = true;
    public float cooldownTime { get; protected set; }
    public float remainingCooldown = 0.0f;
    
    public float effectiveRange { get; protected set; }
    //effectie range is not automatically accurate 
    //effective range is meant to be read by AI so it knows how to use each ability
    public string description = "No description";

    public abstract bool Use(Vector3 targetPosition);

    protected abstract void SetVars();

    public Abilty(GameObject _user)
    {
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
}
