using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowScript : Attack
{
    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply()
    {
        foreach(CombatBody target in hitList) //this passes through enemies, doing damage to each
        {
            target.Hurt(power);
            hitList.Clear();
        }
    }
}
