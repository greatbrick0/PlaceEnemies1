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
        abilityDict.Add(new BoulderAbility());
        abilityDict.Add(new JumpAbility());
        abilityDict.Add(new GravityAbility());
        abilityDict.Add(new HammerAbility());
        abilityDict.Add(new LightningAbility());
        abilityDict.Add(new ConcoctionAbility());
        //slice
        abilityDict.Add(new BigBombAbility());
        //imbued charge
    }
}
