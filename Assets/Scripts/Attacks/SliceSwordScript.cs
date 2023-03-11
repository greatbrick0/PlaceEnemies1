using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSwordScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the movement effect appied to the user.")]
    public Dash movementEffect;

    [HideInInspector]
    public Transform followHost;

    protected override void Start()
    {
        base.Start();

        movementEffect = Instantiate(movementEffect);
    }

    protected override void Update()
    {
        if (followHost != null) transform.position = followHost.position;
        else CompleteAttack();

        base.Update();
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
