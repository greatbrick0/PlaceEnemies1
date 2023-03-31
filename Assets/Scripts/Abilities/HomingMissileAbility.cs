using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileAbility : Ability
{
    GameObject homingPrefab;
    GameObject homingRef;

    public int projectileCount = 1;
    public float timeBetweenShots = 1.0f;
    public float degreesFromStraight = 0.0f;

    public HomingMissileAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        homingPrefab = AttackDict.attacks["GhostHand"];
        upgradeLevel = SessionDataManager.upgrades["green"];
        cooldownTime = 2.0f;
        effectiveRange = 15.0f;
        SetDisplayVars();
        ID = 12;
        colour = ColourTypes.Green;
        projectileCount = (int)CalculateUpgradeStat(upgradeLevel);
        timeBetweenShots = 0.2f;
        degreesFromStraight = 15;
    }

    public override void SetDisplayVars()
    {
        description = "Shoot hands that home in on nearby enemies.";
        displayName = "Ghost Hands";
        upgradeType = "Amount:";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            user.GetComponent<CombatBody>().StopAllCoroutines();
            user.GetComponent<CombatBody>().StartCoroutine(MakeMultipleProjectiles(targetPosition));
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
        return 2 + (level * 1);
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        homingRef = user.GetComponent<CombatBody>().Instantiater(homingPrefab, user.transform.parent);
        homingRef.transform.position = user.transform.position;
        homingRef.GetComponent<Attack>().moveDirection = targetPosition;
        homingRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        homingRef.GetComponent<Attack>().FaceForward();
    }

    IEnumerator MakeMultipleProjectiles(Vector3 targetPosition)
    {
        Vector3 newDirection = (targetPosition - user.transform.position).normalized;

        for (int ii = 0; ii < projectileCount; ii++)
        {
            MakeProjectile(Quaternion.Euler(0, Mathf.Pow(-1, ii) * degreesFromStraight, 0) * newDirection);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
