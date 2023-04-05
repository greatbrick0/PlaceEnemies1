using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : Attack
{
    public Transform homeTarget;
    [SerializeField]
    public float forwardTime = 1.0f;
    [SerializeField]
    private float stillTime = 0.7f;
    private int attackStage = 0;

    [SerializeField]
    private float backSpeed;

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
    }

    protected override void Update()
    {
        if (attackStage == 0 && age >= forwardTime)
        {
            speed = 0;
            attackStage++;
            hitList.Clear();
        }
        else if (attackStage == 1 && age >= forwardTime + stillTime)
        {
            speed = backSpeed;
            attackStage++;
            hitList.Clear();
        }
        else if (attackStage == 2 && homeTarget != null)
        {
            moveDirection = (homeTarget.position - transform.position).normalized;
            if(Vector3.SqrMagnitude(transform.position - homeTarget.position) <= 0.5f) CompleteAttack();
        }
        transform.GetChild(0).Rotate(0, 720 * Time.deltaTime, 0);

        base.Update();
    }
}
