using UnityEngine;

public class DestinationObjects2Nivel2 : MonoBehaviour
{
    public Material correctMaterial; // Material a ser aplicado quando a peça correta é colocada
    private Renderer objectRenderer;
    public Renderer sAObjectRenderer; // O objeto que vai ser aplicado o correctMaterial
    public string findObjectRenderer;
    public string objectCorrectDraggable;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou possui a tag correta (PecaCaixa)
        if (other.CompareTag(objectCorrectDraggable))
        {
            DraggableObjects2Nivel2 draggableObject = other.GetComponent<DraggableObjects2Nivel2>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer.material = correctMaterial;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggableObjects2Nivel2 draggableObject = other.GetComponent<DraggableObjects2Nivel2>();

        // Verifica se o objeto que entrou possui o script correto (PecaCaixa)
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
