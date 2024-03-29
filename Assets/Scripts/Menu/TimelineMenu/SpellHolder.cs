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
    private string abilityUpgradeType = "";
    [SerializeField]
    public string spellTypeGroup { get; private set; } = "rgb";

    private HolderGroup holderGroupRef;
    private Vector2 mousePos = Vector2.zero;
    private bool currentlyDragged = false;
    private bool currentlyHovered = false;
    private float timeHovered = 0.0f;
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
        abilityUpgradeType = abilityRef.upgradeType;
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

        if (currentlyHovered)
        {
            timeHovered += 1.0f * Time.deltaTime;
            transform.localScale = Vector2.one * Mathf.Min(0.6f * Mathf.Sqrt(timeHovered) + 1.1f, 1.25f);
        }

    }

    public void BeginDrag()
    {
        Shrink();
        if (!equipped)
        {
            currentlyDragged = true;
            transform.SetAsLastSibling();
            transform.parent.SetAsLastSibling();
            //AudioSource.PlayClipAtPoint(AudioReference.spellSelectSound, Vector3.zero);
        }
    }

    public void EndDrag()
    {
        currentlyDragged = false;
        mousePos = Mouse.current.position.ReadValue();
        holderGroupRef.SendEndDrag(mousePos, this);
        //AudioSource.PlayClipAtPoint(AudioReference.spellReleaseSound, Vector3.zero);
    }

    public void SendDescription()
    {
        holderGroupRef.DisplayDescription(abilityDescription, abilityName, abilityUpgradeType);
    }

    public void Enlarge()
    {
        if (!equipped) currentlyHovered = true;
    }

    public void Shrink()
    {
        currentlyHovered = false;
        timeHovered = 0.0f;
        transform.localScale = Vector2.one * 1.1f;
    }
}
