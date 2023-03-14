using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : Attack
{
    protected override void Start()
    {

        base.Start();
        hasParticles = true;
    }
    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
        CompleteAttack();
    }
}
