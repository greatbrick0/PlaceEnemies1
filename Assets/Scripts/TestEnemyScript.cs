using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : NPCController
{
    protected override void Start()
    {
        base.Start();

        abilityList[0] = new MagicArrowAbility(gameObject);
        foreach (Abilty abilty in abilityList)
        {
            abilty.EnableCooldown();
        }
    }

    protected override void Update()
    {
        base.Update();

        if(targetList.Count > 0) //different enemies can have different movement or pathfinding
        {
            transform.LookAt(targetList[0].transform.position);
            rb.velocity = Vector3.zero;
            UseAbility(0, targetList[0].transform.position);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }

    protected override bool FilterTarget(GameObject potentialTarget)
    {
        //This example filter lets only rival teams be targeted
        return potentialTarget.GetComponent<CombatBody>().team != this.team;
    }
}
