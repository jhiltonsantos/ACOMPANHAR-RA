using UnityEngine;

public class DestinationBoxPhase1ScriptNivel2 : MonoBehaviour
{
    public string objectCorrectTag;
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
            DraggableObjectPhase1Nivel2 draggableObject = other.GetComponent<DraggableObjectPhase1Nivel2>();
            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                GameManagerPhase1Nivel2 gameManager = FindObjectOfType<GameManagerPhase1Nivel2>();
                if (gameManager != null)
                {
                    gameManager.IncrementObjectCorrect();
                    Destroy(draggableObject.gameObject);
                    ChangeSAAnimalChickMaterial();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectCorrectTag))
        {
            DraggableObjectPhase1Nivel2 draggableObject = other.GetComponent<DraggableObjectPhase1Nivel2>();
            if (draggableObject != null && !draggableObject.isBeingDragged)
            {
                GameManagerPhase1Nivel2 gameManager = FindObjectOfType<GameManagerPhase1Nivel2>();
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
            DraggableObjectPhase1Nivel2 draggableObjectComponent = draggableObject.GetComponent<DraggableObjectPhase1Nivel2>();
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
