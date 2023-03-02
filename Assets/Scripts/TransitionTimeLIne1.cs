using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTimeLIne1 : MonoBehaviour
{

    

    public void FadeToPlacing()
    {
        StartCoroutine(TimeLineLeave());
    }

    public IEnumerator TimeLineLeave()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("CombatScene");
        
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
