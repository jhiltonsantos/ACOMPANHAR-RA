using System;
using UnityEngine;

public class DestinationBoxPhase2ScriptNivel2 : MonoBehaviour
{
    private Material originalMaterial;
    private Renderer objectRenderer;
    private GameManagerPhase2Nivel2 gameManager;
    public string objectCorrectTag1;
    public string objectCorrectTag2;

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
        gameManager = FindObjectOfType<GameManagerPhase2Nivel2>();
    }
    
    #region "Estado dos objetos"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectTag1))
        {
            DraggableObjectPhase2Nivel2 draggableObject1 = other.GetComponent<DraggableObjectPhase2Nivel2>();
            if (draggableObject1 != null && draggableObject1.isBeingDragged)
            {
                GameManagerPhase2Nivel2 GameManagerPhase2Nivel2 = FindAnyObjectByType<GameManagerPhase2Nivel2>();
                if (GameManagerPhase2Nivel2 != null)
                {
                    GameManagerPhase2Nivel2.IncrementObjectsCorrect();
                    Destroy(draggableObject1.gameObject);
                    DisableCollision(gameObject);
                    ChangeMaterialDestination(objectCorrectTag1);
                }
            }
        }
        else if (other.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase2Nivel2 draggableObject2 = other.GetComponent<DraggableObjectPhase2Nivel2>();
            if (draggableObject2 != null && draggableObject2.isBeingDragged)
            {
                GameManagerPhase2Nivel2 GameManagerPhase2Nivel2 = FindAnyObjectByType<GameManagerPhase2Nivel2>();
                if (GameManagerPhase2Nivel2 != null)
                {
                    GameManagerPhase2Nivel2.IncrementObjectsCorrect();
                    Destroy(draggableObject2.gameObject);
                    DisableCollision(gameObject);
                    ChangeMaterialDestination(objectCorrectTag2);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectCorrectTag1) || other.CompareTag(objectCorrectTag2))
        {
            DraggableObjectPhase2Nivel2 draggableObject1 = other.GetComponent<DraggableObjectPhase2Nivel2>();
            DraggableObjectPhase2Nivel2 draggableObject2 = other.GetComponent<DraggableObjectPhase2Nivel2>();
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
            DraggableObjectPhase2Nivel2 draggableObjectComponent = draggableObject.GetComponent<DraggableObjectPhase2Nivel2>();
            if (draggableObjectComponent != null && draggableObjectComponent.isBeingDragged)
            {
                return true;
            }
        }
        return false;

    }
}
