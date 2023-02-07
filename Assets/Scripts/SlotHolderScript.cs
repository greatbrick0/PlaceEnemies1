using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHolderScript : MonoBehaviour
{
    [SerializeField]
    private PlacingManager managerRef;

    public void DraggingPanel(Vector2 panelEdge, float timeSinceDragStart = 1.0f)
    {
        managerRef.DraggingPanel(panelEdge);

        for(int ii = 0; ii < transform.childCount; ii++)
        {
            transform.GetChild(ii).GetChild(0).GetComponent<EnemyPanelScript>().SlideOffScreen(timeSinceDragStart, ii);
        }
    }

    public bool ReleaseDrag(GameObject placeObject)
    {
        return managerRef.ReleaseDrag(placeObject);
    }
}
