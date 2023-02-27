using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCController : CombatBody
{
    protected List<GameObject> targetList = new List<GameObject>();

    [SerializeField]
    private float targetDetectionRadius = 30.0f;
    private SphereCollider targetDetector;

    protected virtual void OnEnable()
    {
        targetDetector = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        targetDetector.isTrigger = true;
        targetDetector.radius = targetDetectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targetList.Count <= 20)
        {
            if (other.gameObject.GetComponent<CombatBody>() != null)
            {
                if (FilterTarget(other.gameObject))
                {
                    targetList.Add(other.gameObject);
                }
            }
        }
    }

    protected abstract bool FilterTarget(GameObject potentialTarget);

    protected override void Die()
    {
        if(team == "enemy")
        {
            GameObject managerRef = GameObject.Find("/ArenaSceneManager"); //i dont like this line
            if (managerRef != null) managerRef.GetComponent<PlacingManager>().DecrementEnemyCount();
        }
        base.Die();
    }
}
