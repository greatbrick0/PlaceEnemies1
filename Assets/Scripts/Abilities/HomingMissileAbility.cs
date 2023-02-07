using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissileAbility : Abilty
{
    GameObject homingPrefab;
    GameObject homingRef;

    public HommingMissileAbility(GameObject _user) : base(_user)
    {

    }

    protected override void SetVars()
    {
        homingPrefab = Resources.Load<GameObject>("Attacks/HomingMissile");
        cooldownTime = 2.0f;
        effectiveRange = 15.0f;
        description = "Shoot a hand that moves towards enemies.";
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
