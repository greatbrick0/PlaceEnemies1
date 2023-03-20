using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDemon : NPCController
{
    private Vector3 chosenPosition;
    private bool reachedPosition = false;
    private Vector3 directionToTarget;
    private float distanceToTarget;
    [SerializeField]
    private float reattemptTime = 6.0f;

    protected override void Start()
    {
        base.Start();

        abilityList[0] = new DemonBombAbility(gameObject);
        foreach (Ability abilty in abilityList)
        {
            abilty.EnableCooldown();
        }

        chosenPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
    }

    protected override bool FilterTarget(GameObject potentialTarget)
    {
        return potentialTarget.GetComponent<CombatBody>().team != this.team;
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

    private void ChooseNewRandomPosition()
    {
        chosenPosition = targetList[0].transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
        reachedPosition = false;
        StartCoroutine(AttemptNewPosition());
    }

    private void Behaviour()
    {
        transform.LookAt(chosenPosition);

        if (reachedPosition)
        {
            if (UseAbility(0, transform.position)) ChooseNewRandomPosition(); 
            controlledVelocity = Vector3.zero;
        }
        else
        {
            directionToTarget = (chosenPosition - transform.position).normalized;
            distanceToTarget = Vector3.Distance(transform.position, chosenPosition);
            controlledVelocity = directionToTarget * moveSpeed;
            if (distanceToTarget < 0.5f)
            {
                reachedPosition = true;
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator AttemptNewPosition()
    {
        yield return new WaitForSeconds(reattemptTime);
        print("RetryPosition");
        ChooseNewRandomPosition();
    }
}
