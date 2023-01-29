using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanelScript : MonoBehaviour
{
    [SerializeField]
    public GameObject linkedObject;
    [SerializeField]
    private int maxUses = 2;
    private int remainingUses = 2;

    [SerializeField]
    private string objectDescription = "";
}
