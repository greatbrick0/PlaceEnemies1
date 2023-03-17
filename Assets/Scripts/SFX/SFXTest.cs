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

    bool magicAttack;
    HammerSFXEvent hammer;
    void Start()
    {
        aud = GameObject.Find("SFX Audio Source").GetComponent<AudioSource>();
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
        AudioSource.PlayClipAtPoint(_spill, Vector3.zero, 1);
    }
    public void Bottle()
    {
        AudioSource.PlayClipAtPoint(_bottle, Vector3.zero, 1f);
    }
    public void MagicDamage()
    {
        AudioSource.PlayClipAtPoint(_magicAttack, Vector3.zero, 1f);
    }
    /*
    public void GhostHands()
    {
        aud.clip = _ghostHands;
        aud.Play();
        if (magicAttack)
        {
            aud.Stop();
            
        }
    }
    */
    public void Chain()
    {
        AudioSource.PlayClipAtPoint(_chain, Vector3.zero, 1f);
    }
    public void LightingStorm()
    {
        AudioSource.PlayClipAtPoint(_storm, Vector3.zero, 1f);
    }
    public void Blade()
    {
        AudioSource.PlayClipAtPoint(_blade, Vector3.zero, 1f);
    }
    public void Gravity()
    {
        AudioSource.PlayClipAtPoint(_gravity, Vector3.zero, 1f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(this.gameObject.name == "HomingMissile(Clone)")
        {
            if(collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                magicAttack = true;
            }
        }
    }
}
