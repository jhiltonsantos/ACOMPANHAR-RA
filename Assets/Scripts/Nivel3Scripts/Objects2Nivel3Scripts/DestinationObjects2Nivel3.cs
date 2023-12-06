using UnityEngine;

public class DestinationObjects2Nivel3 : MonoBehaviour
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
        // Verifica se o objeto que entrou possui a tag correta
        if (other.CompareTag(objectCorrectDraggable))
        {
            DraggableObjects2Nivel3 draggableObject = other.GetComponent<DraggableObjects2Nivel3>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer.material = correctMaterial;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggableObjects2Nivel3 draggableObject = other.GetComponent<DraggableObjects2Nivel3>();

        // Verifica se o objeto que entrou possui o script correto
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
