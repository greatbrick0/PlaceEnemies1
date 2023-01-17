using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Abilty
{
    protected GameObject user;
    protected bool offCooldown = true;
    protected float cooldownTime = 1.0f;
    public float remainingCooldown = 0.0f;

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

    protected void EnableCooldown()
    {
        offCooldown = false;
        remainingCooldown = cooldownTime;
    }
}
