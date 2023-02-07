using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homingmissile : MonoBehaviour
{
    //Speed of projectile
    public float speed;

    //The target object
    private CombatBody enemy;

    private void Start()
    {
        // Get a new target
        GetNewTarget(FindObjectsOfType<CombatBody>());
    }

    // Update is called once per frame
    private void Update()
    {
        // If there's no collision or the hit object is the target, move the projectile forward

        // Check if the target is null or has been destroyed
        if (enemy == null || !enemy.gameObject)
        {
            // If the target is null or has been destroyed, get a new target
            enemy = GetNewTarget(FindObjectsOfType<CombatBody>());
        }

        // Check if there is still no target
        if (enemy == null)
        {
            // If there's no target, move the projectile forward
            transform.position += transform.forward * speed * Time.deltaTime;
            return;
        }

        // Calculate the direction vector from the current position to the target position
        Vector3 direction = (enemy.transform.position - transform.position).normalized;

        // Check for collisions along the path to the next position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, speed * Time.deltaTime))
        {
            // If the hit object is not the target, return and do nothing
            if (hit.collider.gameObject != enemy.gameObject)
                return;

            // Deal 5 damage to the target
            enemy.Hurt(5);

            // Destroy the projectile
            Destroy(gameObject);
        }
        else
        {
            // Move the projectile forward towards the target
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private CombatBody GetNewTarget(CombatBody[] homingTargets)
    {
        CombatBody newHomingTarget = null;

        if (homingTargets.Length == 0)
        {
            return null;
        }

        for (int ii = 0; ii < homingTargets.Length; ii++) //set the initial homing target to the first target of the enemy team
        {
            if(homingTargets[ii].team == "enemy")
            {
                newHomingTarget = homingTargets[ii];
            }
        }
        if(newHomingTarget == null)
        {
            return null;
        }

        float currentDistance = Mathf.Pow(20.0f, 2);
        float newDistance;

        for (int ii = 1; ii < homingTargets.Length; ii++)
        {
            if (homingTargets[ii].team == "enemy")
            {
                newDistance = Vector3.SqrMagnitude(transform.position - homingTargets[ii].transform.position);
                if (newDistance < currentDistance)
                {
                    currentDistance = newDistance;
                    newHomingTarget = homingTargets[ii];
                }
            }
        }
        return newHomingTarget;
    }
}
