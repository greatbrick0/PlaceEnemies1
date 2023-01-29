using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject heldObject;
    [SerializeField]
    private bool isHolding = false;

    public virtual void ReleaseObject()
    {
        if (isHolding)
        {
            if (heldObject.GetComponent<CombatBody>())
            {
                heldObject.GetComponent<CombatBody>().Release();
            }
        }
    }

    public bool AttachObject(GameObject newObject)
    {
        if(!isHolding)
        {
            heldObject = newObject;
            newObject.transform.position = transform.position;
            newObject.transform.eulerAngles = new Vector3(0, 180, 0);
            AffectObject();
            isHolding = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void AffectObject()
    {

    }
}
