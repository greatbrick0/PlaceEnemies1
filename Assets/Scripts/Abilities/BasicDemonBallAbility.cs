using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDemonBallAbility : Ability
{
    GameObject ballPrefab;
    GameObject ballRef;

    public BasicDemonBallAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
       ballPrefab = AttackDict.attacks["BasicDemonBall"];
        cooldownTime = 5.0f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Basic Demon Ball";
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
