using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackDict))]
public class AttackDictEditor : Editor
{
    private int numberOfAttacks = 0;
    private string mostRecentName = "None";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AttackDict myScript = (AttackDict)target;

        //if (GUILayout.Button("Add Attack"))
        //{
        //    numberOfAttacks = myScript.AddAttack();
        //    mostRecentName = myScript.mostRecentName;
        //}

        //GUILayout.Label("Most Recent Attack: " + mostRecentName);
        //GUILayout.Label("Number Of Attacks: " + numberOfAttacks);

        //GUILayout.Label("");
        //if (GUILayout.Button("Clear All"))
        //{
        //    myScript.ClearDict();
        //    mostRecentName = myScript.mostRecentName;
        //    numberOfAttacks = 0;
        //}
    }
}
