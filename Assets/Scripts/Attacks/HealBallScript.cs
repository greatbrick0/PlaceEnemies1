using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBallScript : Attack
{
    [SerializeField]
    private int tooMuchHealth = 50;

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team == this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        if (recentHit.health < tooMuchHealth) recentHit.SetHealth(Mathf.Min(recentHit.health + power, tooMuchHealth));
    }
}
