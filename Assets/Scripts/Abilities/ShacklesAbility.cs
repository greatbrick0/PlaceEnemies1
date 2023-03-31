using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShacklesAbility : Ability
{
    GameObject shacklesPrefab;
    GameObject shacklesRef;

    public ShacklesAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        shacklesPrefab = AttackDict.attacks["Shackles"];
        upgradeLevel = SessionDataManager.upgrades["blue"];
        cooldownTime = CalculateUpgradeStat(upgradeLevel);
        effectiveRange = 12.0f;
        SetDisplayVars();
        ID = 8;
        colour = ColourTypes.Blue;
    }

    public override void SetDisplayVars()
    {
        description = "Shoot a set of shackles that prevent an enemy from moving for a short time.";
        displayName = "Shackles";
        upgradeType = "Cooldown:";
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
        return 3.4f - (level * 0.3f);
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        shacklesRef = user.GetComponent<CombatBody>().Instantiater(shacklesPrefab, user.transform.parent);
        shacklesRef.transform.position = user.transform.position;
        shacklesRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        shacklesRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        shacklesRef.GetComponent<Attack>().FaceForward();
    }
}
