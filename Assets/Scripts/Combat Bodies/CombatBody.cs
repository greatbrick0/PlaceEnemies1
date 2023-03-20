using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public abstract class CombatBody : Placeable
{
    protected Rigidbody rb;

    [SerializeField]
    [Tooltip("Whether the CombatBody has been released from its frozen state. Do not edit, this is in the inpsector for looking only.")]
    public bool released { get; protected set; } = false;

    [SerializeField]
    [Tooltip("Used for preventing friendly fire.")]
    public string team = "enemy";
    [SerializeField]
    [Tooltip("The amount of currency this CombatBody will reward for being killed.")]
    public int bounty = 0 ;

    [field: SerializeField]
    [field: Tooltip("The amount of damage this CombatBody can take before dying.")]
    public int health { get; protected set; } = 2;

    [SerializeField]
    [Tooltip("This CombatBody's default move speed. This value is used as a base when calculating relative boosts and slows.")]
    protected float baseMoveSpeed = 3.0f;
    public float moveSpeed { get; protected set; }
    [SerializeField]
    [Tooltip("The time it takes to accelerate to full speed from standing still.")]
    private float timeToFullSpeed = 0.3f;
    [SerializeField]
    [Tooltip("The time it takes to completely stop from full speed.")]
    private float timeToFullStop = 0.3f;

    [SerializeField]
    private GameObject deathSmoke ;

    [HideInInspector]
    public Vector3 controlledVelocity { get; protected set; }
    private Vector3 previousControlledVelocity;
    [HideInInspector]
    public int sourcesPreventingMovement = 0;
    [HideInInspector]
    public Vector3 forcedVelocity;
    [HideInInspector]
    public int sourcesPreventingAbilities = 0;
    private int _sourcesPreventingHits = 0;
    [HideInInspector]
    public int sourcesPreventingHits
    {
        get
        {
            return _sourcesPreventingHits;
        }
        set
        {
            _sourcesPreventingHits = value;
            //GetComponent<Collider>().enabled = _sourcesPreventingHits == 0;
            gameObject.layer = _sourcesPreventingHits == 0 ? LayerMask.NameToLayer("CombatBodies") : LayerMask.NameToLayer("Dodging");
        }
    }
    [HideInInspector]
    public int sourcesPreventingDamage = 0;
    [SerializeField]
    [Tooltip("All of the abilities that this CombatBody could use.")]
    protected List<Ability> abilityList = new List<Ability>();

    [SerializeField]
    [Tooltip("All of the effects that are currently attached to this CombatBody.")]
    public List<StatusEffect> effectList = new List<StatusEffect>();
    private List<StatusEffect> timedEffects = new List<StatusEffect>();
    private List<StatusEffect> movementEffects = new List<StatusEffect>();

    private Animator animator;


   
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        animator = GetComponent<Animator>();
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

        if(sourcesPreventingDamage == 0)
        {
            health -= Mathf.Max(damageAmount, 0);
            if (health <= 0)
            {
                Die();
            }
        }

        return Mathf.Max(previousHealth - health, 0);
    }

    public void SetHealth(int newHealth)
    {
        if (newHealth > 0) health = newHealth;
    }

    protected virtual void Update()
    {
        if (released)
        {
            CalculateMovement(Time.deltaTime);
            UpdateCooldowns(Time.deltaTime);
            UpdateEffectTimes(Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void CalculateMovement(float delta)
    {
         previousControlledVelocity = CalculateControlledAcceleration(controlledVelocity, previousControlledVelocity, delta);
        rb.velocity = (sourcesPreventingMovement == 0 ? previousControlledVelocity : Vector3.zero) + forcedVelocity;
    }

    private Vector3 CalculateControlledAcceleration(Vector3 desiredVelocity, Vector3 currentVelocity, float delta) 
    {

       if (desiredVelocity == currentVelocity) return currentVelocity;
        
        if(desiredVelocity.magnitude != 0)
        {
            currentVelocity += desiredVelocity.normalized * (moveSpeed / timeToFullSpeed) * delta;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        }
       else
       {
            //  currentVelocity -= currentVelocity.normalized * (moveSpeed / timeToFullStop) * delta;
            //   if (currentVelocity.magnitude < 0.05f) currentVelocity = Vector3.zero;
            currentVelocity -= currentVelocity.normalized * Mathf.Log10(currentVelocity.magnitude + 1) * (moveSpeed / timeToFullStop) * delta;
            if (currentVelocity.sqrMagnitude < (0.05f * 0.05f))
            {
                currentVelocity = Vector3.zero;
            }
        }

       return currentVelocity;
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
        if (deathSmoke != null)
            Instantiate(deathSmoke, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected virtual bool UseAbility(int abilityIndex, Vector3 pos)
    {
        if (!released) return false;
        if (sourcesPreventingAbilities > 0) return false;
        if (abilityIndex >= abilityList.Count) return false;

        if (abilityList[abilityIndex].Use(pos))
        {
            if(animator != null)
            {
                //sets attack1 trigger
                animator.SetTrigger("Attack1");
            }
            else if (transform.GetChild(0).GetComponent<Animationcontroller>() != null)
            {
                transform.GetChild(0).GetComponent<Animationcontroller>().AbilityUsed();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //someone please clean this function, im too tired
    public void AddStatusEffect(StatusEffect newEffectType)
    {
        StatusEffect previousEffect = CheckForDuplicateEffects(newEffectType.effectName);

        if (previousEffect == null) InstantiateEffect(newEffectType);
        else
        {
            switch (newEffectType.behaviour)
            {
                case StatusEffect.DuplicateBahviours.Share:
                    InstantiateEffect(newEffectType);
                    break;
                case StatusEffect.DuplicateBahviours.Preserve:
                    //do nothing
                    break;
                case StatusEffect.DuplicateBahviours.Overwrite:
                    previousEffect.RemoveEffect(); Instantiate(newEffectType);
                    break;
                case StatusEffect.DuplicateBahviours.RefreshTime:
                    previousEffect.age = 0.0f;
                    break;
                case StatusEffect.DuplicateBahviours.SumTime:
                    previousEffect.IncreaseTime(newEffectType.lifeTime);
                    break;
                case StatusEffect.DuplicateBahviours.MultiplyIntensity:
                    previousEffect.intensity *= newEffectType.intensity;
                    break;
                case StatusEffect.DuplicateBahviours.SumIntensity:
                    previousEffect.intensity += newEffectType.intensity;
                    break;
                default:
                    previousEffect.RemoveEffect(); Instantiate(newEffectType);
                    break;
            }
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
