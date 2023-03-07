using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonChainAbility : Ability
{
    GameObject chainPrefab;
    GameObject chainRef;

    public DemonChainAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        chainPrefab = AttackDict.attacks["DemonChain"];
        cooldownTime = 4.8f;
        effectiveRange = 7.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Demon Chain";
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
        chainRef = user.GetComponent<CombatBody>().Instantiater(chainPrefab, user.transform.parent);
        chainRef.transform.position = user.transform.position;
        chainRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        chainRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        chainRef.GetComponent<Attack>().FaceForward();
    }
}
