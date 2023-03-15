using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingDemon : NPCController
{
    protected float distanceToTarget;
    protected Vector3 directionToTarget;
    protected Transform targetTransform;
    protected Vector3 targetPosition;

    protected float stateTime = 0.0f;
    [field: SerializeField]
    protected float chargeTime { get; private set; } = 0.7f;
    [field: SerializeField]
    protected float downTime { get; private set; } = 4.0f;
    [field: SerializeField]
    protected float failedChargeTime { get; private set; } = 2.0f;
    [field: SerializeField]
    protected float circleRange { get; private set; } = 5.5f;
    [field: SerializeField]
    protected float circleWidth { get; private set; } = 0.7f;
    [field: SerializeField]
    protected float aggroRange { get; private set; } = 2.0f;
    [field: SerializeField]
    protected float circleAngle { get; private set; } = 90;
    [field: SerializeField]
    protected bool runStraight { get; private set; } = true;


    protected override void Start()
    {
        base.Start();

        SetFirstAbility();
        foreach (Ability abilty in abilityList)
        {
            abilty.EnableCooldown();
        }
    }

    protected override void Update()
    {
        if (released)
        {
            targetList = CleanTargetList();
            if (targetList.Count > 0)
            {
                Behaviour();
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

    protected virtual void Behaviour()
    {
        targetTransform = targetList[0].transform;

        stateTime += 1.0f * Time.deltaTime;

        if(stateTime < downTime)
        {
            distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
            directionToTarget = (targetTransform.position - transform.position).normalized;

            transform.LookAt(targetTransform.position);
            if (distanceToTarget <= circleRange - circleWidth) controlledVelocity = -directionToTarget * moveSpeed;
            else if (distanceToTarget > circleRange + circleWidth) controlledVelocity = directionToTarget * moveSpeed;
            else controlledVelocity = (Quaternion.Euler(0, circleAngle, 0) * directionToTarget) * moveSpeed;
        }
        else if(stateTime < downTime + chargeTime)
        {
            controlledVelocity = Vector3.zero;
            transform.LookAt(targetTransform.position);
            directionToTarget = (targetTransform.position - transform.position).normalized;
        }
        else
        {
            if (!runStraight) directionToTarget = (targetTransform.position - transform.position).normalized;
            controlledVelocity = directionToTarget * moveSpeed;
            transform.LookAt(targetTransform.position);
            distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            if (stateTime > downTime + chargeTime + failedChargeTime || distanceToTarget < aggroRange)
            {
                stateTime = 0.0f;
                UseAbility(0, transform.position + directionToTarget);
            }
        }
    }

    protected virtual void SetFirstAbility()
    {
        abilityList[0] = new DemonSlashAbility(gameObject);
    }
}
