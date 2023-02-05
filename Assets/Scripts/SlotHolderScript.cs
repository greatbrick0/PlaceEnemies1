using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHolderScript : MonoBehaviour
{
    [SerializeField]
    private PlacingManager managerRef;

    public void DraggingPanel(Vector2 panelEdge)
    {
        managerRef.DraggingPanel(panelEdge);
    }
}
