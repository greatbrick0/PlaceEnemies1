using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlacingManager : MonoBehaviour
{
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

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerRef;

    [SerializeField]
    private GameObject groundHolderRef;
    [SerializeField]
    private SlotHolderScript slotHolderRef;

    private bool combatStarted = false;
    public int remainingEnemies = 0;
    public int cardsPlaced = 0;
    public int minimumCardsPlaced = 3;

    private void Start()
    {
        mouseHitObject = mousePlaneRef;

        playerRef = Instantiate(playerPrefab, transform.parent);
        playerRef.GetComponent<PlayerScript>().SetCameraRef(cam.GetComponent<Camera>());
        groundHolderRef.transform.GetChild(0).GetComponent<GroundScript>().AttachObject(playerRef);
        if(SessionDataManager.playerLoadOut.Count > 0)
        {
            playerRef.GetComponent<PlayerScript>().SetAbilities(SessionDataManager.playerLoadOut);
        }
        else
        {
            playerRef.GetComponent<PlayerScript>().SetDefaultAbilities();
        }

        cam.followTarget = groundHolderRef.transform;
        cam.offset = placingModeCamPos;
        unityCam = cam.GetComponent<Camera>();
    }

    private void Update()
    {
        currentMousePos = Mouse.current.position.ReadValue();
        mouseRay = unityCam.ScreenPointToRay(currentMousePos);
        Physics.Raycast(cam.transform.position, mouseRay.direction, out hitData, 150.0f, 1 << 9);
        mouseHitObject = hitData.collider.gameObject != null ? hitData.collider.gameObject : mousePlaneRef; //this line usually gives errors
    }

    public void StartCombat()
    {
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
        if (mouseHitObject.GetComponent<GroundScript>() != null)
        {
            mouseHitObject.GetComponent<GroundScript>().Raise();
        }
    }

    public bool ReleaseDrag(GameObject placeObject, int enemyCount)
    {
        if (mouseHitObject.GetComponent<GroundScript>() == null) return false;
        if (!mouseHitObject.GetComponent<GroundScript>().FilterObject(placeObject)) return false;

        bool output = mouseHitObject.GetComponent<GroundScript>().AttachObject(Instantiate(placeObject, transform.parent));
        cardsPlaced += output ? 1 : 0;
        remainingEnemies += output ? enemyCount : 0;
        return output;
    }

    public void DecrementEnemyCount()
    {
        remainingEnemies--;

        if (combatStarted)
        {
            if (remainingEnemies == 0)
            {
                playerRef.GetComponent<PlayerScript>().PackPlayer();
                print("Player defeated all enemies, end combat");
                SwitchToTimeLineScene();
            }
        }
    }

    public void SwitchToTimeLineScene()
    {
        SceneManager.LoadScene("TimelineScene");
    }
}
