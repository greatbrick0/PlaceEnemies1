using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : CombatBody
{
    protected List<GameObject> targetList;

    [SerializeField]
    private float targetDetectionRadius = 30.0f;
    private SphereCollider targetDetector;

    protected virtual void OnEnable()
    {
        targetDetector = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        targetDetector.isTrigger = true;
        targetDetector.radius = targetDetectionRadius;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CombatBody>() != null)
        {
            targetList.Add(other.gameObject);
        }
    }

}
