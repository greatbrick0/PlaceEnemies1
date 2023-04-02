using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleAbility : Ability
{
    GameObject sicklePrefab;
    GameObject sickleRef;

    public int projectileCount = 1;
    public float timeBetweenShots = 1.0f;

    public SickleAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        sicklePrefab = AttackDict.attacks["Sickle"];
        upgradeLevel = SessionDataManager.upgrades["red"];
        cooldownTime = 6.0f;
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 4;
        colour = ColourTypes.Red;
        projectileCount = (int)CalculateUpgradeStat(upgradeLevel);
        timeBetweenShots = 0.2f;
    }

    public override void SetDisplayVars()
    {
        description = "Throw spinning sickles and catch them when they return. ";
        displayName = "Sickles";
        upgradeType = "Amount:";
    }

    private float CalculateUpgradeStat(int level)
    {
        return 2 + (level * 1);
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            user.GetComponent<CombatBody>().StopAllCoroutines();
            user.GetComponent<CombatBody>().StartCoroutine(MakeMultipleProjectiles());
            EnableCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator MakeMultipleProjectiles()
    {
        for (int ii = 0; ii < projectileCount; ii++)
        {
            MakeProjectile(user.transform.forward, ii);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void MakeProjectile(Vector3 targetPosition, int num)
    {
        sickleRef = user.GetComponent<CombatBody>().Instantiater(sicklePrefab, user.transform.parent);
        sickleRef.transform.position = user.transform.position;
        sickleRef.GetComponent<Attack>().moveDirection = targetPosition;
        sickleRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        sickleRef.GetComponent<BoomerangScript>().homeTarget = user.transform;
        sickleRef.GetComponent<BoomerangScript>().forwardTime -= num * 0.1f;
    }
}
