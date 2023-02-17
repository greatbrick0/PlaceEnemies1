using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderGroup : MonoBehaviour
{
    [SerializeField]
    private EditSpellsManager managerRef;
    [SerializeField]
    private DescBoxScript descBoxRef;

    public void DisplayDescription(string desc, string spellName)
    {
        descBoxRef.DisplaySpellInfo(spellName, desc);
    }

    public void SendDragInfo(Vector2 mousePos)
    {
        managerRef.DraggingSpell(mousePos);
    }

    public void SendEndDrag(Vector2 mousePos, SpellHolder spellHolderRef)
    {
        managerRef.EndDrag(mousePos, spellHolderRef);
    }
}
