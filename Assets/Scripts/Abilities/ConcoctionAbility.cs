using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcoctionAbility : Ability
{
    GameObject potionPrefab;
    GameObject potionRef;

    private float maxTravelTime = 1.0f;
    private float minTravelTime = 1.0f;

    public ConcoctionAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        potionPrefab = AttackDict.attacks["PotionBottle"];
        cooldownTime = 4.0f;
        effectiveRange = 13.5f;
        SetDisplayVars();
        ID = 6;
        colour = ColourTypes.Blue;
        maxTravelTime = 1.1f;
        minTravelTime = 0.3f;
    }

    public override void SetDisplayVars()
    {
        description = "Throw a bottle that spills corrosive liquid on the ground, slowing anything that touches it. ";
        displayName = "Concoction";
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
        potionRef = user.GetComponent<CombatBody>().Instantiater(potionPrefab, user.transform.parent);
        potionRef.transform.position = user.transform.position;
        potionRef.GetComponent<Attack>().lifetime = Mathf.Clamp((targetPosition - user.transform.position).magnitude / potionRef.GetComponent<Attack>().speed, minTravelTime, maxTravelTime);
        potionRef.GetComponent<Attack>().moveDirection = targetPosition - user.transform.position;
        potionRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        potionRef.GetComponent<Attack>().FaceForward();
    }
}
