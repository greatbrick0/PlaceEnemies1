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
        GetNewTarget();
    }

    // Update is called once per frame
    private void Update()
    {
        // If there's no collision or the hit object is the target, move the projectile forward

        // Check if the target is null or has been destroyed
        if (enemy == null || !enemy.gameObject)
        {
            // If the target is null or has been destroyed, get a new target
            GetNewTarget();
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

    // Gets a new target by finding the closest combat body with the specified team
    private void GetNewTarget()
    {
        // Get an array of all the combat bodies with the specified team
        CombatBody[] targets = FindObjectsOfType<CombatBody>();

        // If there are no targets, return and do nothing
        if (targets.Length == 0)
        {
            enemy = null;
            return;
        }

        // Set the initial target to the first one in the array
        enemy = targets[0];

        // Loop through the rest of the targets and find the closest one on the enemy team
        for (int i = 1; i < targets.Length; i++)
        {
            if (targets[i].team == "enemy")
            {
                float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
                float newDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                if (newDistance < currentDistance)
                {
                    enemy = targets[i];
                }
            }
        }
    }
}
