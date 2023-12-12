using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class GameManagerObjects3Nivel2 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionObjects3Nivel2 ARPlacementAndPlaneDetection;
    public string messageFinish;

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    } // OK

    private int CountObjects()
    {
        int count = 0;
        foreach (string tag in tagsObjectsHaveMove)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in taggedObjects)
            {
                // Verifique se o objeto implementa a interface IDraggableObjects3Nivel2
                IDraggableObjects3Nivel2 draggableObject = obj.GetComponent<IDraggableObjects3Nivel2>();

                if (draggableObject != null)
                {
                    count++;
                }
            }
        }
        return count;
    }


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
        objectsCorrect = 0;
        // Reset AR Planes
        ResetARPlanes();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevel(string nextScene)
    {
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
    }

    public void ShowMessage(string message, string buttonText)
    {
        messageText.text = message;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        messageText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        uIInformPanel.gameObject.SetActive(true);

        // Desabilita os botões de ajuste e inserir
        ARPlacementAndPlaneDetection.SetButtonsDisable();
    }
    #endregion
}
