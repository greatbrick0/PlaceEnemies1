using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Status Effects/Lock Down")]
public class LockDown : SpeedMultiplier
{
    [Tooltip("The point the host is not allowed to escape from. ")]
    public Vector3 trapPoint;
    [Tooltip("The distance the host is allowed to move before being restricted.")]
    public float trapRadius = 2.0f;

    public override void IncreaseTime(float timePassed)
    {
        host.transform.position = Vector3.ClampMagnitude(host.transform.position - trapPoint, trapRadius) + trapPoint;

        base.IncreaseTime(timePassed);
    }
}
