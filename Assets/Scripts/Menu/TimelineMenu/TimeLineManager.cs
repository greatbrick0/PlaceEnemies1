using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLineManager : MonoBehaviour
{
    public void LoadCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }
}
