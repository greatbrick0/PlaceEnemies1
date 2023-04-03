using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingEffectVisual : EffectVisual
{
    private Vector3 point = Vector3.zero;
    [SerializeField]
    private Vector3 pointOffset = Vector3.zero;

    public override void Initialize(StatusEffect effect)
    {
        base.Initialize(effect);
        point = ((LockDown)effect).trapPoint + pointOffset;
    }

    protected override void Update()
    {
        base.Update();
        transform.LookAt(point);
    }
}
