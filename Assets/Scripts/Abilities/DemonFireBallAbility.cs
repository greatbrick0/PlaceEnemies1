using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFireBallAbility : Ability
{
    GameObject firePrefab;
    GameObject fireRef;

    public int projectileCount = 1;
    public float timeBetweenShots = 1.0f;

    public DemonFireBallAbility(GameObject _user = null) : base(_user)
    {

    }

    protected override void SetVars()
    {
        firePrefab = AttackDict.attacks["FireBall"];
        cooldownTime = 4.2f + Random.Range(0.0f, 0.5f);
        effectiveRange = 10.0f;
        SetDisplayVars();
        ID = 0;
        colour = ColourTypes.Demon;
        projectileCount = 2;
        timeBetweenShots = 0.5f;
    }

    public override void SetDisplayVars()
    {
        description = "";
        displayName = "Fire Ball";
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

    private void MakeProjectile(Vector3 targetPosition)
    {
        fireRef = user.GetComponent<CombatBody>().Instantiater(firePrefab, user.transform.parent);
        fireRef.transform.position = user.transform.position;
        fireRef.GetComponent<Attack>().moveDirection = targetPosition;
        fireRef.GetComponent<Attack>().team = user.GetComponent<CombatBody>().team;
        fireRef.GetComponent<Attack>().FaceForward();
    }

    IEnumerator MakeMultipleProjectiles()
    {
        for (int ii = 0; ii < projectileCount; ii++)
        {
            MakeProjectile(user.transform.forward);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}

