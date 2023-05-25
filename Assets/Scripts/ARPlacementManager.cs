using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycestManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public Camera aRCamera;
    public GameObject boxPuzzleGameObject;
    public GameObject boxDestinyGameObject;
    public Vector3 destinationOffset = new Vector3(0f, 0f, 1f);
    public float minPlaneDistance = 0.5f;

    private void Awake()
    {
        m_ARRaycestManager = GetComponent<ARRaycastManager>();
    }
    
    void Start()
    {

    }

    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = aRCamera.ScreenPointToRay(centerOfScreen);

        if (m_ARRaycestManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;
            boxPuzzleGameObject.transform.position = positionToBePlaced;
            boxDestinyGameObject.transform.position = positionToBePlaced + destinationOffset;
        }
    }
}
