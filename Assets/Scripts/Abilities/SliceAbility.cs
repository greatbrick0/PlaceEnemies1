using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAbility : Ability
{
    GameObject swordPrefab;
    GameObject swordRef;

    public SliceAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        swordPrefab = AttackDict.attacks["DashBlade"];
        cooldownTime = 5.5f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 4;
        colour = ColourTypes.Red;
    }
    public override void SetDisplayVars()
    {
        description = "Dash forward with a sword, damaging enemies on contact. ";
        displayName = "Slice";
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
        swordRef = user.GetComponent<CombatBody>().Instantiater(swordPrefab, user.transform.parent);
        swordRef.transform.position = user.transform.position;
        swordRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        swordRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        swordRef.GetComponent<Attack>().FaceForward();
    }
}
