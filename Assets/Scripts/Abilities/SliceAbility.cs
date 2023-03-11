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
        effectiveRange = 7.2f;
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
            MakeProjectile(Vector3.ClampMagnitude(targetPosition - user.transform.position, effectiveRange));
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
        swordRef.GetComponent<SliceSwordScript>().followHost = user.transform;
        swordRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        swordRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        swordRef.GetComponent<Attack>().FaceForward();
        Dash movementEffect = swordRef.GetComponent<SliceSwordScript>().movementEffect;
        movementEffect.forcedVelocity = targetPosition.normalized * 9.0f;
        user.GetComponent<CombatBody>().AddStatusEffect(movementEffect);
    }
}
