using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [field: SerializeField]
    public Sprite icon { get; private set; }
    [field: SerializeField]
    [field: Tooltip("Used to identify other copies of this effect when added to a CombatBody. ")]
    public string effectName { get; protected set; } = "";
    [SerializeField]
    [Tooltip("The prefab that will be instantiated to visualize the status effect. ")]
    private GameObject visualsPrefab;
    private GameObject visualsRef;
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
    [field: Tooltip("The behaviour of this effect when it is applied to a CombatBody that already has another copy of this effect.")]
    public DuplicateBahviours behaviour { get; protected set; } = DuplicateBahviours.RefreshTime;
    [SerializeField]
    [Tooltip("The number used for the primary affect of the effect.")]
    public float intensity = 1.0f;

    [field: SerializeField]
    [field: Tooltip("Determines if this effect will be taken into account when calculating movespeed.")]
    public bool affectsMoveSpeed { get; protected set; } = false;
    [field: SerializeField]
    [field: Tooltip("Determines whether the effect will disable itself after a set amount of time or must be removed from some other source.")]
    public bool hasDuration { get; protected set; } = true;
    [HideInInspector]
    public float age = 0.0f;
    [field: SerializeField]
    [field: Min(0)]
    [field: Tooltip("The seconds it takes for the effect to remove itself, if \"hasDuration\" is enabled.")]
    public float lifeTime { get; protected set; } = 3.0f;

    public CombatBody host { get; private set; }

    public void SetHost(CombatBody newHost)
    {
        host = newHost;
    }

    public virtual float ApplyMovementAffect(float previousMoveSpeed)
    {
        return previousMoveSpeed;
    }

    public virtual void ApplyInitialAffect() 
    {
        if(visualsPrefab != null)
        {
            visualsRef = Instantiate(visualsPrefab);
            visualsRef.GetComponent<EffectVisual>().Initialize(this);
        }
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
        if (visualsRef != null) Destroy(visualsRef);
        host.RemoveEffectFromLists(this);
        Destroy(this);
    }

    public void IncreaseLifetime(float addedTime)
    {
        lifeTime += addedTime;
    }
}
