using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    public AudioClip buttonClick;
    public Button button;
    
    // Update is called once per frame
    void Update()
    {
        button.onClick.AddListener(ButtonSound);
    }

    void ButtonSound()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector3.zero);
    }
}
