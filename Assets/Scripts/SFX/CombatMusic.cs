using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusic : MonoBehaviour
{
    PlacingManager manager;
    HealthVisualManager vManager;
    PlayerScript script;
    public AudioClip[] tracks;
    public AudioSource [] source;
    int _health;
    bool combatStarted;
    static bool lowHealth;
    void Start()
    {
        manager = GameObject.Find("ArenaSceneManager").GetComponent<PlacingManager>();

        //StartCoroutine(ChangeAudio());
        source[0].clip = tracks[0];
        source[0].Play();
        source[0].volume = 0.5f;
        source[0].loop = true;
        for(int i = 1; i <= 4; i++)
        {
            source[i].clip = tracks[i];
            source[i].Play();
            source[i].volume = 0f;
            source[i].loop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (manager.combatStart && !manager.combatEnded)
        {
            GetScripts();
            _health = vManager.health;

            if (!lowHealth)
                StartCombatMusic();

            if (lowHealth)
                LastTransition();

            if (_health == 2)
            {
                LastTransition();
            }

            if(script._hurt && combatStarted)
            {
                FirstTransition();
            }
            //Invoke("FirstTransition", source[1].clip.length);
        }
        if(manager.combatEnded)
        {
            FadeMusic();
        }
    }
    void StartCombatMusic()
    {
        combatStarted = true;
        float startAudio = 0.5f; 
        source[0].volume -= startAudio * Time.deltaTime / 0.5f;

        if (combatStarted)
        {
            if (source[1].volume >= 0.5f)
            {
                source[1].volume = 0.5f;
            }
            else
            {
                source[1].volume += Time.deltaTime * 0.5f;
            }
        }
        
    }

    void FirstTransition()
    {
        //yield return new WaitForSeconds(16.941f);
        combatStarted = false;
        float startAudio = 0.5f;
        source[1].volume -= startAudio * Time.deltaTime / 0.5f;

        if (source[2].volume >= 0.5f)
        {
            source[2].volume = 0.5f;
        }
        else
        {
            source[2].volume += Time.deltaTime * 0.5f;
        }
    }
    void LastTransition()
    {
        lowHealth = true;
        float startAudio = 0.5f;
        for (int i = 0; i < 2; i++)
        {
            source[i].volume -= startAudio * Time.deltaTime / 0.5f;
            source[i].loop = false;
        }
        source[2].loop = false;

        if (source[3].volume >= 0.5f)
        {
            source[3].volume = 0.5f;
        }
        else
        {
            source[3].volume += Time.deltaTime * 0.5f;
        }
    }

    void GetScripts()
    {
        vManager = GameObject.Find("HealthBar").GetComponent<HealthVisualManager>();
        script = GameObject.Find("Player(Clone)").GetComponent<PlayerScript>();
    }

    void FadeMusic()
    {
        float startAudio = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            source[i].volume -= startAudio * Time.deltaTime / 0.5f;
        }
    }
}
