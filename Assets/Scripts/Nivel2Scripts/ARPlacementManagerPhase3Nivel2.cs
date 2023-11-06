using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManagerNivel2Phase3Nivel2 : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public Camera aRCamera;
    public GameObject box1GameObject;
    public GameObject box2GameObject;
    public GameObject destiny1GameObject;
    public GameObject destiny2GameObject;
    public Vector3 destination1Offset;
    public Vector3 destination2Offset;
    public Vector3 boxOffset1;
    public Vector3 boxOffset2;
    private GameManagerPhase3Nivel2 gameManager;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        gameManager = FindObjectOfType<GameManagerPhase3Nivel2>();

        DestinationBoxPhase3ScriptNivel2[] DestinationBoxPhase1ScriptNivel2s = FindObjectsOfType<DestinationBoxPhase3ScriptNivel2>();
        foreach (DestinationBoxPhase3ScriptNivel2 DestinationBoxPhase1ScriptNivel2 in DestinationBoxPhase1ScriptNivel2s)
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
            destiny1GameObject.transform.position = positionToBePlaced + destination1Offset;
            destiny2GameObject.transform.position = positionToBePlaced + destination2Offset;
        }
    }
}
