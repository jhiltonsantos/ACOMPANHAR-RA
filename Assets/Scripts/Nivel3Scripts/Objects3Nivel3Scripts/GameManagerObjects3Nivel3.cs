using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class GameManagerObjects3Nivel3 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;
    public ARPlaneManager arPlaneManager;
    public ARPlacementAndPlaneDetectionObjects3Nivel3 ARPlacementAndPlaneDetection;
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
                // Verifique se o objeto implementa a interface IDraggableObjects3Nivel3
                IDraggableObjects3Nivel3 draggableObject = obj.GetComponent<IDraggableObjects3Nivel3>();

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
