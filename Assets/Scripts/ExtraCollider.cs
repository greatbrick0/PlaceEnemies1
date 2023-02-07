using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExtraCollider : MonoBehaviour
{
    [SerializeField]
    int siblingIndex = 0;
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.SendMessage("ExtraCollision"+siblingIndex.ToString()+"Enter", collision.gameObject);
    }
}
