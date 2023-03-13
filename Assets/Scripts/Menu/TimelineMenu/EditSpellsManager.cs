using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSpellsManager : MonoBehaviour
{
    public List<Ability> currentLoadOut = new List<Ability>();
    public List<EquipSlotScript> loadOutSlots = new List<EquipSlotScript>();

    private void Start()
    {
        if (SessionDataManager.playerLoadOut.Count == 0)
        {
            if (SessionDataManager.nightNum == 0)
            {
                //SessionDataManager.playerLoadOut.Add(new MagicArrowAbility());
                //SessionDataManager.playerLoadOut.Add(new JumpAbility());
                //SessionDataManager.playerLoadOut.Add(new ShacklesAbility());
            }
            else
            {
                for (int ii = 0; ii < 4; ii++)
                {
                    SessionDataManager.playerLoadOut.Add(new LockedAbility());
                }
            }
        }
        currentLoadOut = SessionDataManager.playerLoadOut;

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

    public void SaveLoadout()
    {
        SessionDataManager.playerLoadOut = currentLoadOut;
        print("loadout saved");
    }

    public void InitializeSlot(SpellHolder spellHolderRef, int holderIndex)
    {
        if (loadOutSlots[holderIndex].heldSpell != null) return;

        loadOutSlots[holderIndex].AttemptAttachSpell(spellHolderRef);
    }
}
