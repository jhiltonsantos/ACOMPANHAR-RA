using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManagerPhase1Nivel2 : MonoBehaviour
{
    public int objectCorrect = 0;
    public int totalObjects = 0;
    public string tagObjectHasMove;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionPhase1Nivel2 ARPlacementAndPlaneDetectionPhase1Nivel2;

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
            ShowMessage("Fase Concluída", "Reiniciar");
        }
    }

    public void ResetLevel()
    {
        DraggableObjectPhase1Nivel2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase1Nivel2>();
        foreach (DraggableObjectPhase1Nivel2 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectCorrect = 0;
        // Reset AR Planes
        ResetARPlanes();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel(string nextScene)
    {
        DraggableObjectPhase1Nivel2[] draggableObjects = FindObjectsOfType<DraggableObjectPhase1Nivel2>();
        foreach (DraggableObjectPhase1Nivel2 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectCorrect = 0;
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
    }
    public void ShowMessage(string message, string buttonText)
    {
        messageText.text = message;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        messageText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        uIInformPanel.gameObject.SetActive(true);
        
        // Desabilita os botões de ajuste e inserir
        ARPlacementAndPlaneDetectionPhase1Nivel2.SetButtonsDisable();
    }
    #endregion

}
