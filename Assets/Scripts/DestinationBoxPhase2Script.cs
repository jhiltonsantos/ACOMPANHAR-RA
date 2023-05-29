using UnityEngine;

public class DestinationBoxPhase2Script : MonoBehaviour
{
    public Color correctColor;
    private Material originalMaterial;
    private Renderer objectRenderer;
    public bool isActivated = false;
    public string activatedObjectTag;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && (other.CompareTag("PecaCaixa1") || other.CompareTag("PecaCaixa2")))
        {
            ActivateDestination(other.gameObject);
        }
    }

    private void ActivateDestination(GameObject draggableObject)
    {
        DraggableObjectPhase2 draggableObjectScript = draggableObject.GetComponent<DraggableObjectPhase2>();
        if (draggableObjectScript != null && draggableObjectScript.isBeingDragged)
        {
            GameManagerPhase2 gameManager = FindObjectOfType<GameManagerPhase2>();
            if (gameManager != null)
            {
                if (!gameManager.IsObjectAlreadyActivated(draggableObject))
                {
                    gameManager.IncrementObjectsCorrect();
                    isActivated = true;
                    activatedObjectTag = draggableObject.tag;
                    objectRenderer.material.color = correctColor;
                    gameManager.ObjectReachedDestination(); // Adicionado para verificar vitória
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActivated && (other.CompareTag("PecaCaixa1") || other.CompareTag("PecaCaixa2")))
        {
            DraggableObjectPhase2 draggableObject = other.GetComponent<DraggableObjectPhase2>();
            if (draggableObject != null && !draggableObject.isBeingDragged)
            {
                GameManagerPhase2 gameManager = FindObjectOfType<GameManagerPhase2>();
                if (gameManager != null)
                {
                    gameManager.DecrementObjectsCorrect();
                    ResetBoxMaterial();
                    isActivated = false;
                    activatedObjectTag = string.Empty;
                }
            }
        }
    }

    public void ResetBoxMaterial()
    {
        isActivated = false;
        objectRenderer.material = originalMaterial;
    }
}
