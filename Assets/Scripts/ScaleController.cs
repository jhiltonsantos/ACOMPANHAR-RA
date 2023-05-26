using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    ARSessionOrigin m_ARSessionOrigin;
    public Slider scaleSlider;

    private void Awake()
    {
        m_ARSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSlideValueChanged);

    }

    public void OnSlideValueChanged(float value)
    {
        if (scaleSlider != null)
        {
            m_ARSessionOrigin.transform.localScale = Vector3.one / value;
        }
    }

    void Update()
    {

    }
}
