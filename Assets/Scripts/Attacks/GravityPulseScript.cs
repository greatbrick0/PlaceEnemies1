using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPulseScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the effect appied on hit.")]
    public ForceMovement pullEffect;
    [SerializeField]
    [Tooltip("The reference to the effect appied to the user.")]
    public StatusEffect costEffect;

    protected override void Start()
    {
        base.Start();

        pullEffect = Instantiate(pullEffect);
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        Vector3 pullDirection = transform.position - recentHit.transform.position;
        pullEffect.forcedVelocity = (pullDirection  - pullDirection.normalized) / 2;
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(pullEffect);
    }
}
