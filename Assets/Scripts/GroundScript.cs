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

    #region Helper Functions
    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void ResetRotation()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void ForceRelease()
    {
        if (GetComponent<GroundScript>() != null)
        {
            gameObject.GetComponent<GroundScript>().ReleaseObject();
        }
    }

    public void MoveForward(float length)
    {
        transform.position += transform.forward * length;
    }

    public void RotateClockwise(float angle)
    {
        transform.eulerAngles += Vector3.up * angle;
    }
    #endregion
}
