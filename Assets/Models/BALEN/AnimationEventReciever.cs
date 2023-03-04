using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{

    public AudioClip powerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



   

    public void OnAnimationEvent()
    {
        AudioSource.PlayClipAtPoint(powerUp, transform.position);
    }
}
