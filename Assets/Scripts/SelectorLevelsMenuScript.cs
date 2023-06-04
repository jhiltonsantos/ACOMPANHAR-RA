using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SelectorLevelsMenuScript : MonoBehaviour
{
    public List<string> levelScenes = new List<string>();

    public void SelectLevel(int index)
    {
        if (index >= 0 && index < levelScenes.Count)
        {
            SceneManager.LoadScene(levelScenes[index]);
        }
    }

    public void ReturnNivelSelectorScreen()
    {
        SceneManager.LoadScene("SelectorNivelScene");
    }
}