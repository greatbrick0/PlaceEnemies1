using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Root Or Silence")]
public class RootOrSilence : StatusEffect
{
    [SerializeField]
    private bool preventsMovement = true;
    [SerializeField]
    private bool preventsAbilities = true;

    public override void ApplyInitialAffect()
    {
        base.ApplyInitialAffect();
        host.sourcesPreventingMovement += preventsMovement ? 1 : 0;
        host.sourcesPreventingAbilities += preventsAbilities ? 1 : 0;
    }

    public override void RemoveEffect()
    {
        host.sourcesPreventingMovement -= preventsMovement ? 1 : 0;
        host.sourcesPreventingAbilities -= preventsAbilities ? 1 : 0;
        base.RemoveEffect();
    }
}
