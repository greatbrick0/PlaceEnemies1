using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExtraCollider : MonoBehaviour
{
    [SerializeField]
    int siblingIndex = 0;

    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            transform.parent.SendMessage("ExtraCollision" + siblingIndex.ToString() + "Enter", other.gameObject);
        }
    }
}
