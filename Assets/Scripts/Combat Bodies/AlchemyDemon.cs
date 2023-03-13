using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyDemon :FollowingDemon
{
    protected override void SetFirstAbility()
    {
        abilityList[0] = new DemonPotionAbility(gameObject);
    }
}
