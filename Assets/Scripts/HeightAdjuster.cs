using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField]
    public Vector3 offset;
    private Vector3 adjustment = Vector3.zero;
    [SerializeField] [Tooltip("The transform that will be used as to find the object's distance to the floor. ")]
    Transform castPoint;

    Ray ray;
    RaycastHit hitData;

    private void Start()
    {
        ray = new Ray(castPoint.position, -castPoint.up);
    }

    void Update()
    {
        ray.origin = castPoint.position;
        if(Physics.Raycast(ray, out hitData, 2.0f))
        {
            adjustment = new Vector3(0, -hitData.distance, 0);
        }
        transform.localPosition = adjustment + offset;
    }
}
