using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditSpellsManager : MonoBehaviour
{
    public List<Ability> currentLoadOut = new List<Ability>();
    public List<EquipSlotScript> loadOutSlots = new List<EquipSlotScript>();

    [Header("References")]
    [SerializeField]
    GameObject doneButtonRef;
    [SerializeField]
    GameObject clearButtonRef;
    [SerializeField]
    TextMeshProUGUI currencyLabelRef;

    [SerializeField]
    private GameObject tutorialRef;
    private int tutorialStage = 0;
    private bool showTutorial = false;

    [Header("Debugging options")]
    [SerializeField]
    bool give300Currency = false;

    private void Start()
    {
        if (give300Currency) SessionDataManager.currency += 300;

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
        clearButtonRef.SetActive(CountValidSpells() >= 1);
        UpdateCurrencyLabel();
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
        clearButtonRef.SetActive(CountValidSpells() >= 1);

        if (showTutorial && tutorialStage == 4 && CountValidSpells() == 1) NextTutorialPrompt();
        if (showTutorial && tutorialStage == 7 && CountValidSpells() == 3) NextTutorialPrompt();
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
            output += currentLoadOut[ii] == null ? 1 : 0;
        }
        return output;
    }

    public void ClearLoadout()
    {
        for(int ii = 0; ii < loadOutSlots.Count; ii++)
        {
            loadOutSlots[ii].RemoveSpell();
            currentLoadOut[ii] = new LockedAbility();
        }
        doneButtonRef.SetActive(false);
        clearButtonRef.SetActive(false);
    }

    public void UpdateCurrencyLabel()
    {
        currencyLabelRef.text = SessionDataManager.currency.ToString() + " Blood Essence";
    }
}
