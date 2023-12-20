using UnityEngine;

public class DestinationObjects6Nivel4 : MonoBehaviour
{
    public Material correctMaterial; // Material a ser aplicado quando a peça correta é colocada
    public Renderer sAObjectRenderer; // O objeto que vai ser aplicado o correctMaterial
    public string findObjectRenderer; // O caminho do objeto que vai ser aplicado o correctMaterial
    private Renderer objectRenderer;
    public string objectCorrectDraggable;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCorrectDraggable))
        {
            DraggableObjects6Nivel4 draggableObject = other.GetComponent<DraggableObjects6Nivel4>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer.material = correctMaterial;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggableObjects6Nivel4 draggableObject = other.GetComponent<DraggableObjects6Nivel4>();

        // Verifica se o objeto que entrou possui o script correto (PecaCaixa1)
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
