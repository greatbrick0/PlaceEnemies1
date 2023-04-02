using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileScript : Attack
{
    private List<CombatBody> detectedTargets = new List<CombatBody>();
    
    private CombatBody homingTarget;
    private bool hasValidHomingTarget = false;
    [SerializeField] [Tooltip("The speed that the projectile will rotate towards its target.")]
    float homingPower = 8.0f;

    // Use this method to set up your reference to the child Transform component
    protected override void Start()
    {
        base.Start();
        hasParticles = true;
    }

    protected override void Apply(CombatBody recentHit)
    {
        recentHit.Hurt(power);
        CompleteAttack();
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Update()
    {
        detectedTargets = ClearInvalidTargets(detectedTargets);
        homingTarget = GetNewTarget(detectedTargets);

        if(hasValidHomingTarget)
        {
            moveDirection = Vector3.Lerp(moveDirection, (homingTarget.transform.position - transform.position), homingPower * Time.deltaTime);
            FaceForward();
        }

        base.Update();
    }

    public void ExtraCollision0Enter(GameObject detectedObject)
    {
        if(detectedObject.GetComponent<CombatBody>() == null) return;
        if (!FilterHitTarget(detectedObject.GetComponent<CombatBody>())) return;

        detectedTargets.Add(detectedObject.GetComponent<CombatBody>());
    }

    private CombatBody GetNewTarget(List<CombatBody> homingTargets)
    {
        CombatBody newHomingTarget = null;

        if (homingTargets.Count == 0)
        {
            hasValidHomingTarget = false;
            return null;
        }

        newHomingTarget = homingTargets[0];

        float currentDistance = Vector3.SqrMagnitude(transform.position - newHomingTarget.transform.position); ;
        float newDistance;

        for (int ii = 1; ii < homingTargets.Count; ii++)
        {
            if (homingTargets[ii].team != this.team)
            {
                newDistance = Vector3.SqrMagnitude(transform.position - homingTargets[ii].transform.position);
                if (newDistance < currentDistance)
                {
                    currentDistance = newDistance;
                    newHomingTarget = homingTargets[ii];
                }
            }
        }
        hasValidHomingTarget = true;
        return newHomingTarget;
    }

    private List<CombatBody> ClearInvalidTargets(List<CombatBody> dirtyList)
    {
        List<CombatBody> cleanList = new List<CombatBody>();

        for(int ii = 0; ii < dirtyList.Count; ii++)
        {
            if(dirtyList[ii] != null)
            {
                cleanList.Add(dirtyList[ii]);
            }
        }

        return cleanList;
    }
}
