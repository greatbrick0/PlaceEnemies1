using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBreak : MonoBehaviour
{
    PlayerScript script;
    public AudioClip _healthBreak;
    bool playing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player(Clone)") != null)
        {
            GetScript();
            if(script._hurtSFX && !playing)
            {
                HealthBreakSFX();
                Invoke("SetBoolsFalse", _healthBreak.length);
            }
        }
    }

    void HealthBreakSFX()
    {
        Debug.Log("Hitsound");
        playing = true;
        AudioSource.PlayClipAtPoint(_healthBreak, Vector3.zero, 0.7f);

    }
    void SetBoolsFalse()
    {
        playing = false;
        script._hurtSFX = false;
    }

    void GetScript()
    {
        script = GameObject.Find("Player(Clone)").GetComponent<PlayerScript>();
    }
}
