using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlacingManager : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField]
    private CameraScript cam;
    [SerializeField]
    private Camera unityCam;
    private RaycastHit hitData;
    private Ray mouseRay;
    private GameObject mouseHitObject;
    private Vector2 currentMousePos;
    [SerializeField]
    private Vector3 placingModeCamPos = Vector3.up * 30 + Vector3.back * 30;
    [SerializeField]
    private Vector3 combatModeCamPos;
    [SerializeField]
    private GameObject mousePlaneRef;
    [SerializeField]
    private Vector3 mousePlaneUpperPos = Vector3.up;

    [Header("Placing UI References")]
    [SerializeField]
    private GameObject cardsPlacedLabel;
    [SerializeField]
    private GameObject readyButtonRef;

    [Header("Player References")]
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerRef;
    private PlayerScript playerScriptRef;

    [Header("Other References")]
    [SerializeField]
    private GameObject groundHolderRef;
    [SerializeField]
    private SlotHolderScript slotHolderRef;
    [SerializeField]
    private GameObject VicFadeRef;

    [Space]
    [SerializeField]
    private bool combatStarted = false;
    [field: SerializeField]
    public int remainingEnemies { get; private set; } = 0;
    [field: SerializeField]
    public int cardsPlaced { get; private set; } = 0;
    [field: SerializeField]
    public int minimumCardsPlaced { get; private set; } = 3;

    private void Start()
    {
        mouseHitObject = mousePlaneRef;

        #region Initializing the Player
        playerRef = Instantiate(playerPrefab, transform.parent);
        playerScriptRef = playerRef.GetComponent<PlayerScript>();
        playerScriptRef.SetCameraRef(cam.GetComponent<Camera>());
        groundHolderRef.transform.GetChild(0).GetComponent<GroundScript>().AttachObject(playerRef);
        if(SessionDataManager.playerLoadOut.Count > 0)
        {
            playerScriptRef.SetAbilities(SessionDataManager.playerLoadOut);
        }
        else
        {
            playerScriptRef.SetDefaultAbilities();
        }
        if(SessionDataManager.savedPlayerHealth != 0) playerScriptRef.SetHealth(SessionDataManager.savedPlayerHealth);
        #endregion

        cam.followTarget = groundHolderRef.transform;
        cam.offset = placingModeCamPos;
        unityCam = cam.GetComponent<Camera>();

        UpdatePlacingUI();
    }

    private void Update()
    {
        currentMousePos = Mouse.current.position.ReadValue() != null ? Mouse.current.position.ReadValue() : Vector2.zero;
        mouseRay = unityCam.ScreenPointToRay(currentMousePos);
        Physics.Raycast(cam.transform.position, mouseRay.direction, out hitData, 150.0f, 1 << 9);
        mouseHitObject = hitData.collider.gameObject != null ? hitData.collider.gameObject : mousePlaneRef; //this line usually gives errors
    }

    public void StartCombat()
    {
        if (cardsPlaced < minimumCardsPlaced) return;

        combatStarted = true;
        cam.followTarget = playerRef.transform;
        cam.offset = combatModeCamPos;
        mousePlaneRef.transform.position = mousePlaneUpperPos;
        ReleaseAllTiles();
    }

    private void ReleaseAllTiles()
    {
        for(int ii = 0; ii < groundHolderRef.transform.childCount; ii++)
        {
            groundHolderRef.transform.GetChild(ii).GetComponent<GroundScript>().ReleaseObject();
        }
    }

    public void DraggingPanel(Vector2 panelEdge)
    {
        if (mouseHitObject.GetComponent<GroundScript>() == null) return; 

        mouseHitObject.GetComponent<GroundScript>().Raise();
    }

    public bool ReleaseDrag(GameObject placeObject, int enemyCount)
    {
        if (mouseHitObject.GetComponent<GroundScript>() == null) return false;
        if (!mouseHitObject.GetComponent<GroundScript>().FilterObject(placeObject)) return false;

        bool output = mouseHitObject.GetComponent<GroundScript>().AttachObject(Instantiate(placeObject, transform.parent));
        cardsPlaced += output ? 1 : 0;
        remainingEnemies += output ? enemyCount : 0;
        UpdatePlacingUI();

        return output;
    }

    public void DecrementEnemyCount()
    {
        remainingEnemies--;
        CheckForVictory();
    }

    public void SwitchToTimeLineScene()
    {
        SceneManager.LoadScene("TimelineScene");
    }

    public void PlayerHasDied()
    {
        StartCoroutine(DefeatFadeOut());
    }

    private void CheckForVictory()
    {
        if (combatStarted)
        {
            if (remainingEnemies <= 0)
            {
                print("Player defeated all enemies, end combat");
                playerRef.GetComponent<PlayerScript>().PackPlayer();
                SessionDataManager.nightNum += 1;

                StartCoroutine(VictoryFadeOut());
            }
        }
    }

    private IEnumerator VictoryFadeOut()
    {
        VicFadeRef.SetActive(true);
        VicFadeRef.GetComponent<Transitioner>().FadeOutCall(true);
        yield return new WaitForSeconds(2.0f);

        SwitchToTimeLineScene();
    }

    private IEnumerator DefeatFadeOut()
    {
        yield return new WaitForSeconds(1.0f);
        VicFadeRef.GetComponent<Transitioner>().FadeOutCall(true);
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("LoseScene");
    }

    private void UpdatePlacingUI()
    {
        cardsPlacedLabel.GetComponent<TextMeshProUGUI>().text = cardsPlaced.ToString() + "/" + minimumCardsPlaced.ToString();
        readyButtonRef.SetActive(cardsPlaced >= minimumCardsPlaced);
    }
}
