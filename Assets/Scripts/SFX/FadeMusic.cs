using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    AudioSource source;
    bool fade;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
            Fade();

        if (source.loop == false)
            Invoke("Fade", source.clip.length - 1f);
    }

    public void Fade()
    {
        fade = true;
        float startAudio = 0.5f;
        source.volume -= startAudio * Time.deltaTime / 0.5f;
        
    }
}
