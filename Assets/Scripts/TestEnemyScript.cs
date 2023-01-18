using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : NPCController
{
    new private void Start()
    {
        base.Start();

        abilityList[0] = new CreateSphere(gameObject);
    }

    new private void Update()
    {
        base.Update();

        if(targetList.Count > 0) //different enemies can have different movement or pathfinding
        {
            transform.LookAt(targetList[0].transform.position);
            rb.velocity = Vector3.zero;
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
