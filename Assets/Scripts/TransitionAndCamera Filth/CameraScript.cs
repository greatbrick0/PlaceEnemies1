using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    public Transform followTarget;
    [SerializeField]
    public Vector3 offset = Vector3.zero;
    [SerializeField]
    private Vector3 defualtPosition;

    void Update()
    {
        if(followTarget == null)
        {
            transform.position = defualtPosition;
            return;
        }
        transform.position = followTarget.position + offset;
    }

    public void RemoveCameraChild(int childIndex)
    {
        Destroy(transform.GetChild(childIndex).gameObject);
    }
}
