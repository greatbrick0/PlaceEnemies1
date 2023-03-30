using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField]
    private bool completedAnimation = false;
    [SerializeField]
    Transform crossesRef;
    [SerializeField]
    GameObject arrowRef;
    [SerializeField]
    private int nightNum;
    private float distanceBetweenNights = 168.75f;
    [Space]
    [SerializeField]
    private Transitioner transitionerRef;

    private void OnEnable()
    {
        nightNum = nightNum != 0 ? nightNum : SessionDataManager.nightNum;
        SessionDataManager.nightNum = nightNum;

        crossesRef.gameObject.SetActive(true);
        for(int ii = 0; ii < crossesRef.childCount; ii++)
        {
            crossesRef.GetChild(ii).gameObject.SetActive(ii < nightNum - 1);
        }
        if (!completedAnimation)
        {
            if(nightNum > 0)
            {
                arrowRef.transform.localPosition = new Vector3((nightNum - 3) * distanceBetweenNights, 200, 0);
                GetComponent<Animator>().SetTrigger("ChangeNights");
                Invoke("FadeOut", 4.05f);
                Invoke("GoToCombatScene", 5.5f);
            }
            else
            {
                arrowRef.transform.localPosition = new Vector3((nightNum - 2) * distanceBetweenNights, 200, 0);
                Invoke("FadeOut", 2.05f);
                Invoke("GoToCombatScene", 3.5f);
            }
        }

        completedAnimation = true;
    }

    public void LoadCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void FadeOut()
    {
        transitionerRef.transform.GetChild(0).gameObject.SetActive(true);
        transitionerRef.FadeOut();
    }

    private void GoToCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }
}
