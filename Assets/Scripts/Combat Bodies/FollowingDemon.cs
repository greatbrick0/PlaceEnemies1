using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingDemon : NPCController
{
    [SerializeField]
    [Tooltip("The range at which the enemy will back away from the player, measured in units. One hexagon tile is about 4 units wide.")]
    private float minimumRange = 0.1f;
    [SerializeField]
    [Tooltip("The range at which the enemy will stop moving towards the player. ")]
    private float satisfiedRange = 1.0f;
    [SerializeField]
    [Tooltip("Whether the \"Satisfied Range\" is measured in a ratio of the first abilities effective range or in units. ")]
    private bool satisfiedRangeInRatio = false;
    [SerializeField]
    [Tooltip("The range at which the enemy will start attacking the player. ")]
    private float aggroRange = 1.0f;
    [SerializeField]
    [Tooltip("Whether the \"Aggro Range\" is measured in a ratio of the first abilities effective range or in units. ")]
    private bool aggroRangeInRatio = false;
    private float distanceToTarget;
    private Vector3 directionToTarget;

    protected override void Start()
    {
        base.Start();

        SetFirstAbility();
        foreach (Ability abilty in abilityList)
        {
            abilty.EnableCooldown();
        }

        satisfiedRange = satisfiedRangeInRatio ? satisfiedRange * abilityList[0].effectiveRange : satisfiedRange;
        aggroRange = aggroRangeInRatio ? aggroRange * abilityList[0].effectiveRange : aggroRange;
    }

    protected override void Update()
    {
        if (released)
        {
            targetList = CleanTargetList();
            if (targetList.Count > 0)
            {
                distanceToTarget = Vector3.Distance(transform.position, targetList[0].transform.position);
                directionToTarget = (targetList[0].transform.position - transform.position).normalized;

                transform.LookAt(targetList[0].transform.position);
                controlledVelocity = Vector3.zero * moveSpeed;

                if(distanceToTarget <= minimumRange)
                {
                    controlledVelocity = -directionToTarget * moveSpeed;
                }
                else if(distanceToTarget > satisfiedRange)
                {
                    controlledVelocity = -directionToTarget * moveSpeed;
                }
                if(distanceToTarget <= aggroRange)
                {
                    UseAbility(0, targetList[0].transform.position);
                }
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                controlledVelocity = Vector3.zero * moveSpeed;
            }
        }

        base.Update();
    }

    protected override bool FilterTarget(GameObject potentialTarget)
    {
        return potentialTarget.GetComponent<CombatBody>().team != this.team;
    }

    protected virtual void SetFirstAbility()
    {
        abilityList[0] = new BasicDemonBallAbility(gameObject);
    }
}
