using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundScript))]
public class GroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GroundScript myScript = (GroundScript)target;

        GUILayout.Label("\nDebugging");
        if (GUILayout.Button("Force Release")) myScript.ForceRelease();
        GUILayout.Label("");

        GUILayout.Label("Absolute Positioning");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Move To Zero")) myScript.ResetPosition();
        if (GUILayout.Button("Rotate To Zero")) myScript.ResetRotation();
        GUILayout.EndHorizontal();

        GUILayout.Label("Relative Positioning");
        if (GUILayout.Button("Move One Tile Distance")) myScript.MoveForward(4.0f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotate Counter Clockwise")) myScript.RotateClockwise(300);
        if (GUILayout.Button("Rotate Clockwise")) myScript.RotateClockwise(60);
        GUILayout.EndHorizontal();
    }
}
