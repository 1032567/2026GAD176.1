using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public string gameplaySceneName;

    private SceneManager sceneManager;

    private List<MenuButtonData> menuButtons;

    void Start()
    {
        sceneManager = SceneManager.Instance;
        menuButtons = new List<MenuButtonData>
        {
            new MenuButtonData { label = "Play", onClick = PlayGame },
            new MenuButtonData { label = "Options", onClick = OpenOptions },
            new MenuButtonData { label = "Exit", onClick = ExitGame }
        };

        BuildMenu();
    }

    void BuildMenu()
    {
        foreach (MenuButtonData data in menuButtons)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<Text>().text = data.label;
            newButton.GetComponent<Button>().onClick.AddListener(data.onClick);
        }
    }

    public void PlayGame()
    {
    }

    public void OpenOptions()
    {
        Debug.Log("Options button clicked - build this menu later");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}