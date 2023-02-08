using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacingManager : MonoBehaviour
{
    [SerializeField]
    private CameraScript cam;
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

    private void Start()
    {
        mouseHitObject = mousePlaneRef;

        playerRef = Instantiate(playerPrefab, transform.parent);
        playerRef.GetComponent<PlayerScript>().SetCameraRef(cam.GetComponent<Camera>());
        groundHolderRef.transform.GetChild(0).GetComponent<GroundScript>().AttachObject(playerRef);

        cam.followTarget = groundHolderRef.transform;
        cam.offset = placingModeCamPos;
        unityCam = cam.GetComponent<Camera>();
    }

    private void Update()
    {
        currentMousePos = Mouse.current.position.ReadValue();
        mouseRay = unityCam.ScreenPointToRay(currentMousePos);
        Physics.Raycast(cam.transform.position, mouseRay.direction, out hitData, 150.0f, 1 << 9);
        mouseHitObject = hitData.collider.gameObject;
    }

    public void StartCombat()
    {
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

    public bool ReleaseDrag(GameObject placeObject)
    {
        if (mouseHitObject.GetComponent<GroundScript>() == null)
        {
            return false;
        }

        if (!mouseHitObject.GetComponent<GroundScript>().FilterObject(placeObject))
        {
            return false;
        }

        return mouseHitObject.GetComponent<GroundScript>().AttachObject(Instantiate(placeObject, transform.parent));
    }
}
