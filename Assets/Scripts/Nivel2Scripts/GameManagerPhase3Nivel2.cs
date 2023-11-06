using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class GameManagerPhase3Nivel2 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionPhase3Nivel2 ARPlacementAndPlaneDetectionPhase1Nivel2;
    public List<DestinationBoxPhase3ScriptNivel2> destinationBoxes = new List<DestinationBoxPhase3ScriptNivel2>();

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    }

    private int CountObjects()
    {
        int count = 0;
        DraggableObjectPhase3Nivel2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3Nivel2>();
        foreach (DraggableObjectPhase3Nivel2 draggableObject in draggableObjects)
        {
            if (tagsObjectsHaveMove.Contains(draggableObject.tag))
            {
                count++;
            }
        }
        foreach (DestinationBoxPhase3ScriptNivel2 destination in destinationBoxes)
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
            ShowMessage("Nível Concluído", "Reiniciar");
        }
    } // OK

    public void ResetLevel()
    {
        DraggableObjectPhase3Nivel2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3Nivel2>();
        foreach (DraggableObjectPhase3Nivel2 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        foreach (DestinationBoxPhase3ScriptNivel2 destinationBox in destinationBoxes)
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
        DraggableObjectPhase3Nivel2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase3Nivel2>();
        foreach (DraggableObjectPhase3Nivel2 draggableObject in draggableObjects)
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
    } // OK

    public void ShowMessage(string message, string buttonText)
    {
        messageText.text = message;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        messageText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        uIInformPanel.gameObject.SetActive(true);

        ARPlacementAndPlaneDetectionPhase1Nivel2.SetButtonsDisable();
    }
    #endregion
}
