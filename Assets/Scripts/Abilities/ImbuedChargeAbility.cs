using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImbuedChargeAbility : Ability
{
    GameObject ramPrefab;
    GameObject ramRef;

    public ImbuedChargeAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        ramPrefab = AttackDict.attacks["MagicDash"];
        cooldownTime = 5.5f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 6;
        colour = ColourTypes.Red;
    }
    public override void SetDisplayVars()
    {
        description = "Dash forward with a ram, damaging enemies on contact. ";
        displayName = "Imbued Charge";
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
        ramRef = user.GetComponent<CombatBody>().Instantiater(ramPrefab, user.transform.parent);
        ramRef.transform.position = user.transform.position;
        ramRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        ramRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        ramRef.GetComponent<Attack>().FaceForward();
    }
}
