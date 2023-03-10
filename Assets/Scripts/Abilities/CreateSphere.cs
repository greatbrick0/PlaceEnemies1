using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSphere : Ability
{
    GameObject spherePrefab;
    GameObject sphereRef;

    public CreateSphere(GameObject _user) : base(_user)
    {

    }

    protected override void SetVars()
    {
        spherePrefab = AttackDict.attacks["Sphere"];
        cooldownTime = 6.0f;
        effectiveRange = 30.0f;
        SetDisplayVars();
        colour = ColourTypes.NA;
    }

    public override void SetDisplayVars()
    {
        description = "This is a testing ability.";
        displayName = "Create Sphere";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            MakeSphere(targetPosition);
            EnableCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeSphere(Vector3 targetPosition)
    {
        sphereRef = user.GetComponent<CombatBody>().Instantiater(spherePrefab, user.transform.parent);
        sphereRef.transform.position = targetPosition;
    }
}
