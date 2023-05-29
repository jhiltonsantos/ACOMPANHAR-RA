using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManagerPhase2 : MonoBehaviour
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
    private GameManagerPhase2 gameManager;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        gameManager = FindObjectOfType<GameManagerPhase2>();

        // Encontra todas as instâncias dos objetos destino e adiciona à lista
        DestinationBoxPhase2Script[] destinationBoxScripts = FindObjectsOfType<DestinationBoxPhase2Script>();
        foreach (DestinationBoxPhase2Script destinationBoxScript in destinationBoxScripts)
        {
            gameManager.destinationBoxes.Add(destinationBoxScript);
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
