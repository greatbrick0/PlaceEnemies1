using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CombatUIBrain))]
public class CombatUIHelper : Editor
{
    private int cycleNum = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CombatUIBrain myScript = (CombatUIBrain)target;

        GUILayout.Label("");
        if (GUILayout.Button("Change Active UI"))
        {
            myScript.CycleUI(cycleNum);
            cycleNum++;
            if (cycleNum >= myScript.transform.parent.childCount) cycleNum = 0;
            EditorUtility.SetDirty(myScript.gameObject);
        }
    }
}
