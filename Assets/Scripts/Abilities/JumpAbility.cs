using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{
    GameObject pulsePrefab;
    GameObject pulseRef;

    public JumpAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        pulsePrefab = AttackDict.attacks["JumpWave"];
        upgradeLevel = SessionDataManager.upgrades["green"];
        cooldownTime = 6.0f;
        effectiveRange = CalculateUpgradeStat(upgradeLevel);
        SetDisplayVars();
        ID = 9;
        colour = ColourTypes.Green;
    }

    public override void SetDisplayVars()
    {
        description = "Damage enemies around you, then jump to a new location. ";
        displayName = "Jump";
        upgradeType = "Range:";
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
        return 7.0f + (level * 1.5f);
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        pulseRef = user.GetComponent<CombatBody>().Instantiater(pulsePrefab, user.transform.parent);
        pulseRef.transform.position = user.transform.position;
        pulseRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        Dash movementEffect = pulseRef.GetComponent<JumpWaveScript>().movementEffect;
        movementEffect.forcedVelocity = targetPosition.normalized * 9.0f;
        movementEffect.ChangeLifeTime(targetPosition.magnitude / 9.0f);
        user.GetComponent<CombatBody>().AddStatusEffect(movementEffect);
    }
}
