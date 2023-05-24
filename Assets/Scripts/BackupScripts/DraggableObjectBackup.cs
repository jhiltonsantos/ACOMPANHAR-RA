using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using System.Collections.Generic;

public class DraggableObjectBackup : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector2 touchPosition;
    public bool isBeingDragged;
    private Rigidbody rb;
    private Vector3 initialPosition;

    public Transform correctDestination; // Referência ao objeto do destino

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        correctDestination.position = new Vector3(correctDestination.position.x, correctDestination.position.y, transform.position.z + 0.1f);
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

        if (!ObjectCorrectPosition())
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private bool ObjectCorrectPosition()
    {
        // TODO Implemente a lógica para verificar se o objeto está na posição correta do destino
        // Retorne true se estiver na posição correta, caso contrário, retorne false

        // Por exemplo, você pode comparar a posição do objeto com a posição correta do destino:
        float distanceThreshold = 0.1f; // A distância máxima permitida entre o objeto e o destino para considerá-lo como posição correta

        // Verifica se o objeto está próximo o suficiente da posição correta
        float distance = Vector3.Distance(transform.position, correctDestination.position);
        if (distance <= distanceThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
