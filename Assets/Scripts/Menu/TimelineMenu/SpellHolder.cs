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
    public Ability abilityRef { get; private set; }

    [SerializeField]
    private Vector2 originalPos = Vector2.zero;
    public Vector2 equippedPos = Vector2.zero;

    [SerializeField]
    private string abilityName = "";
    [SerializeField]
    [TextArea]
    private string abilityDescription = "";
    [SerializeField]
    public string spellTypeGroup { get; private set; } = "rgb";

    private HolderGroup holderGroupRef;
    private Vector2 mousePos = Vector2.zero;
    private bool currentlyDragged = false;
    public bool equipped = false;
    public EquipSlotScript equippedSlotRef;

    private void Start()
    {
        holderGroupRef = transform.parent.GetComponent<HolderGroup>();
        originalPos = transform.position;

        #region Initialize Ability Reference
        abilityRef = EveryAbilityDict.abilityDict[abilityRefIndex];
        abilityRef.SetDisplayVars();
        abilityName = abilityRef.displayName;
        abilityDescription = abilityRef.description;
        #endregion

        StartCoroutine(CheckSavedLoadout());
    }

    IEnumerator CheckSavedLoadout()
    {
        yield return new WaitForEndOfFrame();
        if (SessionDataManager.playerLoadOut.Count > 0)
        {
            for (int ii = 0; ii < SessionDataManager.playerLoadOut.Count; ii++)
            {
                if (abilityRef.GetType() == SessionDataManager.playerLoadOut[ii].GetType())
                {
                    holderGroupRef.SendSlotInitialization(this, ii);
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (currentlyDragged)
        {
            mousePos = Mouse.current.position.ReadValue();
            transform.position = mousePos;
            holderGroupRef.SendDragInfo(mousePos);
        }
        else
        {
            transform.position = equipped ? equippedPos : originalPos;
        }
    }

    public void BeginDrag()
    {
        if (!equipped)
        {
            currentlyDragged = true;
            transform.SetAsLastSibling();
            transform.parent.SetAsLastSibling();
        }
    }

    public void EndDrag()
    {
        currentlyDragged = false;
        mousePos = Mouse.current.position.ReadValue();
        holderGroupRef.SendEndDrag(mousePos, this);
    }

    public void SendDescription()
    {
        holderGroupRef.DisplayDescription(abilityDescription, abilityName);
    }

    public void Enlarge()
    {
        transform.localScale = transform.localScale * 1.1f;
    }

    public void Shrink()
    {
        transform.localScale = transform.localScale / 1.1f;
    }
}
