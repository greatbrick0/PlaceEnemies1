using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVisual : MonoBehaviour
{
    private StatusEffect linkedEffect;
    private Transform linkedTransform;
    [SerializeField]
    private Vector3 offset;

    public virtual void Initialize(StatusEffect effect)
    {
        linkedEffect = effect;
        linkedTransform = effect.host.transform;

        transform.parent = linkedTransform.parent;
        transform.position = linkedTransform.position + offset;
    }

    protected virtual void Update()
    {
        if (linkedTransform == null)
        {
            Destroy(this.gameObject); 
            return;
        }
        transform.position = linkedTransform.position + offset;
    }
}
