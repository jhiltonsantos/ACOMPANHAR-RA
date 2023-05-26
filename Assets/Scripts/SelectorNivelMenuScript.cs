using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorNivelMenuScript : MonoBehaviour
{
    public void SelectNivel0()
    {
        SceneManager.LoadScene("Nivel1Level0SceneAR");
    }

    public void SelectNivel1()
    {
        SceneManager.LoadScene("Nivel1Level1SceneAR");
    }

    public void ReturnMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
