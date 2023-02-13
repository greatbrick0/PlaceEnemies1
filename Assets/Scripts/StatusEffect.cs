using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [SerializeField]
    public Sprite icon { get; private set; }
    [SerializeField]
    public string effectName { get; protected set; } = "";
    public enum DuplicateBahviours
    {
        Ignore,
        Preserve,
        Overwrite,
        RefreshTime,
        IncreaseIntensity,
        SumIntensity,
        SumTime,
        SumVars
    }
    public DuplicateBahviours behaviour { get; protected set; } = DuplicateBahviours.RefreshTime;
    [SerializeField]
    protected float intensity = 1.0f;

    [SerializeField]
    public bool affectsMoveSpeed { get; protected set; } = false;
    [SerializeField]
    public bool hasDuration { get; protected set; } = true;
    public float age = 0.0f;
    [SerializeField]
    public float lifeTime { get; protected set; } = 3.0f;

    protected CombatBody host;
    public int hostListIndex { get; private set; }

    public void SetHost(CombatBody newHost, int newIndex)
    {
        host = newHost;
        hostListIndex = newIndex;
    }

    protected virtual float ApplyMovementAffect(float previousMoveSpeed)
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

    protected virtual void RemoveEffect()
    {
        Destroy(this);
    }
}
