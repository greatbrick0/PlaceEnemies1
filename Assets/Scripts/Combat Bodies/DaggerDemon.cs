using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerDemon : WaitingDemon
{
    protected override void SetFirstAbility()
    {
        abilityList[0] = new DemonStabAbility(gameObject);
    }
}
