using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDemon : FollowingDemon
{
    protected override void SetFirstAbility()
    {
        abilityList[0] = new DemonArrowAbility(gameObject);
    }
}
