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
            heldObject.GetComponent<Placeable>().Release();
        }
    }

    public bool AttachObject(GameObject newObject)
    {
        if (isHolding)
        {
            return false;
        }
        if(newObject.GetComponent<Placeable>() == null)
        {
            return false;
        }

        heldObject = newObject;
        newObject.transform.position = transform.position;
        newObject.transform.eulerAngles = new Vector3(0, 180, 0);
        AffectObject();
        isHolding = true;
        return true;
    }

    protected virtual void AffectObject()
    {

    }
}
