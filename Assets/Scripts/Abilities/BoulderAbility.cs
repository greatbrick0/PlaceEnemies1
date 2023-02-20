using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAbility : Ability
{
    GameObject boulderPrefab;
    GameObject boulderRef;

    public BoulderAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        boulderPrefab = AttackDict.attacks["Boulder"];
        cooldownTime = 5.0f;
        effectiveRange = 16.0f;
        SetDisplayVars();
        ID = 4;
        colour = ColourTypes.Red;
    }

    public override void SetDisplayVars()
    {
        description = "Shoot a large boulder that slowly rolls down the arena.";
        displayName = "Boulder";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            MakeProjectile(targetPosition);
            EnableCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        boulderRef = user.GetComponent<CombatBody>().Instantiater(boulderPrefab, user.transform.parent);
        boulderRef.transform.position = user.transform.position;
        boulderRef.GetComponent<BoulderScript>().moveDirection = targetPosition - user.transform.position;
        boulderRef.GetComponent<BoulderScript>().team = user.GetComponent<CombatBody>().team;
        boulderRef.GetComponent<BoulderScript>().FaceForward();
    }
}
