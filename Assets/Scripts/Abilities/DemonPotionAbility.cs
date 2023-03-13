using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonPotionAbility : Ability
{
    GameObject bottlePrefab;
    GameObject bottleRef;

    public DemonPotionAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        bottlePrefab = AttackDict.attacks["DemonPotion"];
        cooldownTime = 5.5f;
        effectiveRange = 6.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Demon Potion";
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
        bottleRef = user.GetComponent<CombatBody>().Instantiater(bottlePrefab, user.transform.parent);
        bottleRef.transform.position = user.transform.position;
        bottleRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        bottleRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        bottleRef.GetComponent<Attack>().FaceForward();
    }
}
