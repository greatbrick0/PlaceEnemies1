using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStabAbility : Ability
{
    GameObject slashPrefab;
    GameObject slashRef;

    [SerializeField]
    private float headStart = 0.8f;

    public DemonStabAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        slashPrefab = AttackDict.attacks["DemonStab"];
        cooldownTime = 0.8f;
        effectiveRange = 1.5f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Demon Slash";
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
        slashRef = user.GetComponent<CombatBody>().Instantiater(slashPrefab, user.transform.parent);
        slashRef.transform.position = user.transform.position + (targetPosition - user.transform.position).normalized * headStart;
        slashRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        slashRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        slashRef.GetComponent<Attack>().FaceForward();
    }
}
