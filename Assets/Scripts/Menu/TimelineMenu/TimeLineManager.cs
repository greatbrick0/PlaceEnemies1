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
            }
            else
            {
                arrowRef.transform.localPosition = new Vector3((nightNum - 2) * distanceBetweenNights, 200, 0);
            }
        }

        completedAnimation = true;
    }

    public void LoadCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

   
}
