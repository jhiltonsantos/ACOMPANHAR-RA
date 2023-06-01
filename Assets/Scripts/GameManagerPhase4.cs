using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerPhase4 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    public List<string> tagsObjectsHaveMove;

    public List<DestinationBoxPhase4Script> destinationBoxes = new List<DestinationBoxPhase4Script>();

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    } // OK

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } // OK

    public void ContinueLevel(string nextScene)
    {
        DraggableObjectPhase4[] draggableObjects = FindObjectsOfType<DraggableObjectPhase4>();
        foreach (DraggableObjectPhase4 draggableObject in draggableObjects)
        {
            draggableObject.ResetPosition();
        }

        objectsCorrect = 0;

        SceneManager.LoadScene(nextScene);
    } // OK

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
    } // OK
    #endregion
}
