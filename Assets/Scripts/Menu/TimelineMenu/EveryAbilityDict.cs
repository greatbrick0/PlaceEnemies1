using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryAbilityDict : MonoBehaviour
{
    public static List<Ability> abilityDict { get; private set; } = new List<Ability>();

    private void OnEnable()
    {
        abilityDict.Add(new MagicArrowAbility());
        abilityDict.Add(new HomingMissileAbility());
        abilityDict.Add(new ShacklesAbility());
    }
}
