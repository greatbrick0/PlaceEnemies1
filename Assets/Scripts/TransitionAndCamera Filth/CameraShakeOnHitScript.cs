using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]

public class CameraShakeOnHitScript : MonoBehaviour
{
    public static CameraShakeOnHitScript instance;

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noisePerlin;

    public bool shaking;
    public float shakeTime;
    public float shakeRemaining;
    public float AmpValue;
    
    

    private void Awake()
    {
        instance = this;
        vcam = GetComponent<CinemachineVirtualCamera>();
        noisePerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        Debug.Log("Start Shake");
        shaking = true;
        noisePerlin.m_AmplitudeGain += AmpValue;
        noisePerlin.m_FrequencyGain += AmpValue;
        shakeRemaining = shakeTime;
    }
    public void DeShake()
    {
        shaking = false;
        noisePerlin.m_AmplitudeGain = 0;
        noisePerlin.m_FrequencyGain = 0;
    }

    private void Update()
    {
        if (shakeRemaining > 0)
        {
            shakeRemaining -= Time.deltaTime;
        }
        if (shaking && shakeRemaining <= 0)
            DeShake();
            

    }
}

