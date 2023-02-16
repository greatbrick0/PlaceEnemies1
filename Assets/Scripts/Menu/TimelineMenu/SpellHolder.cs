using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class SpellHolder : MonoBehaviour
{
    [SerializeField]
    private int abilityRefIndex = 0;
    [SerializeField]
    private Ability abilityRef;

    [SerializeField]
    private Vector2 originalPos = Vector2.zero;
    [SerializeField]
    private string abilityName = "";
    [SerializeField]
    [TextArea]
    private string abilityDescription = "";

    private bool currentlyDragged = false;
    private bool equipped = false;

    private void Start()
    {
        originalPos = transform.localPosition;
        abilityRef = EveryAbilityDict.abilityDict[abilityRefIndex];
        abilityRef.SetDisplayVars();
        abilityName = abilityRef.displayName;
        abilityDescription = abilityRef.description;
    }

    private void Update()
    {
        if (currentlyDragged)
        {
            transform.position = Mouse.current.position.ReadValue();
        }
        else
        {
            if (!equipped)
            {
                transform.localPosition = originalPos;
            }
        }
    }

    public void BeginDrag()
    {
        currentlyDragged = true;
        transform.SetAsLastSibling();
        transform.parent.SetAsLastSibling();
    }

    public void EndDrag()
    {
        currentlyDragged = false;
    }

    public void SendDescription()
    {
        transform.parent.GetComponent<HolderGroup>().DisplayDescription(abilityDescription, abilityName);
    }
}
