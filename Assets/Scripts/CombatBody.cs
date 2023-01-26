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
    [SerializeField]
    protected List<Abilty> abilityList;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        moveSpeed = baseMoveSpeed;
        abilityList = new List<Abilty> { new LockedAbility(gameObject) };
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
        UpdateCooldowns(Time.deltaTime);
    }

    private void UpdateCooldowns(float delta)
    {
        foreach(Abilty ii in abilityList)
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
}
