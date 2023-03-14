using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int stage = 0;

    public void SetTutorialStage(int newStage)
    {
        stage = newStage;
    }

    private void UpdatePrompts()
    {
        for(int ii = 0; ii < transform.childCount; ii++)
        {
            transform.GetChild(ii).gameObject.SetActive(ii == stage);
        }
    }
}
