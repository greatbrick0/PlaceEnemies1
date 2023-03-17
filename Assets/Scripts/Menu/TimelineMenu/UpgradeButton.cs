using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string color;

    public TextMeshProUGUI buttonText;
    public int upgradeCost;

    private void Awake()
    {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = "COST: " + upgradeCost;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = "Upgrade " + color + " Spells";
    }
}
