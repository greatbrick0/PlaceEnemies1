using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescBoxScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text spellNameLabel;
    [SerializeField]
    private TMP_Text spellDescLabel;

    public void Start()
    {
        spellNameLabel.text = "";
        spellDescLabel.text = "";
    }

    public void DisplaySpellInfo(string newName, string newDesc)
    {
        spellNameLabel.text = newName;
        spellDescLabel.text = newDesc;
    }
}
