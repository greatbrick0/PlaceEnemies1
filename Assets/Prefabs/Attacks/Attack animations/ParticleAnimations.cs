using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HammerSwing (AnimationEvent animationEvent)
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.transform.parent = null;
            ps.Stop();
            Destroy(ps.gameObject, 2.0f);
        }
    }
}
