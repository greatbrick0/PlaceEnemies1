using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcoctionSpillScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the effect appied on hit.")]
    public StatusEffect slowEffect;

    [SerializeField]
    [Tooltip("The time between hitting the same taarget twice. ")]
    private float reapplyTime = 0.6f;
    private float reapplyProgress = 0.0f;

    protected override void Start()
    {
        base.Start();

        slowEffect = Instantiate(slowEffect);
    }

    protected override void Update()
    {
        reapplyProgress += 1.0f * Time.deltaTime;
        if(reapplyProgress >= reapplyTime)
        {
            reapplyProgress = 0.0f;
            hitList.Clear();
        }

        base.Update();
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(slowEffect);
    }
}
