using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Force Motion")]
public class ForceMovement : RootOrSilence
{
    [Tooltip("The amount the host CombatBody will be displaced over one second.")]
    public Vector3 forcedVelocity = Vector3.zero;

    public override void ApplyInitialAffect()
    {
        base.ApplyInitialAffect();
        host.forcedVelocity += forcedVelocity;
    }

    public override void RemoveEffect()
    {
        host.forcedVelocity -= forcedVelocity;
        base.RemoveEffect();
    }
}
