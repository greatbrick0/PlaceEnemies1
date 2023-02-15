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
        if(spellID >= SpellIDList.Count)
        {
            return SpellIDList[0];
        }
        if (SpellIDList[spellID] == null)
        {
            return SpellIDList[0];
        }
        return SpellIDList[spellID];
    }
    
}
