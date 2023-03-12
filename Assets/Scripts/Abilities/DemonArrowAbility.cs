using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonArrowAbility : Ability
{
    GameObject arrowPrefab;
    GameObject arrowRef;

    public DemonArrowAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        arrowPrefab = AttackDict.attacks["DemonArrow"];
        cooldownTime = 2.0f;
        effectiveRange = 12.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Basic Arrow";
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
        arrowRef = user.GetComponent<CombatBody>().Instantiater(arrowPrefab, user.transform.parent);
        arrowRef.transform.position = user.transform.position;
        arrowRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        arrowRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        arrowRef.GetComponent<Attack>().FaceForward();
    }
}
