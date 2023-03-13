using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDemon : FollowingDemon
{
    protected override void SetFirstAbility()
    {
        abilityList[0] = new DemonFireBallAbility(gameObject);
    }
}
