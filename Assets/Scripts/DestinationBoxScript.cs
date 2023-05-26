using UnityEngine;

public class DestinationBoxScript : MonoBehaviour
{
    public string objectCorrectTag = "PecaPuzzleCaixa";
    public Color correctColor;
    private Material originalMaterial;
    private Renderer objectRenderer;
    public Material newMaterial;
    private Renderer saAnimalChickRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        // Coloando a cor do objeto inicial como preto
        objectRenderer.material.color = Color.black;
        // Encontrar o Renderer do objeto SA_Animal_Chick
        saAnimalChickRenderer = transform.Find("Animal_Chick/SA_Animal_Chick").GetComponent<Renderer>();
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

     private void ChangeSAAnimalChickMaterial()
    {
        if (saAnimalChickRenderer != null)
        {
            saAnimalChickRenderer.material = newMaterial; // Alterar o material do objeto SA_Animal_Chick
        }
    }

    private void ResetSAAnimalChickMaterial()
    {
        if (saAnimalChickRenderer != null)
        {
            saAnimalChickRenderer.material = originalMaterial; // Redefinir o material do objeto SA_Animal_Chick
        }
    }

}
