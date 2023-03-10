using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBombAbility : Ability
{
    GameObject bombPrefab;
    GameObject bombRef;

    public BigBombAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        bombPrefab = AttackDict.attacks["BigBomb"];
        cooldownTime = 4.0f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 5;
        colour = ColourTypes.Green;
    }

    public override void SetDisplayVars()
    {
        description = "Throw a bomb that explodes on contact. ";
        displayName = "Bomb";
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
        bombRef = user.GetComponent<CombatBody>().Instantiater(bombPrefab, user.transform.parent);
        bombRef.transform.position = user.transform.position;
        bombRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        bombRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        bombRef.GetComponent<Attack>().FaceForward();
    }
}
