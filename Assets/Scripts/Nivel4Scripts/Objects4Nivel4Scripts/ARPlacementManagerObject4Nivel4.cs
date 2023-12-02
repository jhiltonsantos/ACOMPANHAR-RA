using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManagerObject4Nivel4 : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public Camera aRCamera;
    public GameObject box1GameObject;
    public GameObject box2GameObject;
    public GameObject box3GameObject;
    public GameObject box4GameObject;
    public Vector3 boxOffset1;
    public Vector3 boxOffset2;
    public Vector3 boxOffset3;
    public Vector3 boxOffset4;
    public GameObject destiny1GameObject;
    public GameObject destiny2GameObject;
    public GameObject destiny3GameObject;
    public GameObject destiny4GameObject;
    public Vector3 destination1Offset;
    public Vector3 destination2Offset;
    public Vector3 destination3Offset;
    public Vector3 destination4Offset;
    public GameObject objectExample;
    public Vector3 objectExampleOffset;
    private GameManagerObjects4Nivel4 gameManager;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        gameManager = FindObjectOfType<GameManagerObjects4Nivel4>();
    }

    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = aRCamera.ScreenPointToRay(centerOfScreen);

        if (m_ARRaycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;
            box1GameObject.transform.position = positionToBePlaced + boxOffset1;
            box2GameObject.transform.position = positionToBePlaced + boxOffset2;
            box3GameObject.transform.position = positionToBePlaced + boxOffset3;
            box4GameObject.transform.position = positionToBePlaced + boxOffset4;
            destiny1GameObject.transform.position = positionToBePlaced + destination1Offset;
            destiny2GameObject.transform.position = positionToBePlaced + destination2Offset;
            destiny3GameObject.transform.position = positionToBePlaced + destination3Offset;
            destiny4GameObject.transform.position = positionToBePlaced + destination4Offset;
            objectExample.transform.position = positionToBePlaced + objectExampleOffset;
        }
    }
}
