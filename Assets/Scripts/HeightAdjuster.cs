using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField]
    public Vector3 offset;
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
        Physics.Raycast(castPoint.position, -castPoint.up, 2.0f, 1 << 12);
    }
}
