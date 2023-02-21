using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerGravityPulseScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the effect appied on hit.")]
    public StatusEffect stunEffect;

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(stunEffect);
    }
}
