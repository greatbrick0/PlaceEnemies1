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

    [field:SerializeField]
    public Vector3 cameraResetRef { get; private set; }



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
        noisePerlin.m_AmplitudeGain = AmpValue;
        noisePerlin.m_FrequencyGain = AmpValue;
        shakeRemaining = shakeTime;
    }
    public void EndShake()
    {
        shaking = false;
        noisePerlin.m_AmplitudeGain = 0;
        noisePerlin.m_FrequencyGain = 0;
        transform.rotation = Quaternion.Euler(cameraResetRef);
    }

    private void Update()
    {
        if (shakeRemaining > 0)
        {
            shakeRemaining -= Time.deltaTime;
        }
        if (shaking && shakeRemaining <= 0)
            EndShake();
            

    }
}

