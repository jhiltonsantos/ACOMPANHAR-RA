using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerPhase2 : MonoBehaviour
{
    public int objectsCorrect = 0;
    public int totalObjects = 0;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;
    private int objectsReachedDestination = 0;
    public List<string> tagsObjectsHaveMove;

    public List<DestinationBoxPhase2Script> destinationBoxes = new List<DestinationBoxPhase2Script>();

    public void Start()
    {
        totalObjects = CountObjects();
        HideMessage();
    }

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

    public void ObjectReachedDestination()
    {
        objectsReachedDestination++;

        // Verificar se todos os destinos estão ativados
        bool allDestinationsActivated = true;
        foreach (DestinationBoxPhase2Script destinationBox in destinationBoxes)
        {
            if (!destinationBox.isActivated)
            {
                allDestinationsActivated = false;
                break;
            }
        }

        if (objectsReachedDestination == totalObjects && allDestinationsActivated)
        {
            CheckWin();
        }
    }

    public bool IsObjectAlreadyActivated(GameObject draggableObject)
    {
        string objectTag = draggableObject.tag;

        foreach (DestinationBoxPhase2Script destinationBox in destinationBoxes)
        {
            if (destinationBox.isActivated && destinationBox.activatedObjectTag == objectTag)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsDestinationBoxActivated(DestinationBoxPhase2Script destinationBox)
    {
        string destinationTag = destinationBox.activatedObjectTag;

        foreach (string tag in tagsObjectsHaveMove)
        {
            if (destinationBox.isActivated && destinationTag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckWin()
    {
        if (objectsCorrect == totalObjects && objectsReachedDestination == totalObjects)
        {
            ShowMessage("Nível Concluído", "Reiniciar");
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
        objectsReachedDestination = 0;

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
        objectsReachedDestination = 0;

        SceneManager.LoadScene(nextScene);
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
    }
    #endregion
}
