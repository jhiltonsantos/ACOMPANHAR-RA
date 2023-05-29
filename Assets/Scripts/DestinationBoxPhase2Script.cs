using UnityEngine;

public class DestinationBoxPhase2Script : MonoBehaviour
{
    public Color correctColor;
    private Material originalMaterial;
    private Renderer objectRenderer;
    public bool isActivated = false;
    public string activatedObjectTag;

    private GameManagerPhase2 gameManager;
    private bool isSecondBoxActivated = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        gameManager = FindObjectOfType<GameManagerPhase2>();
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
            if (gameManager != null)
            {
                if (!gameManager.IsObjectAlreadyActivated(draggableObject))
                {
                    gameManager.ObjectReachedDestination();
                    gameManager.IncrementObjectsCorrect();
                    isActivated = true;
                    activatedObjectTag = draggableObject.tag;
                    objectRenderer.material.color = correctColor;

                    if (!isSecondBoxActivated)
                    {
                        isSecondBoxActivated = true;
                        gameManager.CheckWin();
                    }
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
        isSecondBoxActivated = false;
    }
}
