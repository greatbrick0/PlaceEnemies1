using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Dash")]
public class Dash : ForceMovement
{
    [SerializeField]
    private bool preventsHits = true;
    [SerializeField]
    private bool preventsDamage = true;

    public override void ApplyInitialAffect()
    {
        base.ApplyInitialAffect();
        host.sourcesPreventingHits += preventsHits ? 1 : 0;
        host.sourcesPreventingDamage += preventsDamage ? 1 : 0;
    }

    public override void RemoveEffect()
    {
        host.sourcesPreventingHits -= preventsHits ? 1 : 0;
        host.sourcesPreventingDamage -= preventsDamage ? 1 : 0;
        base.RemoveEffect();
    }

    public void ChangeLifeTime(float newLifeTime)
    {
        lifeTime = newLifeTime;
    }
}
