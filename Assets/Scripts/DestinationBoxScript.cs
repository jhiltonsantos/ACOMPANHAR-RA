using UnityEngine;

public class DestinationBoxScript : MonoBehaviour
{
    public string objectCorrectTag = "PecaPuzzleCaixa";
    public Color correctColor;
    private Material originalMaterial;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectTag))
        {
            Debug.Log("OnTriggerEnter: Object entered destination box.");
            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.IncrementObjectCorrect();
                    Destroy(draggableObject.gameObject);
                    objectRenderer.material.color = correctColor;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectCorrectTag))
        {
            Debug.Log("OnTriggerExit: Object exited destination box.");
            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && !draggableObject.isBeingDragged)
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.DecrementObjectCorrect();
                    objectRenderer.material = originalMaterial;
                    draggableObject.ResetPosition();
                }
            }
        }
    }
}
