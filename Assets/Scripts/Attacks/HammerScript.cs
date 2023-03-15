using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : Attack
{
    [HideInInspector]
    public GameObject followHost;
    public Vector3 followOffset = Vector3.forward * 1.7f;

    [Tooltip("Whether or not the hammer is moving with the body that summoned it. ")]
    public bool followingOwner = true;

    protected override void Start()
    {
        base.Start();

        canHit = false;
    }

    protected override void Update()
    {
        base.Update();

        if(followingOwner && followHost != null) transform.position = followHost.transform.position + followOffset;
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
