using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBombAbility : Ability
{
    GameObject bombPrefab;
    GameObject bombRef;

    public DemonBombAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        bombPrefab = AttackDict.attacks["DemonBomb"];
        cooldownTime = 2.3f;
        effectiveRange = 2.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Demon Bomb";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            MakeProjectile();
            EnableCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeProjectile()
    {
        bombRef = user.GetComponent<CombatBody>().Instantiater(bombPrefab, user.transform.parent);
        bombRef.transform.position = user.transform.position;
        bombRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
    }
}
