using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NivelMenuScipt : MonoBehaviour
{
   public GameObject levelButtonPrefab;  // Prefab do botão do nível
    public Transform buttonContainer;  // Container dos botões do nível

    public string[] levelNames;  // Nomes das cenas dos níveis

    private void Start()
    {
        CreateLevelButtons();
    }

    private void CreateLevelButtons()
    {
        // Percorre os nomes dos níveis e cria um botão para cada um
        for (int i = 0; i < levelNames.Length; i++)
        {
            // Cria uma instância do prefab do botão
            GameObject buttonObj = Instantiate(levelButtonPrefab, buttonContainer);

            // Configura o texto do botão com o nome do nível
            buttonObj.GetComponentInChildren<Text>().text = "Level " + (i + 1);

            // Obtém o nome do nível para carregar quando o botão for clicado
            string levelName = levelNames[i];

            // Adiciona um listener de clique ao botão para carregar o nível
            buttonObj.GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelName));
        }
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
