using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDemon : FollowingDemon
{
    protected override void SetFirstAbility()
    {
        abilityList[0] = new BasicDemonBallAbility(gameObject);
    }
}
