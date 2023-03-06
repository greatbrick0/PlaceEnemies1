using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWaveScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the movement effect appied to the user.")]
    public Dash movementEffect;

    protected override void Start()
    {
        base.Start();

        movementEffect = Instantiate(movementEffect);
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
    }
}
