using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    public int objectCorrect = 0;
    public int totalObjects = 0;
    public string tagObjectHasMove;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionController aRPlacementAndPlaneDetectionController;
    public string messageFinish;
    public AudioSource messageSound;

    void Start()
    {
        totalObjects = LengthObjects();
        HideMessage();
    }

    private int LengthObjects()
    {
        return GameObject.FindGameObjectsWithTag(tagObjectHasMove).Length;
    }

    public void IncrementObjectCorrect()
    {
        objectCorrect++;
        CheckWin();
    }

    public void DecrementObjectCorrect()
    {
        objectCorrect--;
    }

    public void CheckWin()
    {
        if (objectCorrect == totalObjects)
        {
            ShowMessage(messageFinish, "Reiniciar");
        }
    }

    public void ResetLevel()
    {
        DraggableObject[] draggableObjects = FindObjectsOfType<DraggableObject>();
        foreach (DraggableObject draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectCorrect = 0;
        ResetARPlanes();
        aRPlacementAndPlaneDetectionController.SetAllPlanesActiveOrDeactive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel(string nextScene)
    {
        DraggableObject[] draggableObjects = FindObjectsOfType<DraggableObject>();
        foreach (DraggableObject draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectCorrect = 0;
        ResetARPlanes();
        aRPlacementAndPlaneDetectionController.SetAllPlanesActiveOrDeactive(false);
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator ResetARPlanesCoroutine()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = false;
            arPlaneManager.SetTrackablesActive(false);

            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
                yield return null; // Aguarde um frame
            }

            arPlaneManager.enabled = true;
            arPlaneManager.SetTrackablesActive(true);
        }
    }

    public void ResetARPlanes()
    {
        StartCoroutine(ResetARPlanesCoroutine());
    }

    #region UI Callback Manager
    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        uIInformPanel.gameObject.SetActive(false);
    }
    
    public void ShowMessage(string message, string buttonText)
    {
        messageSound?.Play();
        messageText.text = message;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        messageText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        uIInformPanel.gameObject.SetActive(true);

        aRPlacementAndPlaneDetectionController.SetButtonsDisable();

    }
    #endregion
}
