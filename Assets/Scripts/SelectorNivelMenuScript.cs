using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SelectorNivelMenuScript : MonoBehaviour
{
    public List<string> nivelScenes = new List<string>();

    public void SelectNivel(int index)
    {
        if (index >= 0 && index < nivelScenes.Count)
        {
            SceneManager.LoadScene(nivelScenes[index]);
        }
    }

    public void ReturnMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    
}