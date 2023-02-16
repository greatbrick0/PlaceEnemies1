using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDict : MonoBehaviour
{
    [Serializable]
    public class AttackDictEntry
    {
        public string entryName = "";
        [SerializeField]
        public GameObject entryGameObject;
    }
    [SerializeField]
    public List<AttackDictEntry> attackEntries = new List<AttackDictEntry>();

    public static Dictionary<string, GameObject> attacks { get; private set; } = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        for(int ii = 0; ii < attackEntries.Count; ii++)
        {
            attacks.Add(attackEntries[ii].entryName, attackEntries[ii].entryGameObject);
        }
    }
}
