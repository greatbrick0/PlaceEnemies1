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

    private void Update()
    {
        transform.position = linkedTransform.position + offset;
    }
}
