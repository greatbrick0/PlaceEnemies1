using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReference : MonoBehaviour
{
    public string sceneString;
    public static Vector3 AudioPosition;

    public static AudioClip spellSelectSound;

    public static AudioClip spellReleaseSound;

    [SerializeField]
    public AudioClip spellSelectHold;

    [SerializeField]
    public AudioClip spellReleaseHold;

    private void Awake()
    {
        spellSelectSound = spellSelectHold;
        spellReleaseSound = spellReleaseHold;
    }
    private void Start()
    {
        if (sceneString == "CombatScene")
        {
            Debug.Log("GetAudioPosition of Camera");
            AudioPosition = Camera.main.transform.position;
        }
        else //could change if needed camera position in other scenes... what other scenes? idk.
            AudioPosition = Vector3.zero;
    }
} //is there a solution better than Camera.main? -Spencer
