using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBallScript : Attack
{
    [SerializeField]
    private int tooMuchHealth = 50;

    // Start is called before the first frame update
    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team == this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        if (recentHit.health < tooMuchHealth) recentHit.SetHealth(Mathf.Max(recentHit.health + power, tooMuchHealth));
    }
}
