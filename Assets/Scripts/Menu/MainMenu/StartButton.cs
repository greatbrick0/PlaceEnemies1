using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void SetTutorial(bool tutorial)
    {
        SessionDataManager.usingTutorial = tutorial;
    }

    public void LoadCombatScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetProgress()
    {
        SessionDataManager.ResetSession();
    }
}
