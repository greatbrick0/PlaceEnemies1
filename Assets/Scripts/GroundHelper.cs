using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundHelper : MonoBehaviour
{
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
        if(GetComponent<GroundScript>() != null)
        {
            gameObject.GetComponent<GroundScript>().ReleaseObject();
        }
    }
}

[CustomEditor(typeof(GroundHelper))]
public class GroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GroundHelper myScript = (GroundHelper)target;
        if (GUILayout.Button("Force Release"))
            myScript.ForceRelease();

        if (GUILayout.Button("Move To Zero"))
            myScript.ResetPosition();
        if (GUILayout.Button("Rotate To Zero"))
            myScript.ResetRotation();
        
    }
}
