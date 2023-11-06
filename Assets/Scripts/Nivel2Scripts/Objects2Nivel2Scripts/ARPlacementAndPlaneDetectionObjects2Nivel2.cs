using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ARPlacementAndPlaneDetectionObjects2Nivel2 : MonoBehaviour
{
    ARSessionOrigin m_ARSessionOrigin;
    ARPlaneManager m_ARPlaneManager;
    ARPlacementManagerObjects2Nivel2 m_ARPlacementManagerNivel2;
    private List<ARPlane> activePlanes = new List<ARPlane>();

    public GameObject placeButton;
    public GameObject adjustButton;
    public GameObject closeButton;
    public GameObject raycastCenterImage;
    public GameObject scaleSlider;
    public string selectCloseButtonScreen;

    // Tamanho mínimo e máximo do plano (em metros)
    public float minPlaneSize = 0.5f;
    public float maxPlaneSize = 2f;
    // Distância mínima entre os planos para evitar sobreposição (em metros)
    public float minDistance = 1f;

    private void Awake()
    {
        m_ARSessionOrigin = GetComponent<ARSessionOrigin>();
        m_ARPlaneManager = m_ARSessionOrigin.GetComponent<ARPlaneManager>();
        m_ARPlacementManagerNivel2 = m_ARSessionOrigin.GetComponent<ARPlacementManagerObjects2Nivel2>();
    }

    void Start()
    {
        placeButton.SetActive(true);
        scaleSlider.SetActive(true);
        raycastCenterImage.SetActive(true);
        adjustButton.SetActive(false);
        closeButton.SetActive(true);
    }

    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManagerNivel2.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        raycastCenterImage.SetActive(false);
        adjustButton.SetActive(true);
    }

    public void EnableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManagerNivel2.enabled = true;
        SetAllPlanesActiveOrDeactive(true);

        foreach (var plane in m_ARPlaneManager.trackables)
        {
            if (plane.size.x < minPlaneSize || plane.size.x > maxPlaneSize)
            {
                plane.gameObject.SetActive(false);
            }
        }

        DisablePlaneOverlap(minDistance);

        placeButton.SetActive(true);
        scaleSlider.SetActive(true);
        raycastCenterImage.SetActive(true);
        adjustButton.SetActive(false);
    }

    public void CloseButtonEnable()
    {
        SceneManager.LoadScene(selectCloseButtonScreen);
    }

    public void SetButtonsDisable()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManagerNivel2.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        raycastCenterImage.SetActive(false);
        adjustButton.SetActive(false);
    }

    private void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
            if (value)
            {
                activePlanes.Add(plane);
            }
            else
            {
                activePlanes.Remove(plane);
            }
        }
    }

    private void DisablePlaneOverlap(float minDistance)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            if (activePlanes.Contains(plane))
            {
                continue;
            }

            bool isOverlapping = false;
            foreach (var activePlane in activePlanes)
            {
                if (Vector3.Distance(plane.center, activePlane.center) < minDistance)
                {
                    isOverlapping = true;
                    break;
                }
            }

            if (isOverlapping)
            {
                plane.gameObject.SetActive(false);
            }
            else
            {
                // Reativar o plano se não estiver mais sobreposto
                plane.gameObject.SetActive(true);
            }
        }
    }
}
