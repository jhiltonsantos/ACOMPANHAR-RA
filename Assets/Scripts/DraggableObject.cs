using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using System.Collections.Generic;

public class DraggableObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool isBeingDragged;
    private Rigidbody rb;
    private DestinationBoxScript destinationBoxScript;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        rb = GetComponent<Rigidbody>();
        destinationBoxScript = FindObjectOfType<DestinationBoxScript>();
    }

    void Update()
    {
        if (isBeingDragged)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    MoveObject(touch);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    StopDragging();
                }
            }
        }
    }

    private void MoveObject(Touch touch)
    {
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            transform.position = hitPose.position;
        }
    }

    private void OnMouseDown()
    {
        if (!isBeingDragged)
        {
            StartDragging();
        }
    }

    private void StartDragging()
    {
        isBeingDragged = true;
        rb.isKinematic = true;
    }

    private void StopDragging()
    {
        isBeingDragged = false;
        rb.isKinematic = false;

        if (destinationBoxScript != null && !destinationBoxScript.IsObjectCorrect(gameObject))
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane));
        // Define a posição do objeto a posição do centro da câmera
        transform.position = cameraCenter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestinoCaixa"))
        {
            GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
            if (gameManager != null)
            {
                gameManager.IncrementObjectCorrect();
                gameObject.SetActive(false);
            }
        }
        else
        {
            ResetPosition();
        }
    }
}
