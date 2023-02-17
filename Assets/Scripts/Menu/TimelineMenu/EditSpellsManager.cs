using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSpellsManager : MonoBehaviour
{
    public List<Ability> currentLoadOut = new List<Ability>();
    public List<EquipSlotScript> loadOutSlots = new List<EquipSlotScript>();

    private void Start()
    {
        for(int ii = 0; ii < 4; ii++)
        {
            currentLoadOut.Add(new LockedAbility());
        }

        for (int ii = 0; ii < loadOutSlots.Count; ii++)
        {
            loadOutSlots[ii].managerRef = this;
        }
    }

    public void DraggingSpell(Vector2 mousePos)
    {
        for(int ii = 0; ii < loadOutSlots.Count; ii++)
        {
            loadOutSlots[ii].transform.GetChild(0).gameObject.SetActive(loadOutSlots[ii].DetectCurrentlyHovered(mousePos));
        }
    }

    public void EndDrag(Vector2 mousePos, SpellHolder spellHolderRef)
    {
        for (int ii = 0; ii < loadOutSlots.Count; ii++)
        {
            loadOutSlots[ii].transform.GetChild(0).gameObject.SetActive(false);
            if (loadOutSlots[ii].DetectCurrentlyHovered(mousePos))
            {
                loadOutSlots[ii].AttemptAttachSpell(spellHolderRef);
            }
        }
    }
}
