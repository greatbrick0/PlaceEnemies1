using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerDemon : FollowingDemon
{
    private int randomTargetIndex = 0;

    protected override bool FilterTarget(GameObject potentialTarget)
    {
        return potentialTarget.GetComponent<CombatBody>().team == this.team;
    }

    protected override void SetFirstAbility()
    {
        abilityList[0] = new DemonHealBallAbility(gameObject);
    }

    protected override void Behaviour()
    {
        if (randomTargetIndex >= targetList.Count) ChooseNewIndex();

        distanceToTarget = Vector3.Distance(transform.position, targetList[randomTargetIndex].transform.position);
        directionToTarget = (targetList[randomTargetIndex].transform.position - transform.position).normalized;

        transform.LookAt(targetList[randomTargetIndex].transform.position);
        controlledVelocity = Vector3.zero * moveSpeed;

        if (distanceToTarget <= minimumRange)
        {
            controlledVelocity = -directionToTarget * moveSpeed;
        }
        else if (distanceToTarget > satisfiedRange)
        {
            controlledVelocity = directionToTarget * moveSpeed;
        }
        else
        {
            controlledVelocity = Vector3.zero * moveSpeed;
        }

        if (distanceToTarget <= aggroRange)
        {
            if (UseAbility(0, targetList[randomTargetIndex].transform.position)) ChooseNewIndex();
        }
    }

    private void ChooseNewIndex()
    {
        if(targetList.Count > 0) randomTargetIndex = Random.Range(0, targetList.Count);
    }
}
