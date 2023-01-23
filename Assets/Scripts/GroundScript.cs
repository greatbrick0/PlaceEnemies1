using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject heldObject;

    public virtual void ReleaseObject()
    {
        if (heldObject.GetComponent<CombatBody>())
        {
            heldObject.GetComponent<CombatBody>().Release();
        }
    }
}
