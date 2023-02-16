using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderGroup : MonoBehaviour
{
    [SerializeField]
    private DescBoxScript descBoxRef;

    public void DisplayDescription(string desc, string spellName)
    {
        descBoxRef.DisplaySpellInfo(spellName, desc);
    }
}
