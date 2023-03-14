using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSpellsManager : MonoBehaviour
{
    public List<Ability> currentLoadOut = new List<Ability>();
    public List<EquipSlotScript> loadOutSlots = new List<EquipSlotScript>();

    [SerializeField]
    GameObject doneButtonRef;

    [SerializeField]
    private GameObject tutorialRef;
    private int tutorialStage = 0;
    private bool showTutorial = false;

    private void Start()
    {
        if (SessionDataManager.playerLoadOut.Count == 0)
        {
            for (int ii = 0; ii < 4; ii++)
            {
                SessionDataManager.playerLoadOut.Add(new LockedAbility());
            }
        }
        currentLoadOut = SessionDataManager.playerLoadOut;

        for (int ii = 0; ii < loadOutSlots.Count; ii++)
        {
            loadOutSlots[ii].managerRef = this;
        }

        showTutorial = SessionDataManager.usingTutorial && SessionDataManager.nightNum <= 1;
        tutorialRef.SetActive(showTutorial);
        doneButtonRef.SetActive(CountValidSpells() >= 3);
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
        doneButtonRef.SetActive(CountValidSpells() >= 3);
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

    public void NextTutorialPrompt()
    {
        tutorialStage++;
        tutorialRef.GetComponent<TutorialManager>().SetTutorialStage(tutorialStage);
    }

    private int CountValidSpells()
    {
        int output = 0;
        for(int ii = 0; ii < currentLoadOut.Count; ii++)
        {
            output += currentLoadOut[ii].GetType() != typeof(LockedAbility) ? 1 : 0;
        }
        return output;
    }
}
