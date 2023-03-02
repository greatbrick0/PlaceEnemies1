using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArenaBoundsScript))]
public class ArenaBoundsHelper : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ArenaBoundsScript myScript = (ArenaBoundsScript)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Visuals")) myScript.AddVisuals();
        if (GUILayout.Button("Remove Visuals")) myScript.RemoveVisuals();
        GUILayout.EndHorizontal();

        if (GUI.changed) EditorUtility.SetDirty(myScript.gameObject);
    }
}
