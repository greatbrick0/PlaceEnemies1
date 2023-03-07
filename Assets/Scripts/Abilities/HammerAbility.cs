using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAbility : Ability
{
    GameObject weaponPrefab;
    GameObject weaponRef;

    public HammerAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        weaponPrefab = AttackDict.attacks["Hammer"];
        cooldownTime = 4.3f;
        effectiveRange = 3.0f;
        SetDisplayVars();
        ID = 7;
        colour = ColourTypes.Red;
    }

    public override void SetDisplayVars()
    {
        description = "Summon a hammer that smashes the area in front of you. ";
        displayName = "Hammer";
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
        weaponRef = user.GetComponent<CombatBody>().Instantiater(weaponPrefab, user.transform.parent);
        weaponRef.transform.position = user.transform.position;
        weaponRef.GetComponent<HammerScript>().followHost = user;
        weaponRef.GetComponent<HammerScript>().followOffset = (targetPosition - user.transform.position).normalized * 2.7f;
        weaponRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        weaponRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        weaponRef.GetComponent<Attack>().FaceForward();
    }
}
