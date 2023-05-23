using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Nivel1Level0SceneAR");
    }
}
