using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBallScript : Attack
{
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
