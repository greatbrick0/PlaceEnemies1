using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Damage Over Time")]
public class DamageOverTime : StatusEffect
{
    [field: SerializeField]
    [field: Min(0)]
    public float damageInterval { get; protected set; } = 0.5f;
    private float damageTimer = 0.0f;

    public override void IncreaseTime(float timePassed)
    {
        damageTimer += timePassed;
        if(damageTimer >= damageInterval)
        {
            damageTimer = 0.0f;
            host.Hurt(Mathf.RoundToInt(intensity));
        }

        base.IncreaseTime(timePassed);
    }
}
