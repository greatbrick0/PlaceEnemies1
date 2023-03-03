using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : NPCController
{
    protected override void Start()
    {
        base.Start();

        abilityList[0] = new BasicDemonBallAbility(gameObject);
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
            if (targetList.Count > 0) //different enemies can have different movement or pathfinding
            {
                transform.LookAt(targetList[0].transform.position);
                controlledVelocity = Vector3.zero * moveSpeed;
                UseAbility(0, targetList[0].transform.position);
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
        //This example filter lets only rival teams be targeted
        return potentialTarget.GetComponent<CombatBody>().team != this.team;
    }
}
