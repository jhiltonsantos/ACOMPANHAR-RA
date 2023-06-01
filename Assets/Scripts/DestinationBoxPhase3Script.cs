using System;
using UnityEngine;

public class DestinationBoxPhase3Script : MonoBehaviour
{
    private Material originalMaterial;
    private Renderer objectRenderer;
    private GameManagerPhase3 gameManager;
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
        gameManager = FindObjectOfType<GameManagerPhase3>();
    }
    
    #region "Estado dos objetos"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectTag1))
        {
            DraggableObjectPhase3 draggableObject1 = other.GetComponent<DraggableObjectPhase3>();
            if (draggableObject1 != null && draggableObject1.isBeingDragged)
            {
                if (gameManager != null)
                {
                    gameManager.IncrementObjectsCorrect();
                    Destroy(draggableObject1.gameObject);
                    DisableCollision(gameObject);
                    objectRenderer.material.color = correctColor;
                    ChangeMaterialDestination(objectCorrectTag1);
                }
            }
        }
        else if (other.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase3 draggableObject2 = other.GetComponent<DraggableObjectPhase3>();
            if (draggableObject2 != null && draggableObject2.isBeingDragged)
            {
                if (gameManager != null)
                {
                    gameManager.IncrementObjectsCorrect();
                    Destroy(draggableObject2.gameObject);
                    DisableCollision(gameObject);
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
            DraggableObjectPhase3 draggableObject1 = other.GetComponent<DraggableObjectPhase3>();
            DraggableObjectPhase3 draggableObject2 = other.GetComponent<DraggableObjectPhase3>();
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

    #endregion

    private void DisableCollision(GameObject gameObject)
    {
        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
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
