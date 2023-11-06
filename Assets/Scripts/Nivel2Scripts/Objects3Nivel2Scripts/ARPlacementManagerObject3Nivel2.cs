using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManagerObject3Nivel2 : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public Camera aRCamera;
    public GameObject box1GameObject;
    public GameObject box2GameObject;
    public GameObject box3GameObject;
    public Vector3 boxOffset1;
    public Vector3 boxOffset2;
    public Vector3 boxOffset3;
    public GameObject destiny1GameObject;
    public GameObject destiny2GameObject;
    public GameObject destiny3GameObject;
    public Vector3 destination1Offset;
    public Vector3 destination2Offset;
    public Vector3 destination3Offset;
    private GameManagerPhase4Nivel2 gameManager;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        gameManager = FindObjectOfType<GameManagerPhase4Nivel2>();

        DestinationBoxPhase4ScriptNivel2[] DestinationBoxPhase1ScriptNivel2s = FindObjectsOfType<DestinationBoxPhase4ScriptNivel2>();
        foreach (DestinationBoxPhase4ScriptNivel2 DestinationBoxPhase1ScriptNivel2 in DestinationBoxPhase1ScriptNivel2s)
        {
            gameManager.destinationBoxes.Add(DestinationBoxPhase1ScriptNivel2);
        }
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
            destiny1GameObject.transform.position = positionToBePlaced + destination1Offset;
            destiny2GameObject.transform.position = positionToBePlaced + destination2Offset;
            destiny3GameObject.transform.position = positionToBePlaced + destination3Offset;
        }
    }
}
