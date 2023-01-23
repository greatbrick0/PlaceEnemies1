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

    public void MoveForward(float length)
    {
        transform.position += transform.forward * length;
    }

    public void RotateClockwise(float angle)
    {
        transform.eulerAngles += Vector3.up * angle;
    }
}

[CustomEditor(typeof(GroundHelper))]
public class GroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Label("Debugging");
        GroundHelper myScript = (GroundHelper)target;
        if (GUILayout.Button("Force Release"))
            myScript.ForceRelease();

        GUILayout.Label("Absolute Positioning");
        if (GUILayout.Button("Move To Zero"))
            myScript.ResetPosition();
        if (GUILayout.Button("Rotate To Zero"))
            myScript.ResetRotation();

        GUILayout.Label("Relative Positioning");
        if (GUILayout.Button("Move One Tile Distance"))
            myScript.MoveForward(4.0f);
        if (GUILayout.Button("Rotate Clockwise"))
            myScript.RotateClockwise(60);
        if (GUILayout.Button("Rotate Counter Cclockwise"))
            myScript.RotateClockwise(300);
    }
}
