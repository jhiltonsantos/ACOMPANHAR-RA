using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class GameManagerPhase4 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public List<DestinationBoxPhase4Script> destinationBoxes = new List<DestinationBoxPhase4Script>();
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionPhase4 aRPlacementAndPlaneDetectionController;
    public string messageFinish;

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    }

    private int CountObjects()
    {
        int count = 0;
        DraggableObjectPhase4[] draggableObjects = FindObjectsOfType<DraggableObjectPhase4>();
        foreach (DraggableObjectPhase4 draggableObject in draggableObjects)
        {
            if (tagsObjectsHaveMove.Contains(draggableObject.tag))
            {
                count++;
            }
        }
        foreach (DestinationBoxPhase4Script destination in destinationBoxes)
        {
            count++;
        }
        return count;
    }


    public void IncrementObjectsCorrect()
    {
        objectsCorrect++;
        CheckWin();
    }

    public void DecrementObjectsCorrect()
    {
        objectsCorrect--;
    }

    public void CheckWin()
    {
        if (objectsCorrect == totalObjects)
        {
            ShowMessage(messageFinish, "Reiniciar");
        }
    }

    public void ResetLevel()
    {
        DraggableObjectPhase4[] draggableObjects = FindObjectsOfType<DraggableObjectPhase4>();
        foreach (DraggableObjectPhase4 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        foreach (DestinationBoxPhase4Script destinationBox in destinationBoxes)
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
        DraggableObjectPhase4[] draggableObjects = FindObjectsOfType<DraggableObjectPhase4>();
        foreach (DraggableObjectPhase4 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }
        objectsCorrect = 0;
        // Reset AR Planes
        ResetARPlanes();
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
            arPlaneManager.SetTrackablesActive(true);
        }
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
        messageText.text = message;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        messageText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        uIInformPanel.gameObject.SetActive(true);

        aRPlacementAndPlaneDetectionController.SetButtonsDisable();
    }
    #endregion
}
