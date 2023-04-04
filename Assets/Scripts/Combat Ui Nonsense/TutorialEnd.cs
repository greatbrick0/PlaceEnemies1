using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    public PlacingManager manager;
    public GameObject dimmer;
    private Animation animationRef;

    void OnEnable()
    {
        animationRef = transform.GetChild(0).GetComponent<Animation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            manager.NextTutorialPrompt(true);
            dimmer.SetActive(false);
            animationRef.Play();
            StartCoroutine(StartAnimation());
        }
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        gameObject.SetActive(false);
    }
}
