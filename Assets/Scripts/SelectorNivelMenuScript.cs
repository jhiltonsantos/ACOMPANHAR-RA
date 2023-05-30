using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SelectorNivelMenuScript : MonoBehaviour
{
    public List<string> levelScenes = new List<string>();

    public void SelectNivel(int index)
    {
        if (index >= 0 && index < levelScenes.Count)
        {
            SceneManager.LoadScene(levelScenes[index]);
        }
    }

    public void ReturnMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}