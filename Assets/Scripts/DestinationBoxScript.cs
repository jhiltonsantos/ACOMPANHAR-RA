using UnityEngine;

public class DestinationBoxScript : MonoBehaviour
{
    public string objectCorrectTag = "PecaPuzzleCaixa";
    public Color correctColor;

    private Material originalMaterial;
    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        DraggableObject draggableObject = other.GetComponent<DraggableObject>();

        if (draggableObject != null && draggableObject.isBeingDragged)
        {
            if (other.CompareTag(objectCorrectTag))
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.IncrementObjectCorrect();
                    Destroy(draggableObject.gameObject);
                    renderer.material.color = correctColor;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DraggableObject draggableObject = other.GetComponent<DraggableObject>();

        if (draggableObject != null && !draggableObject.isBeingDragged)
        {
            if (other.CompareTag(objectCorrectTag))
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.DecrementObjectCorrect();
                    renderer.material = originalMaterial;
                }
            }
        }
    }
}
