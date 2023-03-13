using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dayCountRef;

    [SerializeField]
    private Image fadeInPanelRef;
    [SerializeField]
    private float timeToFadeIn;
    private float fadingTime = 0.0f;

    void Start()
    {
        dayCountRef.text = "Day " + SessionDataManager.nightNum + " of 5 reached";
    }

    private void Update()
    {
        if(fadeInPanelRef.color.a >= 0)
        {
            fadingTime += 1.0f * Time.deltaTime;
            fadeInPanelRef.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, fadingTime / timeToFadeIn));
            if (fadeInPanelRef.color.a <= 0) fadeInPanelRef.gameObject.SetActive(false);
        }
    }

    public void SwitchToStartScene()
    {
        SessionDataManager.ResetSession();
        SceneManager.LoadScene("MenuScene");
    }
}
