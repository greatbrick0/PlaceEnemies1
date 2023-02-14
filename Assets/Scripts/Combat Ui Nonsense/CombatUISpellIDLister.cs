using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUISpellIDLister : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> SpellIDList;

    
    
    public Sprite GetSpellIcon(int spellID)
    {
        if (SpellIDList[spellID] != null)
        {
            return SpellIDList[spellID];
        }
        return SpellIDList[0];
    }
    
}
