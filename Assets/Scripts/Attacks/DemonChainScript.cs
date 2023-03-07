using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonChainScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the effect appied on hit.")]
    public LockDown lassoEffect;

    protected override void Start()
    {
        base.Start();

        lassoEffect = Instantiate(lassoEffect);
        lassoEffect.trapPoint = transform.position;
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(lassoEffect);
        CompleteAttack();
    }
}
