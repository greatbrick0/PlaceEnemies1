using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTest : MonoBehaviour
{
    AudioSource aud;

    public AudioClip _hammer;
    public AudioClip _spill;
    public AudioClip _bottle;
    public AudioClip _magicAttack;
    public AudioClip _ghostHands;
    public AudioClip _chain;
    public AudioClip _storm;
    public AudioClip _blade;
    public AudioClip _gravity;
    public AudioClip _jump;
    public AudioClip _bomb;
    public AudioClip _boulder;
    public AudioClip _charge;

    bool magicAttack;
    HammerSFXEvent hammer;
    void Start()
    {
        aud = GameObject.Find("SFXAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hammer()
    {
        AudioSource.PlayClipAtPoint(_hammer, Vector3.zero);
    }

    public void Spill()
    {
        AudioSource.PlayClipAtPoint(_spill, Vector3.zero, 1f);
    }
    public void Bottle()
    {
        AudioSource.PlayClipAtPoint(_bottle, Vector3.zero, 1f);
    }
    public void MagicDamage()
    {
        AudioSource.PlayClipAtPoint(_magicAttack, Vector3.zero, 1f);
    }
    public void Bomb()
    {
        AudioSource.PlayClipAtPoint(_bomb, Vector3.zero, 1f);
    }
    
    public void GhostHands()
    {
        AudioSource.PlayClipAtPoint(_ghostHands, Vector3.zero, 1f);
        /*
        aud.clip = _ghostHands;
        if (GameObject.Find("HomingMissile(Clone)") != null)
        {
            aud.Play();
        }
        else
        {
            aud.Stop();
        }
        */
    }
    public void Chain()
    {
        AudioSource.PlayClipAtPoint(_chain, Vector3.zero, 1f);
    }
    public void LightingStorm()
    {
        AudioSource.PlayClipAtPoint(_storm, Vector3.zero, 0.7f);
    }
    public void Blade()
    {
        AudioSource.PlayClipAtPoint(_blade, Vector3.zero, 1f);
    }
    public void Gravity()
    {
        AudioSource.PlayClipAtPoint(_gravity, Vector3.zero, 0.3f);
    }
    public void Jump()
    {
        AudioSource.PlayClipAtPoint(_jump, Vector3.zero, 1f);
    }
    public void Boulder()
    {
        AudioSource.PlayClipAtPoint(_boulder, Vector3.zero, 1f);
    }
    public void Charge()
    {
        AudioSource.PlayClipAtPoint(_charge, Vector3.zero, 1f);
    }
}
