using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    SlotHolderScript slotHolderRef;
    [SerializeField]
    public PlacingManager managerRef;
    [SerializeField]
    public GameObject linkedObject;
    [SerializeField]
    private int enemyAmount = 1;
    [SerializeField]
    private int maxUses = 2;
    private int remainingUses;

    [SerializeField]
    private Vector2 hoverSize = Vector2.one * 1.1f;
    [SerializeField]
    private Vector2 hoverOffset = Vector2.up * 20;
    [SerializeField]
    private Vector2 draggedOffset = Vector2.down * 180;
    [SerializeField]
    private Vector2 hideOffset = Vector2.down * 500;
    private Vector2 defaultPos = Vector2.zero;

    private float timeHovered = 0.0f;
    private bool currentlyHovered = false;
    private float timeDragging = 0.0f;
    private bool currentlyDragging = false;
    private bool currentlyClicked = false;
    private bool currentlyHiding = false;
    private bool validToUse = true;
    [HideInInspector]
    public int panelTypeIndex = 0;

    private void Start()
    {
        slotHolderRef = transform.parent.parent.GetComponent<SlotHolderScript>();
        remainingUses = maxUses;
        defaultPos = transform.localPosition;
        hoverOffset = hoverOffset + defaultPos;
        draggedOffset = draggedOffset + defaultPos;
        hideOffset = hideOffset + defaultPos;
    }

    private void Update()
    {
        if (currentlyHovered)
        {
            timeHovered += 1.0f * Time.deltaTime;
            transform.localPosition = Vector2.Lerp(defaultPos, hoverOffset, Mathf.Min(Mathf.Sqrt(timeHovered * 8), 1));
            transform.localScale = Vector2.Lerp(Vector2.one, hoverSize, Mathf.Min(Mathf.Sqrt(timeHovered * 8), 1));
        }
        else if (currentlyDragging)
        {
            timeDragging += 1.0f * Time.deltaTime;
            transform.localPosition = Vector2.Lerp(hoverOffset, draggedOffset, Mathf.Min(Mathf.Sqrt(timeDragging * 8), 1));
            transform.localScale = Vector2.Lerp(hoverSize, Vector2.one, Mathf.Min(Mathf.Sqrt(timeDragging * 8), 1));
            slotHolderRef.DraggingPanel(transform.GetChild(0).position, timeDragging);
        }
        else
        {
            if (!currentlyHiding)
            {
                transform.localPosition = defaultPos;
                transform.localScale = Vector2.one;
            }
            currentlyHiding = false;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentlyHovered = validToUse;
        currentlyDragging = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentlyHovered = false;
        timeHovered = 0.0f;
        currentlyDragging = currentlyClicked;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentlyClicked = validToUse;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlaceObjectAttempt();
        currentlyClicked = false;
        currentlyDragging = false;
        timeDragging = 0.0f;
    }

    private void PlaceObjectAttempt()
    {
        if (!currentlyDragging)
        {
            return;
        }

        if (slotHolderRef.ReleaseDrag(linkedObject, enemyAmount))
        {
            remainingUses--;
            if (remainingUses <= 0)
            {
                MakePanelInvalid();
            }
        }
    }

    public void SlideOffScreen(float timeSinceStart, int index)
    {
        if (!currentlyHovered && !currentlyDragging)
        {
            currentlyHiding = true;
            transform.localPosition = Vector2.Lerp(defaultPos, hideOffset, Mathf.Min(Mathf.Sqrt(timeSinceStart * 4), 1));
        }
    }

    private void MakePanelInvalid()
    {
        validToUse = false;
        transform.parent.GetComponent<EnemyPanelSlotScript>().ReplacePanel(panelTypeIndex);

        Destroy(this.gameObject);
    }
}
