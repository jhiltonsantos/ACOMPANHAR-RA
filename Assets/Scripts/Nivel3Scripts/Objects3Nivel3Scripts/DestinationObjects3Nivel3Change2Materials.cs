using UnityEngine;

public class DestinationObjects3Nivel3Change2Materials : MonoBehaviour
{
    public Material correctMaterial1; // Material a ser aplicado quando a peça correta é colocada
    public Renderer sAObjectRenderer1; // O objeto que vai ser aplicado o correctMaterial
    public string findObjectRenderer1; // O caminho do objeto que vai ser aplicado o correctMaterial

    public Material correctMaterial2;
    public Renderer sAObjectRenderer2;
    public string findObjectRenderer2;

    private Renderer objectRenderer;
    public string objectCorrectDraggable;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        sAObjectRenderer1 = transform.Find(findObjectRenderer1).GetComponent<Renderer>();
        sAObjectRenderer2 = transform.Find(findObjectRenderer2).GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou possui a tag correta
        if (other.CompareTag(objectCorrectDraggable))
        {
            DraggableObjects3Nivel3Change2Materials draggableObject = other.GetComponent<DraggableObjects3Nivel3Change2Materials>();

            if (draggableObject != null && draggableObject.isBeingDragged)
            {
                // A peça correta foi colocada no destino
                sAObjectRenderer1.material = correctMaterial1;
                sAObjectRenderer2.material = correctMaterial2;
            }
        }
    }

    public bool IsObjectCorrect(Collider other)
    {
        DraggableObjects3Nivel3Change2Materials draggableObject = other.GetComponent<DraggableObjects3Nivel3Change2Materials>();

        // Verifica se o objeto que entrou possui o script correto
        return (draggableObject != null && draggableObject.isBeingDragged);
    }
}
