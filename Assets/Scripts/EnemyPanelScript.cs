using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    public PlacingManager managerRef;
    [SerializeField]
    public GameObject linkedObject;
    [SerializeField]
    private int maxUses = 2;
    private int remainingUses = 2;

    [SerializeField]
    private string objectDescription = "";
    [SerializeField]
    private Vector2 hoverOffset = Vector2.right * 30;
    [SerializeField]
    public Vector2 verticalOffset = Vector2.zero;

    private float timeHovered = 0.0f;
    private bool currentlyHovered = false;
    private bool currentlyDragging = false;

    private void Update()
    {
        if (currentlyHovered)
        {
            timeHovered += 1.0f * Time.deltaTime;
        }
        transform.localPosition = verticalOffset + Vector2.Lerp(Vector2.zero, hoverOffset, Mathf.Min(Mathf.Sqrt(timeHovered * 8), 1));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentlyHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentlyHovered = false;
        timeHovered = 0.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");
    }
}
