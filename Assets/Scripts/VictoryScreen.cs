using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{


    public void Awake()
    {
        StartCoroutine(PlacementFadeIn());
    }
    private IEnumerator PlacementFadeIn()
    {
        FadeIn();
        yield return new WaitForSeconds(2);
        SetFaderInactive();
    }
    public void FadeIn()
    {
        transform.GetChild(0).gameObject.SetActive(true);     
        transform.GetChild(0).GetComponent<Animator>().SetBool("FadeIn", true);
    }

    public void FadeOut()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Animator>().SetBool("FadeOut", true);
    }

    public void SetFaderInactive()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
