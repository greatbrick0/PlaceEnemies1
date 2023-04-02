using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescBoxScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text spellNameLabel;
    [SerializeField]
    private TMP_Text spellDescLabel;
    [SerializeField]
    private TMP_Text spellUpgradeType;
    [SerializeField]
    private List<Sprite> starSprites;
    [SerializeField]
    private Image stars;
    [SerializeField]
    private GameObject starBackground;

    private string colour = "placeholder";
    private int visibleUpgrade = 0;

    public void Start()
    {
        spellNameLabel.text = "";
        spellDescLabel.text = "";
        spellUpgradeType.text = "";
        stars.sprite = starSprites[0];
        stars.gameObject.SetActive(false);
        starBackground.SetActive(false);
    }

    private void Update()
    {
        if (SessionDataManager.upgrades[colour] != visibleUpgrade)
        {
            stars.sprite = starSprites[SessionDataManager.upgrades[colour]];
            visibleUpgrade = SessionDataManager.upgrades[colour];
        }
    }

    public void DisplaySpellInfo(string newName, string newDesc, string newUpgradeType, string newColour)
    {
        spellNameLabel.text = newName;
        spellDescLabel.text = newDesc;
        spellUpgradeType.text = newUpgradeType;
        colour = newColour;
        stars.sprite = starSprites[SessionDataManager.upgrades[newColour]];
        visibleUpgrade = SessionDataManager.upgrades[newColour];
        stars.gameObject.SetActive(true);
        starBackground.SetActive(true);
    }
}
