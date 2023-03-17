using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerListenerscript : MonoBehaviour
{
    private AudioListener listener;
    public GameObject isometricCamera; // assign the isometric camera object in the inspector

    void Start()
    {
        listener = GetComponent<AudioListener>();
    }

    void Update()
    {
        if (listener != null)
        {
            listener.transform.position = isometricCamera.transform.position;
            listener.transform.rotation = isometricCamera.transform.rotation;
        }
    }
}
