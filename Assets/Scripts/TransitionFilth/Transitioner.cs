using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner: MonoBehaviour
{

    [SerializeField]
    AudioClip transitionOut;
    [SerializeField]
    AudioClip sceneExit;

    [SerializeField]
    string sceneChangeTarget;
    public void OnEnable()
    {
        StartCoroutine(TimeLineEnter());
    }

    public void FadeOutCall(bool toPlacing)
    {
        StartCoroutine(TimeLineLeave(toPlacing));
    }

    public IEnumerator TimeLineLeave(bool sceneChange)
    {
        if (sceneChange)
        {
            AudioSource.PlayClipAtPoint(transitionOut, AudioReference.AudioPosition);
        }
        else
        {
            AudioSource.PlayClipAtPoint(sceneExit, AudioReference.AudioPosition);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        FadeOut();
        yield return new WaitForSeconds(2);
        if (sceneChange)
            SceneManager.LoadScene(sceneChangeTarget);
        else
        SetFaderInactive();
      
    }
    public IEnumerator TimeLineEnter()
    {
        
        transform.GetChild(0).gameObject.SetActive(true);
        FadeIn();
        yield return new WaitForSeconds(2);
        SetFaderInactive();
    }


    public void FadeIn()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("FadeIn", true);
    }

    public void FadeOut()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("FadeOut", true);
    }

    public void SetFaderInactive()
    {
        Debug.Log("SetFalse");
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
