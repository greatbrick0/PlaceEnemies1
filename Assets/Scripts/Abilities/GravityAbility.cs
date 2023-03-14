using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAbility : Ability
{
    GameObject pulsePrefab;
    GameObject pulseRef;

    public GravityAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        pulsePrefab = AttackDict.attacks["GravityPulse"];
        cooldownTime = 6.0f;
        effectiveRange = 4.0f;
        SetDisplayVars();
        ID = 7;
        colour = ColourTypes.Blue;
    }

    public override void SetDisplayVars()
    {
        description = "Create a pulse that brings enemies towards you, then stun them.";
        displayName = "Gravitic Pulse";
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
        pulseRef = user.GetComponent<CombatBody>().Instantiater(pulsePrefab, user.transform.parent);
        pulseRef.transform.position = user.transform.position;
        pulseRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        user.GetComponent<CombatBody>().AddStatusEffect(pulseRef.GetComponent<GravityPulseScript>().costEffect);
    }
}
