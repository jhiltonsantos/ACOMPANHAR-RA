using UnityEngine;

public class CubeObjectScript : MonoBehaviour
{
    public Color objectColor;
    private Material originalMaterial;
    private Renderer objectRenderer;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        objectRenderer.material.color = objectColor;
    }

    void Update()
    {
        
    }
}
