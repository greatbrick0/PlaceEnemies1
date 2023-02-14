using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [field: SerializeField]
    public Sprite icon { get; private set; }
    [field: SerializeField]
    public string effectName { get; protected set; } = "";
    public enum DuplicateBahviours
    {
        Share, //share lets multiple effects stay on the host
        Preserve, //preserve prevents any new effects from being added if one already exists
        Overwrite, //overwrite completely replaces the previous effect
        RefreshTime, //refresh time resets the age of the existing effect
        SumTime, //sum time stacks the lifetime of the new effect onto the existing effect
        MultiplyIntensity, //multiply intensity multiplies the intensity of the new and existing effects
        SumIntensity, //sum intensity adds the intensity of the new and existing effects
    }
    [field: SerializeField]
    public DuplicateBahviours behaviour { get; protected set; } = DuplicateBahviours.RefreshTime;
    [SerializeField]
    public float intensity = 1.0f;

    [field: SerializeField]
    public bool affectsMoveSpeed { get; protected set; } = false;
    [field: SerializeField]
    public bool hasDuration { get; protected set; } = true;
    [HideInInspector]
    public float age = 0.0f;
    [field: SerializeField] [field: Min(0)]
    public float lifeTime { get; protected set; } = 3.0f;

    protected CombatBody host;
    public int hostListIndex { get; private set; }

    public void SetHost(CombatBody newHost)
    {
        host = newHost;
    }

    public virtual float ApplyMovementAffect(float previousMoveSpeed)
    {
        return previousMoveSpeed;
    }
    
    public virtual void IncreaseTime(float timePassed)
    {
        age += timePassed;
        if(hasDuration && age >= lifeTime)
        {
            RemoveEffect();
        }
    }

    public virtual void RemoveEffect()
    {
        host.RemoveEffectFromLists(this);
        Destroy(this);
    }

    public void IncreaseLifetime(float addedTime)
    {
        lifeTime += addedTime;
    }
}
