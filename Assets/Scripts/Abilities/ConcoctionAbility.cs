using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcoctionAbility : Ability
{
    GameObject potionPrefab;
    GameObject potionRef;

    public ConcoctionAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        potionPrefab = AttackDict.attacks["Potion"];
        cooldownTime = 4.0f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 4;
        colour = ColourTypes.Blue;
    }

    public override void SetDisplayVars()
    {
        description = "Throw a bottle that spills corrosive liqued on the ground, slowing anything that thouches it. ";
        displayName = "Concoction";
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
        potionRef = user.GetComponent<CombatBody>().Instantiater(potionPrefab, user.transform.parent);
        potionRef.transform.position = user.transform.position;
        potionRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        potionRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        potionRef.GetComponent<Attack>().FaceForward();
    }
}
