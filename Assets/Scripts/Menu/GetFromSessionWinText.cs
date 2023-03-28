using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetFromSessionWinText: MonoBehaviour
{
    public bool currency;
    private void Awake()
    {
        if (currency)
            GetComponent<TextMeshProUGUI>().text = (SessionDataManager.currency * 13).ToString();
        else
            GetComponent<TextMeshProUGUI>().text = (SessionDataManager.savedPlayerHealth).ToString();
    }
}
