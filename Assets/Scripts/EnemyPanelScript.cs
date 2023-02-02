using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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
    private Vector2 hoverSize = Vector2.one * 1.1f;
    [SerializeField]
    private Vector2 hoverOffset = Vector2.up * 20;
    [SerializeField]
    private Vector2 draggedOffset = Vector2.down * 180;
    private Vector2 defaultPos = Vector2.zero;

    private float timeHovered = 0.0f;
    [SerializeField]
    private bool currentlyHovered = false;
    private float timeDragging = 0.0f;
    [SerializeField]
    private bool currentlyDragging = false;
    [SerializeField]
    private bool currentlyClicked = false;

    private void Start()
    {
        defaultPos = transform.localPosition;
        hoverOffset = hoverOffset + defaultPos;
        draggedOffset = draggedOffset + defaultPos;
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
        }
        else
        {
            transform.localPosition = defaultPos;
            transform.localScale = Vector2.one;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentlyHovered = true;
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
        currentlyClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentlyClicked = false;
        currentlyDragging = false;
        timeDragging = 0.0f;
    }
}
