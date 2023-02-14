using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Movement Multiplier")]
public class SpeedMultiplier : StatusEffect
{
    public override float ApplyMovementAffect(float previousMoveSpeed)
    {
        return previousMoveSpeed * intensity;
    }
}
