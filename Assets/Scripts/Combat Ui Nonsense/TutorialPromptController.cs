using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPromptController : MonoBehaviour
{
    public Image dimmer;

    private void OnEnable()
    {
        dimmer.gameObject.SetActive(true);
    }
}
