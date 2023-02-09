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
    
    [SerializeField][field: Min(0.1f)]
    public float effectiveRange { get; protected set; }
    //effective range is not automatically accurate 
    //effective range is meant to be read by AI so that it knows how to use each ability

    [field: SerializeField]
    public string displayName { get; protected set; } = "";
    [TextArea]
    public string description = "No description";

    public abstract bool Use(Vector3 targetPosition);

    protected abstract void SetVars();

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
}
