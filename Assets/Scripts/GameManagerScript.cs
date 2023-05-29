using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int objectCorrect = 0;
    public int totalObjects = 0;
    public string tagObjectHasMove;
    public TextMeshProUGUI messageText;
    public Button resetButton;
    public Image uIInformPanel;

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
            Debug.Log("Finalizou");
            ShowMessage("Nível Concluído", "Reiniciar");
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
