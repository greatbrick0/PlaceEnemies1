using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChargeScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the movement effect appied to the user.")]
    public Dash movementEffect;

    [SerializeField]
    [Tooltip("The reference to the effect that will be applied on hit. ")]
    public ForceMovement knockBackEffect;
    [SerializeField]
    [Tooltip("The reference to the other effect that will be applied on hit. ")]
    public StatusEffect slowEffect;

    [HideInInspector]
    public Transform followHost;

    protected override void Start()
    {
        base.Start();

        movementEffect = Instantiate(movementEffect);
        knockBackEffect = Instantiate(knockBackEffect);
        slowEffect = Instantiate(slowEffect);
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
        Vector3 pushDirection = recentHit.transform.position - transform.position;
        knockBackEffect.forcedVelocity = pushDirection.normalized * 6.0f;
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(knockBackEffect);
        recentHit.AddStatusEffect(slowEffect);
    }
}
