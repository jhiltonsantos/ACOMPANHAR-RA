using UnityEngine;

public class DestinationObjects4Nivel4 : MonoBehaviour
{
    public Material correctMaterial; // Material a ser aplicado quando a peça correta é colocada
    public Renderer sAObjectRenderer; // O objeto que vai ser aplicado o correctMaterial
    public string findObjectRenderer; // O caminho do objeto que vai ser aplicado o correctMaterial
    private Renderer objectRenderer;
    public string correctTagDraggable;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou possui a tag correta (PecaCaixa1)
        if (other.CompareTag(correctTagDraggable))
        {
            DraggableObjects4Nivel4 draggableObject = other.GetComponent<DraggableObjects4Nivel4>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer.material = correctMaterial;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggableObjects4Nivel4 draggableObject = other.GetComponent<DraggableObjects4Nivel4>();

        // Verifica se o objeto que entrou possui o script correto (PecaCaixa1)
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
