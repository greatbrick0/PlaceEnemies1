using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : Attack
{
    private int attackStage = 0;
    [HideInInspector]
    public GameObject followHost;
    [HideInInspector]
    public Vector3 followOffset = Vector3.forward * 1.5f;

    [SerializeField]
    [Tooltip("The speed at which the hammer visually falls. ")]
    private float fallSpeed = 3.0f;

    protected override void Start()
    {
        base.Start();

        canHit = false;
    }

    protected override void Update()
    {
        base.Update();

        if(age < lifetime * 0.9f)
        {
            if(followHost != null) transform.position = followHost.transform.position + followOffset;
        }
        else if (attackStage == 0)
        {
            attackStage = 1;
            canHit = true;
        }

        if(attackStage == 1)
        {
            transform.GetChild(0).position += Vector3.down * fallSpeed * Time.deltaTime;
        }
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
