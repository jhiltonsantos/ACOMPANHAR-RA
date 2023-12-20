using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class GameManagerPhase2 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public ARPlaneManager arPlaneManager;
    public List<DestinationBoxPhase2Script> destinationBoxes = new List<DestinationBoxPhase2Script>();
    public ARPlacementAndPlaneDetectionPhase2 aRPlacementAndPlaneDetectionController;
    public string messageFinish;
    public AudioSource messageSound;

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    } // OK

    private int CountObjects()
    {
        int count = 0;
        DraggableObjectPhase2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase2>();
        foreach (DraggableObjectPhase2 draggableObject in draggableObjects)
        {
            if (tagsObjectsHaveMove.Contains(draggableObject.tag))
            {
                count++;
            }
        }
        foreach (DestinationBoxPhase2Script destination in destinationBoxes)
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
    }

    public void ResetLevel()
    {
        DraggableObjectPhase2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase2>();
        foreach (DraggableObjectPhase2 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        foreach (DestinationBoxPhase2Script destinationBox in destinationBoxes)
        {
            destinationBox.ResetBoxMaterial();
        }

        objectsCorrect = 0;
        ResetARPlanes();
        aRPlacementAndPlaneDetectionController.SetAllPlanesActiveOrDeactive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel(string nextScene)
    {
        DraggableObjectPhase2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase2>();
        foreach (DraggableObjectPhase2 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectsCorrect = 0;
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

        // Desabilita os botões de ajuste e inserir
        aRPlacementAndPlaneDetectionController.SetButtonsDisable();
    }
    #endregion
}
