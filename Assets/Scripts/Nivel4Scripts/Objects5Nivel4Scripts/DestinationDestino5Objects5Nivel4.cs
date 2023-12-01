using UnityEngine;

public class DestinationDestino5Objects5Nivel4 : MonoBehaviour
{
    public Material correctMaterial; // Material a ser aplicado quando a peça correta é colocada
    public Renderer sAObjectRenderer; // O objeto que vai ser aplicado o correctMaterial
    public string findObjectRenderer; // O caminho do objeto que vai ser aplicado o correctMaterial
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        sAObjectRenderer = transform.Find(findObjectRenderer).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou possui a tag correta (PecaCaixa5)
        if (other.CompareTag("PecaCaixa5"))
        {
            DraggablePecaCaixa5Objects5Nivel4 draggableObject = other.GetComponent<DraggablePecaCaixa5Objects5Nivel4>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer.material = correctMaterial;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggablePecaCaixa5Objects5Nivel4 draggableObject = other.GetComponent<DraggablePecaCaixa5Objects5Nivel4>();

        // Verifica se o objeto que entrou possui o script correto (PecaCaixa5)
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
