using UnityEngine;

public class DestinationBoxScript : MonoBehaviour
{
    public string objectCorrectTag;
    public Color correctColor;
    private Material originalMaterial;
    private Renderer objectRenderer;
    public Material changeMaterialSameMovingObject;
    public Renderer sAObjectRenderer;
    public string findObjectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectTag))
        {
            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.IncrementObjectCorrect();
                    Destroy(draggableObject.gameObject);
                    objectRenderer.material.color = correctColor;
                    ChangeSAAnimalChickMaterial();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectCorrectTag))
        {
            DraggableObject draggableObject = other.GetComponent<DraggableObject>();
            if (draggableObject != null && !draggableObject.isBeingDragged)
            {
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                if (gameManager != null)
                {
                    gameManager.DecrementObjectCorrect();
                    objectRenderer.material = originalMaterial;
                    draggableObject.ResetPosition();
                    draggableObject.gameObject.SetActive(true); // Ativar o objeto caixa ao sair do destino
                    ResetSAAnimalChickMaterial();
                }
            }
        }
    }

    public bool IsObjectCorrect(GameObject draggableObject)
    {
        // Verifica se o objeto caixa está correto
        // Retorna verdadeiro se estiver correto, falso caso contrário

        if (draggableObject.CompareTag(objectCorrectTag))
        {
            DraggableObject draggableObjectComponent = draggableObject.GetComponent<DraggableObject>();
            if (draggableObjectComponent != null && draggableObjectComponent.isBeingDragged)
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeSAAnimalChickMaterial()
    {
        if (sAObjectRenderer != null)
        {
            sAObjectRenderer.material = changeMaterialSameMovingObject; // Alterar o material do objeto
        }
    }

    public void ResetSAAnimalChickMaterial()
    {
        if (sAObjectRenderer != null)
        {
            sAObjectRenderer.material = originalMaterial; // Redefinir o material do objeto
        }
    }
}
