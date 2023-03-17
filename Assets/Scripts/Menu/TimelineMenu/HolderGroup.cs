using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HolderGroup : MonoBehaviour
{
    [SerializeField]
    private EditSpellsManager managerRef;
    [SerializeField]
    private DescBoxScript descBoxRef;
    [SerializeField]
    private string upgradeColour;
    [SerializeField]
    GameObject upgradeButton;
    [SerializeField]
    private List<int> upgradePrices;
    [SerializeField]
    private AudioClip successSound;

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

    public void SendSlotInitialization(SpellHolder spellHolderRef, int holderIndex)
    {
        managerRef.InitializeSlot(spellHolderRef, holderIndex);
    }

    private void Start()
    {
        upgradeButton.GetComponent<UpgradeButton>().upgradeCost = upgradePrices[SessionDataManager.upgrades[upgradeColour]];
    }

    public void AttemptUpgrade()
    {
        if(SessionDataManager.currency >= upgradePrices[SessionDataManager.upgrades[upgradeColour]])
        {
            SessionDataManager.currency -= upgradePrices[SessionDataManager.upgrades[upgradeColour]];
            SessionDataManager.upgrades[upgradeColour] += 1;
            for(int ii = 0; ii < transform.childCount; ii++)
            {
                transform.GetChild(ii).GetComponent<SpellHolder>().abilityRef.upgradeLevel += 1;
            }

            managerRef.UpdateCurrencyLabel();
            if (upgradePrices[SessionDataManager.upgrades[upgradeColour]] >= 0)
            {
                upgradeButton.GetComponent<UpgradeButton>().upgradeCost = upgradePrices[SessionDataManager.upgrades[upgradeColour]];
                upgradeButton.GetComponent<UpgradeButton>().buttonText.text = "Success";
            }
            else
            {
                Destroy(upgradeButton);
            }
            AudioReference.AudioAtCamera(successSound);
        }
    }
}
