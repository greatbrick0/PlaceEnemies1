using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShacklesAbility : Ability
{
    GameObject shacklesPrefab;
    GameObject shacklesRef;

    public ShacklesAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        shacklesPrefab = AttackDict.attacks["Shackles"];
        cooldownTime = 3.0f;
        effectiveRange = 12.0f;
        description = "Shoot a set of shackles that prevent an enemy from moving for a short time.";
        displayName = "Shackles";
        ID = 3;

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
        shacklesRef = user.GetComponent<CombatBody>().Instantiater(shacklesPrefab, user.transform.parent);
        shacklesRef.transform.position = user.transform.position;
        shacklesRef.transform.GetComponent<ShacklesScript>().moveDirection = targetPosition - user.transform.position;
        shacklesRef.transform.GetComponent<ShacklesScript>().team = user.GetComponent<CombatBody>().team;
        shacklesRef.transform.GetComponent<ShacklesScript>().FaceForward();
    }
}
