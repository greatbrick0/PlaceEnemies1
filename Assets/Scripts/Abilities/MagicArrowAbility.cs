using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowAbility : Abilty
{
    GameObject arrowPrefab;
    GameObject arrowRef;

    public MagicArrowAbility(GameObject _user) : base(_user)
    {

    }

    protected override void SetVars()
    {
        arrowPrefab = Resources.Load<GameObject>("Attacks/MagicArrow");
        cooldownTime = 3.5f;
        effectiveRange = 15.0f;
        description = "Shoot an arrow that passes through enemies.";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if(offCooldown)
        {
            MakeSphere(targetPosition);
            EnableCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeSphere(Vector3 targetPosition)
    {
        arrowRef = user.GetComponent<CombatBody>().Instantiater(arrowPrefab, user.transform.parent);
        arrowRef.transform.position = user.transform.position;
        arrowRef.transform.GetComponent<MagicArrowScript>().moveDirection = targetPosition - user.transform.position;
        arrowRef.transform.GetComponent<MagicArrowScript>().team = user.GetComponent<CombatBody>().team;
        arrowRef.transform.GetComponent<MagicArrowScript>().FaceForward();
    }
}
