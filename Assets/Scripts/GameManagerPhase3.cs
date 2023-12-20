using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class GameManagerPhase3 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionPhase3 aRPlacementAndPlaneDetectionController;
    public List<DestinationBoxPhase3Script> destinationBoxes = new();
    public string messageFinish;
    public AudioSource messageSound;

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    }

    private int CountObjects()
    {
        int count = 0;
        DraggableObjectPhase3[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3>();
        foreach (DraggableObjectPhase3 draggableObject in draggableObjects)
        {
            if (tagsObjectsHaveMove.Contains(draggableObject.tag))
            {
                count++;
            }
        }
        foreach (DestinationBoxPhase3Script destination in destinationBoxes)
        {
            count++;
        }
        return count;
    } // OK


    public void IncrementObjectsCorrect()
    {
        objectsCorrect++;
        CheckWin();
    } // OK

    public void DecrementObjectsCorrect()
    {
        objectsCorrect--;
    } // OK

    public void CheckWin()
    {
        if (objectsCorrect == totalObjects)
        {
            ShowMessage(messageFinish, "Reiniciar");
        }
    } // OK

    public void ResetLevel()
    {
        DraggableObjectPhase3[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3>();
        foreach (DraggableObjectPhase3 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        foreach (DestinationBoxPhase3Script destinationBox in destinationBoxes)
        {
            destinationBox.ResetBoxMaterial();
        }

        objectsCorrect = 0;
        // Reset AR Planes
        ResetARPlanes();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel(string nextScene)
    {
        DraggableObjectPhase3[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3>();
        foreach (DraggableObjectPhase3 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectsCorrect = 0;
        // Reset AR Planes
        ResetARPlanes();
        aRPlacementAndPlaneDetectionController.SetAllPlanesActiveOrDeactive(false);
        SceneManager.LoadScene(nextScene);
    }

    private void ResetARPlanes()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = false;
            arPlaneManager.SetTrackablesActive(false);
            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }
            arPlaneManager.enabled = true;
            aRPlacementAndPlaneDetectionController.SetAllPlanesActiveOrDeactive(false);
            arPlaneManager.SetTrackablesActive(true);
        }
    }

    #region UI Callback Manager
    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        uIInformPanel.gameObject.SetActive(false);
    } // OK

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
