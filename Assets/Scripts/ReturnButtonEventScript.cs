using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonEventScript : MonoBehaviour
{
    public string returnScene;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                ReturnMainScreen();
            }
        }
    }

    private void ReturnMainScreen()
    {
        SceneManager.LoadScene(returnScene);
    }
}
