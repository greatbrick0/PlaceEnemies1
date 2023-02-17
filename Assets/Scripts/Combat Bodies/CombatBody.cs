using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CombatBody : Placeable
{
    protected Rigidbody rb;
    [SerializeField]
    protected bool released = false;
    [SerializeField]
    public string team = "enemy";
    [SerializeField]
    int bounty = 0;
    [SerializeField]
    protected int health = 2;
    [SerializeField]
    protected float baseMoveSpeed = 3.0f;
    protected float moveSpeed;
    protected Vector3 controlledVelocity;
    public int sourcesPreventingMovement = 0;
    public Vector3 forcedVelocity;
    public int sourcesPreventingAbilities = 0;
    [SerializeField]
    protected List<Ability> abilityList = new List<Ability>();
    [SerializeField]
    public List<StatusEffect> effectList = new List<StatusEffect>();
    private List<StatusEffect> timedEffects = new List<StatusEffect>();
    private List<StatusEffect> movementEffects = new List<StatusEffect>();

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        moveSpeed = baseMoveSpeed;
        if (abilityList.Count <= 0) abilityList.Add(new LockedAbility(gameObject));
    }

    public override void Release()
    {
        released = true;
    }

    public virtual int Hurt(int damageAmount=1)
    {
        int previousHealth = health;

        health -= Mathf.Max(damageAmount, 0);
        if(health <= 0)
        {
            Die();
        }

        return Mathf.Max(previousHealth - health, 0);
    }

    protected virtual void Update()
    {
        if (released)
        {
            rb.velocity = (sourcesPreventingMovement == 0 ? controlledVelocity : Vector3.zero) + forcedVelocity;
            UpdateCooldowns(Time.deltaTime);
            UpdateEffectTimes(Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void UpdateCooldowns(float delta)
    {
        foreach(Ability ii in abilityList)
        {
            if(ii.remainingCooldown > 0.0f)
            {
                ii.remainingCooldown -= 1.0f * delta;
                if (ii.remainingCooldown < 0.0f)
                {
                    ii.DisableCooldown();
                }
            }
        }
    }

    public GameObject Instantiater(GameObject prefab, Transform prefabParent)
    {
        return Instantiate(prefab, prefabParent);
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    protected virtual void UseAbility(int abilityIndex, Vector3 pos)
    {
        if (!released) return;
        if (sourcesPreventingAbilities > 0) return;
        if (abilityIndex >= abilityList.Count) return;

        if (abilityList[abilityIndex].Use(pos))
        {
            if(transform.GetChild(0).GetComponent<Animationcontroller>() != null)
            {
                transform.GetChild(0).GetComponent<Animationcontroller>().AbilityUsed();
            }
        }
    }

    //someone please clean this function, im too tired
    public void AddStatusEffect(StatusEffect newEffectType)
    {
        if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.Preserve) return;

        StatusEffect previousEffect = CheckForDuplicateEffects(newEffectType.effectName);

        if (previousEffect == null || newEffectType.behaviour == StatusEffect.DuplicateBahviours.Share)
        {
            InstantiateEffect(newEffectType);
        }
        else if (previousEffect == null && newEffectType.behaviour == StatusEffect.DuplicateBahviours.Preserve)
        {
            InstantiateEffect(newEffectType);
        }
        else if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.Overwrite)
        {
            previousEffect.RemoveEffect();
            Instantiate(newEffectType);
        }
        else if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.RefreshTime)
        {
            previousEffect.age = 0.0f;
        }
        else if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.SumTime)
        {
            previousEffect.IncreaseTime(newEffectType.lifeTime);
        }
        else if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.MultiplyIntensity)
        {
            previousEffect.intensity *= newEffectType.intensity;
        }
        else if (newEffectType.behaviour == StatusEffect.DuplicateBahviours.SumIntensity)
        {
            previousEffect.intensity += newEffectType.intensity;
        }

        if (newEffectType.affectsMoveSpeed) moveSpeed = CalculateMoveSpeed();
    }

    private void InstantiateEffect(StatusEffect effect)
    {
        StatusEffect newEffect = Instantiate(effect);

        newEffect.SetHost(gameObject.GetComponent<CombatBody>());
        effectList.Add(newEffect);
        if (newEffect.hasDuration) timedEffects.Add(newEffect);
        if (newEffect.affectsMoveSpeed) movementEffects.Add(newEffect);
        newEffect.ApplyInitialAffect();
    }

    private void UpdateEffectTimes(float delta)
    {
        for(int ii = 0; ii < timedEffects.Count; ii++)
        {
            timedEffects[ii].IncreaseTime(delta);
        }
    }

    public void RemoveEffectFromLists(StatusEffect effectToRemove)
    {
        effectList.Remove(effectToRemove);
        if (effectToRemove.hasDuration) timedEffects.Remove(effectToRemove);
        if (effectToRemove.affectsMoveSpeed)
        {
            movementEffects.Remove(effectToRemove);
            moveSpeed = CalculateMoveSpeed();
        }
    }

    private StatusEffect CheckForDuplicateEffects(string newEffectType)
    {
        for(int ii = 0; ii < effectList.Count; ii++)
        {
            if(effectList[ii].effectName == newEffectType)
            {
                return effectList[ii];
            }
        }
        return null;
    }

    private float CalculateMoveSpeed()
    {
        float calculatedSpeed = baseMoveSpeed;

        for(int ii = 0; ii < movementEffects.Count; ii++)
        {
            calculatedSpeed = movementEffects[ii].ApplyMovementAffect(calculatedSpeed);
        }

        return calculatedSpeed;
    }
}
