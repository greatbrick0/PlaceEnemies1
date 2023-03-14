using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingDemon : NPCController
{
    protected float distanceToTarget;
    protected Vector3 directionToTarget;
    protected Transform targetTransform;
    protected Vector3 targetPosition;

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

        targetPosition = Quaternion.Euler(0, -100, 0) * targetTransform.forward;
        distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (Vector3.Distance(Quaternion.Euler(0, 100, 0) * targetTransform.forward, transform.position) < distanceToTarget)
        {
            targetPosition = Quaternion.Euler(0, 100, 0) * targetTransform.forward;
            distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        }
        directionToTarget = (targetPosition - transform.position).normalized;
    }

    protected virtual void SetFirstAbility()
    {
        abilityList[0] = new DemonSlashAbility(gameObject);
    }
}
