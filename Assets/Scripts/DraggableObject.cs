using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using System.Collections.Generic;

public class DraggableObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector2 touchPosition;
    public bool isBeingDragged;
    private Rigidbody rb;
    private Vector3 initialPosition;
    public Transform correctDestination; // Referência ao objeto do destino
    private bool isPlacedOnDestination; // Indica se o objeto foi colocado no destino

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
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
        isPlacedOnDestination = false; // Reinicia a flag de colocação no destino
    }

    private void StopDragging()
    {
        isBeingDragged = false;
        rb.isKinematic = false;

        if (isPlacedOnDestination)
        {
            // Objeto colocado no destino corretamente
            // Você pode adicionar qualquer lógica adicional aqui, se necessário
        }
        else
        {
            // Objeto não foi colocado no destino corretamente
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestinoCaixa"))
        {
            isPlacedOnDestination = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DestinoCaixa"))
        {
            isPlacedOnDestination = false;
        }
    }
}
