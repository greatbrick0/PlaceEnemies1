using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotScript : MonoBehaviour
{
    [HideInInspector]
    public EditSpellsManager managerRef;
    [SerializeField]
    private int siblingIndex = 0;

    [HideInInspector]
    public Vector2 spellHolderPostion = Vector2.zero;

    [SerializeField]
    private Vector2 hoverBounds = Vector2.one * 100;

    [SerializeField]
    private string acceptedSpellTypeGroup = "rgb";
    
    public SpellHolder heldSpell;

    private void Start()
    {
        spellHolderPostion = transform.position;
    }

    public bool DetectCurrentlyHovered(Vector2 mousePos)
    {
        bool withinXBounds = (mousePos.x <= transform.position.x + hoverBounds.x) && (mousePos.x >= transform.position.x - hoverBounds.x);
        bool withinYBounds = (mousePos.y <= transform.position.y + hoverBounds.y) && (mousePos.y >= transform.position.y - hoverBounds.y);
        return withinXBounds && withinYBounds;
    }

    private void AttachSpell(SpellHolder spell)
    {
        spell.equipped = true;
        spell.equippedPos = spellHolderPostion;
        spell.equippedSlotRef = this;
        heldSpell = spell;
        managerRef.currentLoadOut[siblingIndex] = heldSpell.abilityRef;
    }

    public void RemoveSpell()
    {
        if (heldSpell == null) return;
        heldSpell.equipped = false;
        heldSpell.equippedSlotRef = null;
        heldSpell = null;
    }

    public bool AttemptAttachSpell(SpellHolder spell)
    {
        if (spell.spellTypeGroup != acceptedSpellTypeGroup) return false;

        if(spell.equippedSlotRef == null)
        {
            RemoveSpell();
            AttachSpell(spell);
            return true;
        }
        if(spell.equippedSlotRef != this)
        { //swap with sibling
            return false; //currently not supported
        }
        else
        {
            return false;
        }
    }
}
