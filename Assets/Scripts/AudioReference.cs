using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReference : MonoBehaviour
{
    public string sceneString;
    public static Vector3 AudioPosition;

    public static AudioClip spellSelectSound;

    public static AudioClip spellReleaseSound;

    public static AudioClip [] spellSounds;

    [SerializeField]
    public AudioClip spellSelectHold;

    [SerializeField]
    public AudioClip spellReleaseHold;

    [SerializeField]
    public AudioClip [] spellSoundHold;
    private void Awake()
    {
        spellSelectSound = spellSelectHold;
        spellReleaseSound = spellReleaseHold;
        spellSounds = spellSoundHold;
    }
    private void Start()
    {
        if (sceneString == "CombatScene")
        {
            Debug.Log("GetAudioPosition of Camera");
            AudioPosition = Camera.main.transform.position;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) AudioPosition = playerObj.transform.position;
        }
        else //could change if needed camera position in other scenes... what other scenes? idk.
            AudioPosition = Vector3.zero;
    }

    public static void AudioAtCamera(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, AudioPosition);
    }

    public static void AudioAtVector3(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}
