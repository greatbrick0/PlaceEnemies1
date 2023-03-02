using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBoundsScript : MonoBehaviour
{
    [SerializeField]
    private MeshFilter meshFilterRef;
    [SerializeField]
    private MeshRenderer meshRendererRef;

    public void RemoveVisuals()
    {
        Transform currentChild;

        for (int ii = 0; ii < transform.childCount; ii++)
        {
            currentChild = transform.GetChild(ii);
            DestroyImmediate(currentChild.GetComponent<MeshFilter>());
            DestroyImmediate(currentChild.GetComponent<MeshRenderer>());
        }
    }

    public void AddVisuals()
    {
        Transform currentChild;

        for (int ii = 0; ii < transform.childCount; ii++)
        {
            currentChild = transform.GetChild(ii);

            if(currentChild.GetComponent<MeshFilter>() == null)
            {
                currentChild.gameObject.AddComponent<MeshFilter>();
                currentChild.gameObject.AddComponent<MeshRenderer>();

                currentChild.GetComponent<MeshFilter>().mesh = meshFilterRef.sharedMesh;
                currentChild.GetComponent<MeshRenderer>().materials = meshRendererRef.sharedMaterials;
            }
        }
    }

    private void Start()
    {
        AddVisuals();
    }
}
