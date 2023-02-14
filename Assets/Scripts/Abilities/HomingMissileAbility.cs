using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileAbility : Ability
{
    GameObject homingPrefab;
    GameObject homingRef;

    public HomingMissileAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        homingPrefab = AttackDict.attacks["GhostHand"];
        cooldownTime = 2.0f;
        effectiveRange = 15.0f;
        description = "Shoot a hand that moves towards enemies.";
        displayName = "Ghost Hands";
        ID = 2;
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
        homingRef = user.GetComponent<CombatBody>().Instantiater(homingPrefab, user.transform.parent);
        homingRef.transform.position = user.transform.position;
        homingRef.transform.GetComponent<HomingMissileScript>().moveDirection = targetPosition - user.transform.position;
        homingRef.transform.GetComponent<HomingMissileScript>().team = user.GetComponent<CombatBody>().team;
        homingRef.transform.GetComponent<HomingMissileScript>().FaceForward();
    }
}
