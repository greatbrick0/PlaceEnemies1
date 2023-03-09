using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : Ability
{
    GameObject boltPrefab;
    GameObject boltRef;

    public int projectileCount = 1;
    public float timeBetweenShots = 1.0f;
    public float radiusFromCentre = 0.0f;

    public LightningAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        boltPrefab = AttackDict.attacks["LightningBolt"];
        cooldownTime = 2.0f;
        effectiveRange = 8.0f;
        SetDisplayVars();
        ID = 2;
        colour = ColourTypes.Green;
        projectileCount = 3;
        timeBetweenShots = 0.4f;
        radiusFromCentre = 1.0f;
    }

    public override void SetDisplayVars()
    {
        description = "Strike multiple lightning bolts in a small area. ";
        displayName = "Lightning";
    }

    public override bool Use(Vector3 targetPosition)
    {
        if (offCooldown)
        {
            targetPosition = Vector3.ClampMagnitude(targetPosition - user.transform.position, effectiveRange) + user.transform.position;
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

    IEnumerator MakeMultipleProjectiles(Vector3 targetPosition)
    {
        Vector3 offSet = Vector3.zero;

        for (int ii = 0; ii < projectileCount; ii++)
        {
            offSet = ii % 2 == 0 ? Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * radiusFromCentre : Vector3.zero;
            MakeProjectile(targetPosition + offSet);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void MakeProjectile(Vector3 targetPosition)
    {
        boltRef = user.GetComponent<CombatBody>().Instantiater(boltPrefab, user.transform.parent);
        boltRef.transform.position = targetPosition;
        boltRef.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        boltRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
    }
}
