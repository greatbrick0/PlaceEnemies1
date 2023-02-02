using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingManager : MonoBehaviour
{
    [SerializeField]
    private CameraScript cam;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerRef;

    [SerializeField]
    private GameObject groundHolderRef;

    private void Start()
    {
        playerRef = Instantiate(playerPrefab, transform.parent);
        playerRef.GetComponent<PlayerScript>().SetCameraRef(cam.GetComponent<Camera>());
        groundHolderRef.transform.GetChild(0).GetComponent<GroundScript>().AttachObject(playerRef);

        cam.followTarget = groundHolderRef.transform;
        cam.offset = new Vector3(-5, 45, -48);
    }

    public void StartCombat()
    {
        cam.followTarget = playerRef.transform;
        cam.offset = new Vector3(0, 25, -25);
        ReleaseAllTiles();
    }

    private void ReleaseAllTiles()
    {
        for(int ii = 0; ii < groundHolderRef.transform.childCount; ii++)
        {
            groundHolderRef.transform.GetChild(ii).GetComponent<GroundScript>().ReleaseObject();
        }
    }
}
