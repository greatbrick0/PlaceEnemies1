using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI buttonText;
    
    private void Awake()
    {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize = 75;
        buttonText.enableVertexGradient = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize = 60;
        buttonText.enableVertexGradient = true;
    }
}
