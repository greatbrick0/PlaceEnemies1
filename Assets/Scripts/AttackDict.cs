using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDict : MonoBehaviour
{
    public static Dictionary<string, GameObject> attacks = new Dictionary<string, GameObject>();

    [SerializeField]
    private GameObject attackToAdd;
    [SerializeField]
    private string attackName;

    public string mostRecentName { get; private set; } = "None";

    public int AddAttack()
    {
        attacks.Add(attackName, attackToAdd);
        mostRecentName = attackName;
        return attacks.Count;
    }

    public void ClearDict()
    {
        attacks.Clear();
        mostRecentName = "None";
    }
}
