using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTimeLIne : MonoBehaviour
{

    public void OnEnable()
    {
        StartCoroutine(TimeLineFade());
    }

    public void FadeToPlacing()
    {
        StartCoroutine(TimeLineLeave());
    }

    public IEnumerator TimeLineLeave()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        FadeOut();
        yield return new WaitForSeconds(2);
        SetFaderInactive();
    }
    public IEnumerator TimeLineFade()
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
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
