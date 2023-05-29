using System;
using UnityEngine;

public class DestinationBoxPhase2Script : MonoBehaviour
{
    private Material originalMaterial;
    private Renderer objectRenderer;
    private GameManagerPhase2 gameManager;
    public string objectCorrectTag1;
    public string objectCorrectTag2;
    public Color correctColor;

    // Propriedades para alterar o material do objeto
    public Material changeMaterialSameMovingObject1;
    public Material changeMaterialSameMovingObject2;
    public Renderer sAObjectRenderer;
    public string findObjectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
        gameManager = FindObjectOfType<GameManagerPhase2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectTag1))
        {
            DraggableObjectPhase2 draggableObject1 = other.GetComponent<DraggableObjectPhase2>();
            if (draggableObject1 != null && draggableObject1.isBeingDragged)
            {
                GameManagerPhase2 gameManagerPhase2 = FindAnyObjectByType<GameManagerPhase2>();
                if (gameManagerPhase2 != null)
                {
                    gameManagerPhase2.IncrementObjectsCorrect();
                    Destroy(draggableObject1.gameObject);
                    objectRenderer.material.color = correctColor;
                    ChangeMaterialDestination(objectCorrectTag1);
                }
            }
        }
        else if (other.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase2 draggableObject2 = other.GetComponent<DraggableObjectPhase2>();
            if (draggableObject2 != null && draggableObject2.isBeingDragged)
            {
                GameManagerPhase2 gameManagerPhase2 = FindAnyObjectByType<GameManagerPhase2>();
                if (gameManagerPhase2 != null)
                {
                    gameManagerPhase2.IncrementObjectsCorrect();
                    Destroy(draggableObject2.gameObject);
                    objectRenderer.material.color = correctColor;
                    ChangeMaterialDestination(objectCorrectTag2);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectCorrectTag1) || other.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase2 draggableObject1 = other.GetComponent<DraggableObjectPhase2>();
            DraggableObjectPhase2 draggableObject2 = other.GetComponent<DraggableObjectPhase2>();
            if (draggableObject1 != null && draggableObject1.isBeingDragged || draggableObject2 != null && draggableObject2.isBeingDragged)
            {
                if (gameManager != null && draggableObject1)
                {
                    gameManager.DecrementObjectsCorrect();
                    objectRenderer.material = originalMaterial;
                    draggableObject1.ResetPosition();
                    draggableObject1.gameObject.SetActive(true);
                    ResetBoxMaterial();
                }
                else if (gameManager != null && draggableObject2)
                {
                    gameManager.DecrementObjectsCorrect();
                    objectRenderer.material = originalMaterial;
                    draggableObject2.ResetPosition();
                    draggableObject2.gameObject.SetActive(true);
                    ResetBoxMaterial();
                }
            }
        }
    }

    private void ChangeMaterialDestination(string tag)
    {

        if (sAObjectRenderer != null && tag == objectCorrectTag1)
        {
            sAObjectRenderer.material = changeMaterialSameMovingObject1;
        }
        else if (sAObjectRenderer != null && tag == objectCorrectTag2)
        {
            sAObjectRenderer.material = changeMaterialSameMovingObject2;
        }

    }

    public void ResetBoxMaterial()
    {
        objectRenderer.material = originalMaterial;
    }

    public bool IsObjectCorrect(GameObject draggableObject)
    {
        if (draggableObject.CompareTag(objectCorrectTag1) || draggableObject.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase2 draggableObjectComponent = draggableObject.GetComponent<DraggableObjectPhase2>();
            if (draggableObjectComponent != null && draggableObjectComponent.isBeingDragged)
            {
                return true;
            }
        }
        return false;

    }
}
