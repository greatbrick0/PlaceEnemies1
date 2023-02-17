using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowAbility : Ability
{
    GameObject arrowPrefab;
    GameObject arrowRef;

    public MagicArrowAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        arrowPrefab = AttackDict.attacks["PiercingArrow"];
        cooldownTime = 3.5f;
        effectiveRange = 15.0f;
        SetDisplayVars();
        ID = 1;
        colour = ColourTypes.Red;
    }

    public override void SetDisplayVars()
    {
        description = "Shoot an arrow that passes through enemies.";
        displayName = "Piercing Arrow";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if(offCooldown)
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
        arrowRef = user.GetComponent<CombatBody>().Instantiater(arrowPrefab, user.transform.parent);
        arrowRef.transform.position = user.transform.position;
        arrowRef.transform.GetComponent<MagicArrowScript>().moveDirection = targetPosition - user.transform.position;
        arrowRef.transform.GetComponent<MagicArrowScript>().team = user.GetComponent<CombatBody>().team;
        arrowRef.transform.GetComponent<MagicArrowScript>().FaceForward();
    }
}
