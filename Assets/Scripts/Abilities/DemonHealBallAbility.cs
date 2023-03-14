using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealBallAbility : Ability
{
    GameObject ballPrefab;
    GameObject ballRef;

    public DemonHealBallAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        ballPrefab = AttackDict.attacks["HealBall"];
        cooldownTime = 5.0f;
        effectiveRange = 7.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Heal Ball";
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
        ballRef = user.GetComponent<CombatBody>().Instantiater(ballPrefab, user.transform.parent);
        ballRef.transform.position = user.transform.position;
        ballRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        ballRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        ballRef.GetComponent<Attack>().FaceForward();
    }
}
