using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSFXEvent : MonoBehaviour
{
    public AudioClip _hammer;
    public void HammerSound()
    {
        AudioSource.PlayClipAtPoint(_hammer, Vector3.zero);
    }
}
