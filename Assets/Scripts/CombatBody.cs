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
    protected List<Abilty> abilityList = new List<Abilty> { new LockedAbility() };

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        moveSpeed = baseMoveSpeed;
    }

    public override void Release()
    {
        released = true;
    }

    public virtual int Hurt(int damageAmount=1)
    {
        int previousHealth = health;

        health -= Mathf.Max(damageAmount, 0);

        return Mathf.Max(previousHealth - health, 0);
    }
}
