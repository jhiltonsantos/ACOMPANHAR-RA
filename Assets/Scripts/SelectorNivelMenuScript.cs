using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorNivelMenuScript : MonoBehaviour
{
    public string scene1;
    public string scene2;
    public void SelectNivel1()
    {
        SceneManager.LoadScene(scene1);
    }

    public void SelectNivel2()
    {
        SceneManager.LoadScene(scene2);
    }

    public void ReturnMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
