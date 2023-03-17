using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImbuedChargeAbility : Ability
{
    GameObject ramPrefab;
    GameObject ramRef;

    public ImbuedChargeAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        ramPrefab = AttackDict.attacks["MagicDash"];
        upgradeLevel = SessionDataManager.upgrades["blue"];
        cooldownTime = CalculateUpgradeStat(upgradeLevel);
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 6;
        colour = ColourTypes.Blue;
    }
    public override void SetDisplayVars()
    {
        description = "Dash forward with a ram, damaging enemies on contact. ";
        displayName = "Imbued Charge";
    }
    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            MakeProjectile(Vector3.ClampMagnitude(targetPosition - user.transform.position, effectiveRange));
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
        return 5.5f - (level * 0.6f);
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        ramRef = user.GetComponent<CombatBody>().Instantiater(ramPrefab, user.transform.parent);
        ramRef.GetComponent<MagicChargeScript>().followHost = user.transform;
        ramRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        ramRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        ramRef.GetComponent<Attack>().FaceForward();
        Dash movementEffect = ramRef.GetComponent<MagicChargeScript>().movementEffect;
        movementEffect.forcedVelocity = targetPosition.normalized * 9.0f;
        user.GetComponent<CombatBody>().AddStatusEffect(movementEffect);
    }
}
