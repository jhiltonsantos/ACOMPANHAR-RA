using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using System.Collections.Generic;

public class DraggablePecaCaixa3Objects3Nivel3 : MonoBehaviour, IDraggableObjects3Nivel3
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool isBeingDragged;
    private Rigidbody rb;
    private DestinationDestino3Objects3Nivel3 destinoScript;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        rb = GetComponent<Rigidbody>();
        destinoScript = GetComponentInParent<DestinationDestino3Objects3Nivel3>();
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

        if (destinoScript != null && !destinoScript.IsObjectCorrect(this.GetComponent<Collider>()))
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane));
        transform.position = cameraCenter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destino3"))
        {
            GameManagerObjects3Nivel3 gameManager = FindObjectOfType<GameManagerObjects3Nivel3>();
            if (gameManager != null)
            {
                gameManager.IncrementObjectsCorrect();
                gameObject.SetActive(false);
            }
        }
        else
        {
            ResetPosition();
        }
    }
}
