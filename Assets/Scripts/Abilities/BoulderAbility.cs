using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAbility : Ability
{
    GameObject boulderPrefab;
    GameObject boulderRef;

    public BoulderAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        boulderPrefab = AttackDict.attacks["Boulder"];
        upgradeLevel = SessionDataManager.upgrades["red"];
        cooldownTime = 6.5f;
        effectiveRange = 16.0f;
        SetDisplayVars();
        ID = 2;
        colour = ColourTypes.Red;
    }

    public override void SetDisplayVars()
    {
        description = "Shoot a large boulder that slowly rolls down the arena.";
        displayName = "Boulder";
        upgradeType = "Speed:";
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

    private float CalculateUpgradeStat(int level)
    {
        return 6 + (level * 3);
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        boulderRef = user.GetComponent<CombatBody>().Instantiater(boulderPrefab, user.transform.parent);
        boulderRef.transform.position = user.transform.position;
        boulderRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        boulderRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        boulderRef.GetComponent<Attack>().FaceForward();
        boulderRef.GetComponent<Attack>().speed = CalculateUpgradeStat(upgradeLevel);
    }
}
